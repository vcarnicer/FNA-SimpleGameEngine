using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SDL2;
using Color = Microsoft.Xna.Framework.Color;

namespace GameEngine
{
    //TO ADD NEW GAME OBJECTS TO THE EDITOR FOLLOW THESE INSTRUCTIONS:
    //1. Add the name of the object to the enum ObjectType (make sure it's spelled exactly the same). Make sure you add it BEFORE NumOfObjects.
    //2. Make sure objectsNamespace is correctly set. This should be the namespace that all your classes are under. Go to Player.cs and copy the namespace that's there, it's likely the one you need.

    public partial class Editor : Form
    {
        public Game1 game;
        IntPtr gameWinHandle; //HWND for the game window.

        public enum CreateMode { None, Walls, Objects, Decor };
        public CreateMode mode = CreateMode.None; //What are we creating right now?
        public bool placingItem = false; //Are we currently dragging and dropping something onto the map?

        Texture2D grid, pixel;
        Vector2 cameraPosition; //Used to move the camera around while paused.

        string savePath = ""; //Used for saving files. After initially saving a file we'll store the path so we can quickly save again without picking a path again.

        enum ObjectType
        {
            //IMPORTANT: Add the object types you want the editor to support here, then also add the constructor for object type in the addButton_Click function.
            //Also, make sure NumOfObjects is always the last item in this list so some other code below works properly.

            Enemy, PowerUp, NumOfObjects,
        };

        const string objectsNamespace = "GameEngine."; //IMPORTANT: Type the namespace here that all of your classes will be in! Make sure you spell it exactly and put a . at the end!

        public Editor(Game1 inputGame)
        {
            InitializeComponent();

            //Save a copy of our main game class, because we'll want to communicate with it a lot:
            game = inputGame;

            //While in editor mode we want the mouse to be visible:
            game.IsMouseVisible = true;

            //Save the winHandle of the game's window so we can bring focus to the game window and get it's location when needed:
            SDL.SDL_SysWMinfo info = new SDL.SDL_SysWMinfo();
            SDL.SDL_GetWindowWMInfo(game.Window.Handle, ref info);
            gameWinHandle = info.info.win.window;
            
            //Set the editor position to be a little to the right of our game window's position:
            RECT gameWindow = new RECT();
            GetWindowRect(gameWinHandle, ref gameWindow);
            Location = new System.Drawing.Point(gameWindow.Right + 11, gameWindow.Top);

            //Add the object types to the object list:
            PopulateObjectList();

            //Set the map height/width boxes on the form to the right values:
            mapHeight.Value = game.map.mapHeight;
            mapWidth.Value = game.map.mapWidth;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (objectTypes.SelectedIndex == -1)
                return;

            if (mode == CreateMode.Objects)
            {
                ObjectType selectedObject = (ObjectType)objectTypes.Items[objectTypes.SelectedIndex];

                //Create a new instance of what was selected:
                Type type = Type.GetType(objectsNamespace + selectedObject.ToString()); //First string should be the namespace the class is located in.
                GameObject newObject = (GameObject)Activator.CreateInstance(type);

                if (newObject == null)
                    return; //No valid object created.

                //Load the object and add it into our list:
                newObject.Load(game.Content);
                game.objects.Add(newObject);

                placingItem = true;
                FocusGameWindow();

                SetListBox(game.objects, false);
            }
            else if (mode == CreateMode.Decor)
            {
                Decor newDecor = new Decor();
                newDecor.imagePath = "decorplaceholder"; //Default image until we load the one we want.
                newDecor.Load(game.Content, newDecor.imagePath);
                game.map.decors.Add(newDecor);

                placingItem = true;
                SetListBox(game.map.decors, false);
                FocusGameWindow();   
            }
        }

        public void LoadTextures(ContentManager content)
        {
            grid = TextureLoader.Load("128grid", content);
            pixel = TextureLoader.Load("pixel", content);
        }

