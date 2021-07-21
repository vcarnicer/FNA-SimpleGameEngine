using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace GameEngine
{
    public class Game1 : Game
    {
		GraphicsDeviceManager gdm;
		SpriteBatch spriteBatch;
		GameHUD gameHUD = new GameHUD();
		Editor editor;

		public List<GameObject> objects = new List<GameObject>(); //Stores all objects in the game

		public Map map = new Map();

		public Game1()
        {
			gdm = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			Resolution.Init(ref gdm);
			Resolution.SetVirtualResolution(1280, 720); //La resolución en la que nuestros assets y nuestro juego trabajan.
			Resolution.SetResolution(640, 360, false); //La resolución a la que se dibuja (tamaño ventana), el tercer parámetro es si FullScreen o no
		}

		protected override void Initialize()
		{
			/* This is a nice place to start up the engine, after
			 * loading configuration stuff in the constructor
			 */
#if DEBUG
			editor = new Editor(this);
			editor.Show();
#endif
			
			base.Initialize();
			Camera.Initialize();
			Global.Initialize(this);
		}

		protected override void LoadContent()
		{
			// Load textures, sounds, and so on in here...
			spriteBatch = new SpriteBatch(GraphicsDevice);

#if DEBUG
			editor.LoadTextures(Content);
#endif

			map.Load(Content);
			gameHUD.Load(Content);
			LoadLevel("nivelungo.jorge");
		}

		protected override void UnloadContent()
		{
			// Clean up after yourself!
			base.UnloadContent();
		}

		protected override void Update(GameTime gameTime)
		{			
			// Run game logic in here. Do NOT render anything here!

			Input.Update(); //For us to have the latest info on inputs (custom class from Utilities)			

			UpdateObjects();
			map.Update(objects);
			UpdateCamera();

#if DEBUG
			editor.Update(objects, map);
#endif

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			// Render stuff in here. Do NOT run game logic in here!
			// This clears the screen each frame as a background.
			//GraphicsDevice.Clear(Color.CornflowerBlue);

			Resolution.BeginDraw();

			spriteBatch.Begin(
                SpriteSortMode.BackToFront, //Back to Front draws the sprites based on their layer depth from 0 (closest) to 1 (furthest)
				BlendState.AlphaBlend,
                SamplerState.LinearClamp,
                DepthStencilState.Default,
                RasterizerState.CullNone,
                null,
                Camera.GetTransformMatrix());

#if DEBUG
			editor.Draw(spriteBatch);
#endif

			DrawObjects();
			map.DrawWalls(spriteBatch);
			spriteBatch.End();

			//Añadimos el HUD al haber acabado el anterior SpriteBatch porque dentro de HUD creamos uno nuevo independiente.
			gameHUD.Draw(spriteBatch);

			base.Draw(gameTime);
		}

		public void LoadLevel(string fileName)
        {
			Global.levelName = fileName;

			//Load the level data:
			LevelData levelData = XmlHelper.Load("Content\\Levels\\" + fileName);

			map.walls = levelData.walls;
			map.decors = levelData.decor;
			objects = levelData.objects;

			//objects.Add(new Player(new Vector2(640, 0)));

			//objects.Add(new Enemy(new Vector2(300, 522)));

			////Add walls:
			//map.walls.Add(new Wall(new Rectangle(256, 256, 256, 256)));
			//map.walls.Add(new Wall(new Rectangle(700, 480, 256, 2)));
			//map.walls.Add(new Wall(new Rectangle(0, 650, 1280, 128)));

			////Add decors:
			//map.decors.Add(new Decor(Vector2.Zero, "background", 1f));

			map.LoadMap(Content);
			LoadObjects();
        }
		public void LoadObjects()
        {
			for (int i = 0; i < objects.Count; i++)
            {
				objects[i].Initialize();
				objects[i].Load(Content);
            }
        }

		public void UpdateObjects()
        {
			for (int i = 0; i < objects.Count; i++)
            {
				objects[i].Update(objects, map);
            }
        }

		public void DrawObjects()
		{
			for (int i = 0; i < objects.Count; i++)
			{
				objects[i].Draw(spriteBatch);
			}

			for (int i = 0; i < map.decors.Count; i++)
			{
				map.decors[i].Draw(spriteBatch);
			}
		}

		private void UpdateCamera()
        {
			if(objects.Count == 0)
            {
				//Si no hay objetos no hay nada que seguir entonces nada.
				return;
            }

			Camera.Update(objects[0].position); //Hacemos que siga al primer objeto, que debería ser el jugador (me parece un poco yank pero bueno).
        }
	}
}
