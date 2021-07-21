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
    public class Bullet : GameObject
    {
        const float speed = 12f; //How fast the bullet will move. Si es demasiado rápida puede que traspase paredes.
        Character owner;

        int destroyTimer;
        const int maxTimer = 180; //180 frames = 3 s

        public Bullet()
        {
            active = false;
        }
        public override void Load(ContentManager content)
        {
            image = TextureLoader.Load("bullet", content);
            base.Load(content);
        }
        public override void Update(List<GameObject> objects, Map map)
        {
            if(active == false)
            {
                return;
            }

            //Update movement
            position += direction * speed;

            CheckCollisions(objects, map);

            //Update destroyTimer
            destroyTimer--; //Cada frame perderá 1.
            if (destroyTimer <= 0 && active == true)
            {
                Destroy();
            }

            base.Update(objects, map);
        }

        private void CheckCollisions(List<GameObject> objects, Map map)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i].active == true && objects[i] != owner && objects[i].CheckCollision(BoundingBox) == true)
                {
                    Destroy();
                    objects[i].BulletResponse();
                    return;
                }
            }

            if(map.CheckCollision(BoundingBox) != Rectangle.Empty)
            {
                Destroy();
            }
        }
        public void Fire(Character inputOwner, Vector2 inputPosition, Vector2 inputDirection)
        {
            //Dispara esta bala
            owner = inputOwner;
            position = inputPosition;
            direction = inputDirection;
            active = true;
            destroyTimer = maxTimer;
        }

        public void Destroy()
        {
            active = false;
        }
    }
}
