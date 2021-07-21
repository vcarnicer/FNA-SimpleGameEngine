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
    public class GameObject
    {
        protected Texture2D image;
        public Vector2 position;
        public Color drawColor = Color.White;
        public float scale = 1f, rotation = 0f;
        public float layerDepth = .5f;
        public bool active = true;
        protected Vector2 center;

        //For collisions
        public bool collidable = true;
        protected int boundingBoxWidth, boundingBoxHeight;
        protected Vector2 boundingBoxOffset;
        Texture2D boundingBoxImage;
        const bool drawBoundingBoxes = false;
        protected Vector2 direction = new Vector2(1, 0); //Defaults gameObjects to be looking to the right

        public Vector2 startPosition = new Vector2(-1, -1); //Inicializamos esta posición a -1 para saber que no es válida.

        //As a property because position can change each frame so we need to calculate it, BoundingBox is recalculated and up to date this way
        public Rectangle BoundingBox 
        { 
            get {
                return new Rectangle((int)(position.X + boundingBoxOffset.X), (int)(position.Y + boundingBoxOffset.Y), boundingBoxWidth, boundingBoxHeight);
            }
        }

        public GameObject()
        {

        }

        public virtual void Initialize()
        {
            if(startPosition == new Vector2(-1, -1))
            {
                startPosition = position;
            }
        }

        public virtual void SetToDefaultPosition()
        {
            position = startPosition;
        }

        public virtual void Load(ContentManager content)
        {
            boundingBoxImage = TextureLoader.Load("pixel", content);

            CalculateCenter();

            //Default bounding boxes but they can be overriden on each child
            if (image != null){
                boundingBoxWidth = image.Width;
                boundingBoxHeight = image.Height;
            }
        }

        public virtual void Update(List<GameObject> objects, Map map)
        {

        }

        public virtual bool CheckCollision(Rectangle input)
        {
            return BoundingBox.Intersects(input);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (boundingBoxImage != null && drawBoundingBoxes == true && active == true){
                spriteBatch.Draw(boundingBoxImage, new Vector2(BoundingBox.X, BoundingBox.Y), BoundingBox, new Color(120, 120, 120, 120), 0f, Vector2.Zero, 1f, SpriteEffects.None, layerDepth);
            }

            if (image != null && active == true)
            {
                spriteBatch.Draw(image, position, null, drawColor, rotation, Vector2.Zero, scale, SpriteEffects.None, layerDepth);
            }            
        }
        public virtual void BulletResponse()
        {

        }

        private void CalculateCenter()
        {
            if(image != null)
            {
                center.X = image.Width / 2;
                center.Y = image.Height / 2;
                return;
            }

            throw new Exception();            
        }
    }
}