        public void Update(List<GameObject> objects, Map map)
        {
            Vector2 mousePosition = Input.MousePositionCamera();
            Point desiredIndex = map.GetTileIndex(mousePosition); //Grid location of where our mouse is.

            if ((Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftControl) || (Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.RightControl))) &&
                    mode != CreateMode.Walls)
            {
                //We held down control and clicked on something, auto select what we clicked in the list box:
                if (Input.MouseLeftClicked() && mode == CreateMode.Objects)
                {
                    for (int i = 0; i < objects.Count; i++)
                    {
                        if (objects[i].CheckCollision(new Rectangle(desiredIndex.X * map.tileSize, desiredIndex.Y * map.tileSize, 128, 128)))
                        {
                            listBox.SelectedIndex = i;
                            break;
                        }
                    }
                }
                else if (Input.MouseLeftClicked() && mode == CreateMode.Decor)
                {
                    for (int i = 0; i < map.decors.Count; i++)
                    {
                        if (map.decors[i].CheckCollision(new Rectangle(desiredIndex.X * map.tileSize, desiredIndex.Y * map.tileSize, 128, 128)))
                        {
                            listBox.SelectedIndex = i;
                            break;
                        }
                    }
                }
                else if (Input.KeyPressed(Microsoft.Xna.Framework.Input.Keys.C) && mode == CreateMode.Decor)
                    CopyDecor(); //Make a copy of the highlighted decor.
            }
            else if (Input.MouseLeftDown() == true && GameWindowFocused() == true)
            {
                //We clicked on something normally, without holding anything else down:
                if (mode == CreateMode.Walls)
                {
                    #region Add Walls
                    //Try to add a new wall here:
                    if (desiredIndex.X >= 0 && desiredIndex.X < map.mapWidth && desiredIndex.Y >= 0 && desiredIndex.Y < map.mapHeight)
                    {
                        Rectangle newWall = new Rectangle(desiredIndex.X * map.tileSize, desiredIndex.Y * map.tileSize, map.tileSize, map.tileSize);

                        if (map.CheckCollision(newWall) == Rectangle.Empty)
                        {
                            //Add new wall...                                                      
                            Rectangle oldWall = Rectangle.Empty;

                            //Check for nearby walls and see if we can combine them into one rectangle:
                            for (int i = 0; i < map.walls.Count; i++)
                            {
                                oldWall = map.walls[i].wall;

                                if (map.walls[i].wall.Intersects(new Rectangle(newWall.X + map.tileSize, newWall.Y, newWall.Width, newWall.Height))
                                    && map.walls[i].wall.Y == newWall.Y && map.walls[i].wall.Height == newWall.Height)
                                {
                                    //There is a wall to the right we can combine with:
                                    newWall = new Rectangle(oldWall.X - map.tileSize, oldWall.Y, oldWall.Width + map.tileSize, oldWall.Height);
                                    map.walls[i].wall = newWall;
                                    break;
                                }
                                else if (map.walls[i].wall.Intersects(new Rectangle(newWall.X - map.tileSize, newWall.Y, newWall.Width, newWall.Height))
                                    && map.walls[i].wall.Y == newWall.Y && map.walls[i].wall.Height == newWall.Height)
                                {
                                    //There is a wall to the left we can combine with:
                                    newWall = new Rectangle(oldWall.X, oldWall.Y, oldWall.Width + map.tileSize, oldWall.Height);
                                    map.walls[i].wall = newWall;
                                    break;
                                }
                                if (map.walls[i].wall.Intersects(new Rectangle(newWall.X, newWall.Y + map.tileSize, newWall.Width, newWall.Height))
                                    && map.walls[i].wall.X == newWall.X && map.walls[i].wall.Width == newWall.Width)
                                {
                                    //There is a wall below we can combine with:
                                    newWall = new Rectangle(oldWall.X, oldWall.Y - map.tileSize, oldWall.Width, oldWall.Height + map.tileSize);
                                    map.walls[i].wall = newWall;
                                    break;
                                }
                                else if (map.walls[i].wall.Intersects(new Rectangle(newWall.X, newWall.Y - map.tileSize, newWall.Width, newWall.Height))
                                    && map.walls[i].wall.X == newWall.X && map.walls[i].wall.Width == newWall.Width)
                                {
                                    //There is a wall above we can combine with:
                                    newWall = new Rectangle(oldWall.X, oldWall.Y, oldWall.Width, oldWall.Height + map.tileSize);
                                    map.walls[i].wall = newWall;
                                    break;
                                }

                                oldWall = Rectangle.Empty;
                            }

                            if (oldWall == Rectangle.Empty)
                                map.walls.Add(new Wall(newWall));

                            SetListBox(map.walls, false);
                        }
                        else
                        {
                            //Select the existing wall we clicked on in the editor:
                            for (int i = 0; i < map.walls.Count; i++)
                            {
                                if (map.walls[i].wall.Intersects(newWall))
                                {
                                    listBox.SelectedIndex = i;
                                    break;
                                }
                            }
                        }
                    }
                    #endregion
                }
                else if (mode == CreateMode.Objects && placingItem == true)
                {
                    game.objects[game.objects.Count - 1].startPosition = game.objects[game.objects.Count - 1].position;
                    game.objects[game.objects.Count - 1].Initialize();
                    SetListBox(game.objects, false);
                }
                else if (mode == CreateMode.Decor && placingItem == true)
                    SetListBox(game.map.decors, false);

                //No longer placing an item if we clicked left
                placingItem = false;
            }
            else if (Input.MouseRightDown() == true && GameWindowFocused() == true)
            {
                if (mode == CreateMode.Walls)
                {
                    #region Remove Walls
                    //Try to remove a wall here:
                    Rectangle input = new Rectangle((int)mousePosition.X, (int)mousePosition.Y, 1, 1);
                    for (int i = 0; i < game.map.walls.Count; i++)
                    {
                        if (game.map.walls[i].wall.Intersects(input) == true)
                        {
                            RemoveWall(i);
                            break;
                        }
                    }
                    #endregion
                }
            }
            else if (placingItem == true)
            {
                //Object is moving with the mouse, locking to grid coordinates:
                if (mode == CreateMode.Objects)
                    game.objects[game.objects.Count - 1].position = new Vector2(desiredIndex.X * map.tileSize, desiredIndex.Y * map.tileSize);
                else if (mode == CreateMode.Decor)
                    game.map.decors[game.map.decors.Count - 1].position = new Vector2(desiredIndex.X * map.tileSize, desiredIndex.Y * map.tileSize);
            }

