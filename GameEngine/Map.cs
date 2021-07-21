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
    public class Map
    {
        public List<Wall> walls = new List<Wall>();
        public List<Decor> decors = new List<Decor>();
        Texture2D wallImage;

        public int mapWidth = 25;
        public int mapHeight = 18;
        public int tileSize = 128;

        public void LoadMap(ContentManager content)
        {
            for (int i = 0; i < decors.Count; i++)
            {
                decors[i].Load(content, decors[i].imagePath);
            }
        }

        public void Load(ContentManager content)
        {
            wallImage = TextureLoader.Load("pixel", content);
        }

        public void Update(List<GameObject> objects)
        {
            for (int i = 0; i < decors.Count; i++)
            {
                decors[i].Update(objects, this);
            }
        }

        public Rectangle CheckCollision(Rectangle input)
        {
            for (int i = 0; i < walls.Count; i++)
            {
                if (walls[i] != null && walls[i].wall.Intersects(input) == true)
                {
                    return walls[i].wall;
                }
            }

            return Rectangle.Empty;
        }

        public void DrawWalls(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < walls.Count; i++)
            {
                if (walls[i] != null && walls[i].active == true)
                {
                    spriteBatch.Draw(wallImage, new Vector2(walls[i].wall.X, walls[i].wall.Y), walls[i].wall, Color.Red, 0f, Vector2.Zero, 1f, SpriteEffects.None, .7f);
                }
            }
        }

        public Point GetTileIndex(Vector2 inputPosition)
        {
            if(inputPosition == new Vector2(-1, -1))
            {
                return new Point(-1, -1);
            }

            return new Point((int)inputPosition.X / tileSize, (int)inputPosition.Y / tileSize);
        }
    }

    public class Wall
    {
        public Rectangle wall;
        public bool active = true;

        public Wall()
        {

        }

        public Wall(Rectangle inputRectangle)
        {
            wall = inputRectangle;
        }
    }

    public class Decor : GameObject
    {
        public string imagePath;
        public Rectangle sourceRect;
        
        public string Name { get { return imagePath; } }
        public Decor()
        {
            collidable = false;
        }

        public Decor(Vector2 inputPosition, string inputImagePath, float inputDepth)
        {
            position = inputPosition;
            imagePath = inputImagePath;
            layerDepth = inputDepth;
            active = true;
            collidable = false;
        }

        public virtual void Load(ContentManager content, string assetName)
        {
            image = TextureLoader.Load(assetName, content);
            image.Name = assetName;

            boundingBoxWidth = image.Width;
            boundingBoxHeight = image.Height;

            if(sourceRect == Rectangle.Empty)
            {
                sourceRect = new Rectangle(0, 0, image.Width, image.Height);
            }
        }

        public void SetImage(Texture2D input, string newPath)
        {
            image = input;
            imagePath = newPath;
            boundingBoxWidth = sourceRect.Width = image.Width; //a sourceRect se le asigna image y a boundingBox después sourceRect
            boundingBoxHeight = sourceRect.Height = image.Height;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (image != null && active == true)
            {
                spriteBatch.Draw(image, position, sourceRect, drawColor, rotation, Vector2.Zero, scale, SpriteEffects.None, layerDepth);
                //Especificar aquí sourceRect nos ayudará a elegir qué cacho de imagen queremos dibujar
            }
        }

    }
}
