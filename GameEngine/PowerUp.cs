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
    public class PowerUp : AnimatedObject
    {
        public PowerUp()
        {
        }

        public PowerUp(Vector2 inputPosition)
        {
            position = inputPosition;
        }

        public override void Initialize()
        {
            base.Initialize();
            collidable = false; //no queremos chocarnos con él
        }

        public override void Load(ContentManager content)
        {
            //Load our image/sprite sheet:
            image = TextureLoader.Load("orbsheet", content);

            //Load any animation stuff if this object animates:
            LoadAnimation("PowerUp.anm", content);
            ChangeAnimation(Animations.Idle); //Set our default animation.

            //Load stuff from our parent class:
            base.Load(content);

            //Customize the size of our bounding box for collisions:
            //Podemos cambiar el tamaño si queremos que se ajuste mejor a la bola
            boundingBoxOffset.X = 0;
            boundingBoxOffset.Y = 0;
            boundingBoxWidth = animationSet.width; //or use image.Width if it's not animated
            boundingBoxHeight = animationSet.height; //or use image.Height if it's not animated
        }

        public override void Update(List<GameObject> objects, Map map)
        {
            if (active == false)
            {
                return;
            }
            CheckPlayerCollision(objects, map);

            base.Update(objects, map);
        }

        private void CheckPlayerCollision(List<GameObject> objects, Map map)
        {
            if (AnimationIsNot(Animations.PickUp) && objects[0].CheckCollision(BoundingBox) == true)
            {
                Player.score++;
                ChangeAnimation(Animations.PickUp);
            }
        }
        protected override void UpdateAnimations()
        {
            if (currentAnimation == null)
                return; //Animation isn't loaded, so return.

            base.UpdateAnimations();

            //ADD YOUR ANIMATION LOGIC HERE AT THE BOTTOM!
            if (AnimationComplete() == true && GetAnimationName(Animations.PickUp) == currentAnimation.name)
            {
                active = false;
            }
        }
    }
}