            //Update editor movement if we're paused:
            if (paused.Checked == false && game.objects.Count > 0) //Constantly sync camera position with the player.
                cameraPosition = game.objects[0].position;
            else //we're paused, move the camera with arrow keys...
            {
                if (Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right))
                    cameraPosition.X += 6;
                else if (Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left))
                    cameraPosition.X -= 6;

                if (Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Down))
                    cameraPosition.Y += 6;
                else if (Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up))
                    cameraPosition.Y -= 6;

                Camera.Update(cameraPosition);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw a rectangle around the item we've selected in the editor:
            DrawSelectedItem(spriteBatch);

            //Draw the grid if checked:
            if (drawGridCheckBox.Checked == false)
                return;

            //Draw the editor grid:
            for (int x = 0; x < game.map.mapWidth; x++)
            {
                for (int y = 0; y < game.map.mapHeight; y++)
                    spriteBatch.Draw(grid, new Vector2(x, y) * game.map.tileSize, null, Color.Cyan, 0f, Vector2.Zero, 1f, SpriteEffects.None, .1f);
            }
        }

        #region Helpers

        #region DLL Functions
        //These are external functions that we're calling from a .dll file. They're used to give focus and get the location of the game window:
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd); //Gives a window focus / brings it to the foreground of all of the active windows.

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow(); //Returns the HWND of the window that is currently in focus / selected by the user. Could be the editor, the game, or another program that's open.

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect); //Gets position of the window passed in. We'll use this to find the Game window's position.
        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        #endregion

        private Texture2D LoadTextureStream(GraphicsDevice graphicsDevice, string filePath)
        {
            //This function loads a texture from stream and then changes blending options to make it draw like something we'd load from the ContentPipeline.
            //If all we do is load the texture from stream without changing blending, there's artifacts around the sprite.

            Texture2D file = null;
            Texture2D resultTexture;
            RenderTarget2D result = null;

            try
            {
                using (System.IO.Stream titleStream = TitleContainer.OpenStream(filePath))
                {
                    file = Texture2D.FromStream(graphicsDevice, titleStream);
                }
            }
            catch
            {
                throw new System.IO.FileLoadException("Cannot load '" + filePath + "' file!");
            }
            PresentationParameters pp = graphicsDevice.PresentationParameters;
            //Setup a render target to hold our final texture which will have premulitplied alpha values
            result = new RenderTarget2D(graphicsDevice, file.Width, file.Height, true, pp.BackBufferFormat, pp.DepthStencilFormat);

            graphicsDevice.SetRenderTarget(result);
            graphicsDevice.Clear(Color.Black);

            //Multiply each color by the source alpha, and write in just the color values into the final texture
            BlendState blendColor = new BlendState();
            blendColor.ColorWriteChannels = ColorWriteChannels.Red | ColorWriteChannels.Green | ColorWriteChannels.Blue;

            blendColor.AlphaDestinationBlend = Blend.Zero;
            blendColor.ColorDestinationBlend = Blend.Zero;

            blendColor.AlphaSourceBlend = Blend.SourceAlpha;
            blendColor.ColorSourceBlend = Blend.SourceAlpha;

            SpriteBatch spriteBatch = new SpriteBatch(graphicsDevice);
            spriteBatch.Begin(SpriteSortMode.Immediate, blendColor);
            spriteBatch.Draw(file, file.Bounds, Color.White);
            spriteBatch.End();

            //Now copy over the alpha values from the PNG source texture to the final one, without multiplying them
            BlendState blendAlpha = new BlendState();
            blendAlpha.ColorWriteChannels = ColorWriteChannels.Alpha;

            blendAlpha.AlphaDestinationBlend = Blend.Zero;
            blendAlpha.ColorDestinationBlend = Blend.Zero;

            blendAlpha.AlphaSourceBlend = Blend.One;
            blendAlpha.ColorSourceBlend = Blend.One;

            spriteBatch.Begin(SpriteSortMode.Immediate, blendAlpha);
            spriteBatch.Draw(file, file.Bounds, Color.White);
            spriteBatch.End();

            //Release the GPU back to drawing to the screen
            graphicsDevice.SetRenderTarget(null);

            resultTexture = new Texture2D(graphicsDevice, result.Width, result.Height);
            Color[] data = new Color[result.Height * result.Width];
            Color[] textureColor = new Color[result.Height * result.Width];

            result.GetData<Color>(textureColor);

            for (int i = 0; i < result.Height; i++)
            {
                for (int j = 0; j < result.Width; j++)
                {
                    data[j + i * result.Width] = textureColor[j + i * result.Width];
                }
            }

            resultTexture.SetData(data);

            return resultTexture;
        }

        private void DrawSelectedItem(SpriteBatch spriteBatch)
        {
            if (drawSelected.Checked == false)
                return;

            //Draw a slight box around whatever we're currently editing:
            if (mode == CreateMode.Walls)
            {
                if (game.map.walls.Count == 0 || listBox.SelectedIndex >= game.map.walls.Count)
                    return;

                Wall selectedWall = game.map.walls[listBox.SelectedIndex];
                spriteBatch.Draw(pixel, new Vector2((int)selectedWall.wall.X, (int)selectedWall.wall.Y), selectedWall.wall, Color.SkyBlue, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0);
            }
            else if (mode == CreateMode.Objects)
            {
                if (game.objects.Count == 0 || listBox.SelectedIndex >= game.objects.Count)
                    return;

                GameObject selectedObject = game.objects[listBox.SelectedIndex];
                spriteBatch.Draw(pixel, new Vector2((int)selectedObject.BoundingBox.X, (int)selectedObject.BoundingBox.Y), selectedObject.BoundingBox, new Color(80, 80, 100, 80), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0);
            }
            else if (mode == CreateMode.Decor)
            {
                if (game.map.decors.Count == 0 || listBox.SelectedIndex >= game.map.decors.Count)
                    return;

                Decor selectedDecor = game.map.decors[listBox.SelectedIndex];
                spriteBatch.Draw(pixel, new Vector2((int)selectedDecor.BoundingBox.X, (int)selectedDecor.BoundingBox.Y), selectedDecor.BoundingBox, new Color(80, 80, 100, 80), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0);
            }
        }

        private void CopyDecor()
        {
            if (listBox.SelectedIndex == -1)
                return;

            //Make a new piece of decor exactly like the one we have selected:
            Decor selectedDecor = (Decor)game.map.decors[listBox.SelectedIndex];
            Decor newDecor = new Decor(selectedDecor.position, selectedDecor.imagePath, selectedDecor.layerDepth);

            //Load it:
            newDecor.Load(game.Content, newDecor.imagePath);

            //Add to our list:
            game.map.decors.Add(newDecor);
            SetListBox(game.map.decors, false);
        }

        public void RemoveWall(int index)
        {
            int bookmarkIndex = listBox.SelectedIndex;
            game.map.walls.RemoveAt(index);

            SetListBox(game.map.walls, false);
        }

        private void ResetGame()
        {
            //Reset objects to their starting positions:
            for (int i = 0; i < game.objects.Count; i++)
            {
                game.objects[i].Initialize();
                game.objects[i].SetToDefaultPosition();
            }

            //Reset the decor:
            for (int i = 0; i < game.map.decors.Count; i++)
                game.map.decors[i].Initialize();
        }

        public void PopulateObjectList()
        {
            //Add all the supported object types to our list so we can add them through the editor:
            for (int i = 0; i < (int)ObjectType.NumOfObjects; i++)
                objectTypes.Items.Add((ObjectType)i);

            objectTypes.SelectedIndex = 0;
        }

        private void ResetEditorList()
        {
            //Put us in none mode and set the list again (used when starting a new level or loading a new one):
            objectsRadioButton.Checked = decorRadioButton.Checked = wallsRadioButton.Checked = false;
            noneRadioButton.Checked = true;
            List<int> nothing = new List<int>();
            SetListBox(nothing, true);
            FocusGameWindow();
        }

        private void LoadLevelContent()
        {
            //Load decor images:
            for (int i = 0; i < game.map.decors.Count; i++)
                game.map.decors[i].Load(game.Content, game.map.decors[i].imagePath);

            //Load objects:
            for (int i = 0; i < game.objects.Count; i++)
            {
                game.objects[i].Initialize();
                game.objects[i].Load(game.Content);
            }
        }
        #endregion

        public void SetListBox<T>(List<T> inputList, bool highlightFirstInList)
        {
            //Updates the list box with a list of whatever we're editing at the moment (walls, objects, etc.)
            listBox.DataSource = null;
            listBox.DataSource = inputList;

            if (highlightFirstInList == true && inputList != null && inputList.Count > 0)
                listBox.SelectedIndex = listBox.TopIndex = 0; //Start us on the first entry in the list, if there are entries
            else if (highlightFirstInList == true && inputList != null)
                listBox.SelectedIndex = listBox.TopIndex = -1; //Nothing in the list right now.
            else if (listBox.SelectedIndex < 0 && listBox.Items.Count > 0)
                listBox.SelectedIndex = 0;
            else
                listBox.SelectedIndex = listBox.Items.Count - 1;

            SetDisplayMember();
        }

        private void SetDisplayMember()
        {
            //Set the display member (what specific information gets shown in the listbox) depending for whatever mode we're in:
            if (mode == CreateMode.Walls)
                listBox.DisplayMember = "EditorWall";
            else if (mode == CreateMode.Objects)
            {
                //I'm cool with what it shows now, but if you want you can change it!
            }
            else if (mode == CreateMode.Decor)
                listBox.DisplayMember = "Name";
        }

        public void RefreshListBox<T>(List<T> inputList)
        {
            //Called when we remove something from the list and need to refresh the listbox:
            if (listBox.SelectedIndex - 1 >= 0)
                listBox.SelectedIndex--; //Move back one cause we just erased the one we're highlighting.
            else
                listBox.SelectedIndex = 0; //Only one item left in list, or nothing in list.

            placingItem = false;

            int bookmarkIndex = listBox.SelectedIndex;
            string displayMember = "";
            
                if (mode == CreateMode.Walls)
                {
                    if (bookmarkIndex == -1 && game.map.walls.Count > 0)
                        bookmarkIndex = 0;
                }
                else if (mode == CreateMode.Objects)
                {
                    if (bookmarkIndex == -1 && game.objects.Count > 0)
                        bookmarkIndex = 0;
                }
                else if (mode == CreateMode.Decor)
                {
                    if (bookmarkIndex == -1 && game.map.decors.Count > 0)
                        bookmarkIndex = 0;
                }

            int bookmarkTopIndex = listBox.TopIndex;

            listBox.DataSource = null;
            listBox.DataSource = inputList;
            listBox.DisplayMember = displayMember;
            if (listBox.DataSource != null && inputList.Count > 0)
            {
                listBox.SelectedIndex = bookmarkIndex;
                listBox.TopIndex = bookmarkTopIndex;
            }

            SetDisplayMember();
        }

        private void wallsRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (wallsRadioButton.Checked == true)
            {
                mode = CreateMode.Walls;
                SetListBox(game.map.walls, true);

                //Turn on the forms we need for this create mode:
                height.Enabled = width.Enabled = true;
            }
        }

        private void objectsRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (objectsRadioButton.Checked == true)
            {
                mode = CreateMode.Objects;
                SetListBox(game.objects, true);

                //Turn on the forms we need for this create mode:
                objectTypes.Visible = true;
                height.Enabled = width.Enabled = false;
            }
            else
                objectTypes.Visible = false;
        }

        private void decorRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (decorRadioButton.Checked == true)
            {
                mode = CreateMode.Decor;
                SetListBox(game.map.decors, true);

                //Turn on the forms we need for this create mode:
                height.Enabled = width.Enabled = false;
                imagePath.Visible = imagePathLabel.Visible = loadImageButton.Visible = layerDepthLabel.Visible = layerDepth.Visible = decorSourceXLabel.Visible = decorSourceX.Visible =
                    decorSourceYLabel.Visible = decorSourceY.Visible = decorSourceWidthLabel.Visible = decorSourceWidth.Visible = decorSourceHeightLabel.Visible = decorSourceHeight.Visible =
                    sourceRectangleLabel.Visible = true;
            }
            else
                imagePath.Visible = imagePathLabel.Visible = loadImageButton.Visible = layerDepthLabel.Visible = layerDepth.Visible = decorSourceXLabel.Visible = decorSourceX.Visible = 
                    decorSourceYLabel.Visible = decorSourceY.Visible = decorSourceWidthLabel.Visible = decorSourceWidth.Visible = decorSourceHeightLabel.Visible = decorSourceHeight.Visible =
                    sourceRectangleLabel.Visible = false;
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex == -1)
                return; //Nothing valid is selected.

            int savedIndex = listBox.SelectedIndex; //What index are we about to remove? 

            if (mode == CreateMode.Walls)
            {
                game.map.walls.RemoveAt(listBox.SelectedIndex); //Remove the item selected in the listbox.
                RefreshListBox(game.map.walls);
            }
            else if (mode == CreateMode.Objects && game.objects[listBox.SelectedIndex] is Player == false) //Don't remove the player!
            {
                game.objects.RemoveAt(listBox.SelectedIndex);
                RefreshListBox(game.objects);
            }
            else if (mode == CreateMode.Decor)
            {
                game.map.decors.RemoveAt(listBox.SelectedIndex);
                RefreshListBox(game.map.decors);
            }

            //We just removed an item, no longer possible to place anything:
            placingItem = false;
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex == -1)
                return;

            //Here we need to set the form items that we're using with the information of the item we've selected in the listbox:
            if (mode == CreateMode.Walls)
            {
                Rectangle selectedWall = game.map.walls[listBox.SelectedIndex].wall;
                xPosition.Value = selectedWall.X;
                yPosition.Value = selectedWall.Y;
                height.Value = selectedWall.Height;
                width.Value = selectedWall.Width;
            }
            else if (mode == CreateMode.Objects)
            {
                GameObject selectedObject = game.objects[listBox.SelectedIndex];
                xPosition.Value = (int)selectedObject.startPosition.X;
                yPosition.Value = (int)selectedObject.startPosition.Y;
            }
            else if (mode == CreateMode.Decor)
            {
                Decor selectedDecor = game.map.decors[listBox.SelectedIndex];
                xPosition.Value = (decimal)selectedDecor.position.X;
                yPosition.Value = (decimal)selectedDecor.position.Y;
                layerDepth.Value = (decimal)selectedDecor.layerDepth;
                imagePath.Text = selectedDecor.imagePath;
                decorSourceX.Value = (decimal)selectedDecor.sourceRect.X;
                decorSourceY.Value = (decimal)selectedDecor.sourceRect.Y;
                decorSourceWidth.Value = (decimal)selectedDecor.sourceRect.Width;
                decorSourceHeight.Value = (decimal)selectedDecor.sourceRect.Height;
            }
        }

        private void xPosition_ValueChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex == -1)
                return;

            if (mode == CreateMode.Walls)
            {
                Rectangle selectedWall = game.map.walls[listBox.SelectedIndex].wall;
                selectedWall.X = (int)xPosition.Value;
                game.map.walls[listBox.SelectedIndex].wall = selectedWall;
            }
            else if (mode == CreateMode.Objects)
                game.objects[listBox.SelectedIndex].startPosition.X = (float)xPosition.Value;
            else if (mode == CreateMode.Decor)
                game.map.decors[listBox.SelectedIndex].position.X = (float)xPosition.Value;
        }

        private void yPosition_ValueChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex == -1)
                return;

            if (mode == CreateMode.Walls)
            {
                Rectangle selectedWall = game.map.walls[listBox.SelectedIndex].wall;
                selectedWall.Y = (int)yPosition.Value;
                game.map.walls[listBox.SelectedIndex].wall = selectedWall;
            }
            else if (mode == CreateMode.Objects)
                game.objects[listBox.SelectedIndex].startPosition.Y = (float)yPosition.Value;
            else if (mode == CreateMode.Decor)
                game.map.decors[listBox.SelectedIndex].position.Y = (float)yPosition.Value;
        }

        private void width_ValueChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex == -1)
                return;

            if (mode == CreateMode.Walls)
            {
                Rectangle selectedWall = game.map.walls[listBox.SelectedIndex].wall;
                selectedWall.Width = (int)width.Value;
                game.map.walls[listBox.SelectedIndex].wall = selectedWall;
            }
        }

        private void height_ValueChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex == -1)
                return;

            if (mode == CreateMode.Walls)
            {
                Rectangle selectedWall = game.map.walls[listBox.SelectedIndex].wall;
                selectedWall.Height = (int)height.Value;
                game.map.walls[listBox.SelectedIndex].wall = selectedWall;
            }
        }

        private void layerDepth_ValueChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex == -1)
                return;

            game.map.decors[listBox.SelectedIndex].layerDepth = (float)layerDepth.Value;
        }

        private void loadImageButton_Click(object sender, EventArgs e) //Browse for an image and then load it in as a decor image...
        {
            if (listBox.SelectedIndex == -1)
                return;

            //Open file dialog:
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //Only allow png files to be selected, and only one file can be picked (no multiselecting):
            openFileDialog1.Filter = "PNG (.png)|*.png";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = false;

            //If the open file dialog returns an OK result, we can continue with adding the image that was picked:
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //If an error occurs inside of a try bracket the program will immedietely go to whatever is inside the catch brackets to avoid crashing.
                //Try catch brackets are very expensive to the computer so should only be used at uncertain times like this where we're opening a file the user picked:
                try
                {
                    //Check to see if a folder called BackupTextures exists by our .exe, if not create it:
                    if (Directory.Exists("BackupTextures") == false)
                        Directory.CreateDirectory("BackupTextures");

                    //Copy the file to the backup directory (useful in case the file gets moved, makes it easier to access later too):
                    File.Copy(openFileDialog1.FileName, "BackupTextures\\" + openFileDialog1.SafeFileName, true);

                    //Create a new Texture2D from the path the player selected:
                    Texture2D newImage = LoadTextureStream(game.GraphicsDevice, "BackupTextures\\" + openFileDialog1.SafeFileName);

                    //Get the path to the image the user selected (minus the .png extension at the end):
                    string fileName = Path.GetFileNameWithoutExtension(openFileDialog1.SafeFileName);

                    //Update the decor we're editing with the newly loaded image and updated file path:
                    game.map.decors[listBox.SelectedIndex].SetImage(newImage, fileName);

                    //Refresh the listbox and reset our selected index to the decor we had selected before:
                    SetListBox(game.map.decors, false);
                    FocusGameWindow();
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Error Loading Image: " + exception.Message);
                }
            }
        }

        private void menuStrip_MouseEnter(object sender, EventArgs e)
        {
            //As soon as the mouse enters into the menu strip, we should bring focus on it in case the game window is in focus and we're trying to 
            //click File -> Open or something. Without this we'd have to click on File two times and that's kind of annoying
            Focus();
        }

        private void mapWidth_ValueChanged(object sender, EventArgs e)
        {
            if (mapWidth.Value > mapWidth.Maximum)
                mapWidth.Value = mapWidth.Maximum;

            game.map.mapWidth = (int)mapWidth.Value;
        }

        private void mapHeight_ValueChanged(object sender, EventArgs e)
        {
            if (mapHeight.Value > mapHeight.Maximum)
                mapHeight.Value = mapHeight.Maximum;

            game.map.mapHeight = (int)mapHeight.Value;
        }

        private void resetNPC_Click(object sender, EventArgs e)
        {
            ResetGame();
            FocusGameWindow();
        }

        private void paused_CheckedChanged(object sender, EventArgs e)
        {
            FocusGameWindow();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Clear the level:
            game.objects.Clear();
            game.map.walls.Clear();
            game.map.decors.Clear();

            //Add one initial player object:
            game.objects.Add(new Player(Vector2.Zero));

            //Set default editor values:
            mapWidth.Value = game.map.mapWidth = 30;
            mapHeight.Value = game.map.mapHeight = 17;
            savePath = "";

            //Load all of our objects:
            for (int i = 0; i < game.objects.Count; i++)
            {
                game.objects[i].Load(game.Content);
                game.objects[i].Initialize();
            }

            //Start us in wall mode:
            ResetEditorList();
        }

        private void Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Close the game if we close the toolbox:
            game.Exit();
        }

        private void decorSourceX_ValueChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex == -1)
                return;

            game.map.decors[listBox.SelectedIndex].sourceRect.X = (int)decorSourceX.Value;
        }

        private void decorSourceY_ValueChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex == -1)
                return;

            game.map.decors[listBox.SelectedIndex].sourceRect.Y = (int)decorSourceY.Value;
        }

        private void decorSourceWidth_ValueChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex == -1)
                return;

            game.map.decors[listBox.SelectedIndex].sourceRect.Width = (int)decorSourceWidth.Value;
        }

        private void decorSourceHeight_ValueChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex == -1)
                return;

            game.map.decors[listBox.SelectedIndex].sourceRect.Height = (int)decorSourceHeight.Value;
        }

        private void noneRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (noneRadioButton.Checked == true)
            {
                mode = CreateMode.None;
                List<int> nothing = new List<int>();
                SetListBox(nothing, false);
            }
        }

        private void FocusGameWindow()
        {
            SetForegroundWindow(gameWinHandle);
        }

        private bool GameWindowFocused()
        {
            //GetForeGroundWindow() returns the HWND of the current window that is selected / focused.
            //If it is equal to our gameWinHandle, the game window is currently focused:
            return GetForegroundWindow() == gameWinHandle;
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenLevel();
        }

        private void SaveAs()
        {
            //Create the browsing window for the user to save:
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            //Clear our save path just in case we are re selecting a new place to save:
            savePath = "";

            //Filter specifies what types of files we want to save or open:
            saveFileDialog.Filter = "JORGE (.jorge)|*.jorge";

            //Try/catch blocks will stop the program from crashing if an error occurs. If an error occurs in the try block,
            //whatever is inside catch will be called. Useful to display errors, but very expensive to use CPU side.
            //Only use them in areas where uncertain things can happen like saving/loading files:
            try
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Store the path the user selected so we can easily save next time without repicking a save location:
                    savePath = saveFileDialog.FileName;

                    //Now save out the level:
                    SaveLevel();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error Saving: " + exception.Message + " " + exception.InnerException);
            }
        }

        private void Save()
        {
            //If no save path is selected... we need to pick one before saving:
            if (savePath == "")
            {
                SaveAs();
                return;
            }

            try
            {
                SaveLevel();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error Saving: " + exception.Message + " " + exception.InnerException);
            }
        }

        private void SaveLevel()
        {
            //Reset all the variables:
            ResetGame();

            //Make a levelData and store all of the information we want to save out inside it:
            LevelData levelData = new LevelData()
            {
                objects = game.objects,
                walls = game.map.walls,
                decor = game.map.decors,
                mapWidth = game.map.mapWidth,
                mapHeight = game.map.mapHeight,
            };

            //Finally, we can save out an XML file containing our level data:
            XmlHelper.Save(levelData, savePath);
        }

        public void OpenLevel()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //What file type do we want?
            openFileDialog1.Filter = "JORGE (.jorge)|*.jorge";

            //Don't allow the user to pick more than one file:
            openFileDialog1.Multiselect = false;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    LevelData levelData = XmlHelper.Load(openFileDialog1.FileName);

                    //Set all of our data to the new stuff we just loaded:
                    game.objects = levelData.objects;
                    game.map.walls = levelData.walls;
                    game.map.decors = levelData.decor;
                    mapWidth.Value = game.map.mapWidth = levelData.mapWidth;
                    mapHeight.Value = game.map.mapHeight = levelData.mapHeight;

                    //Call Load on all decor and objects so we load the images they need:
                    LoadLevelContent();

                    //Immeditely make the camera look at the first object in the list:
                    if (game.objects.Count > 0)
                        Camera.LookAt(game.objects[0].position);

                    //Put the editor back in wall mode and reset the list so it accurately is displaying the new things in the level:
                    ResetEditorList();

                    //Set our save path to nothing:
                    savePath = "";

                    //Bring the game window into focus:
                    FocusGameWindow();
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Error Loading: " + exception.Message + " " + exception.InnerException);
                }
            }
        }
    }
}
