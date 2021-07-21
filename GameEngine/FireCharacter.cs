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
    public class FireCharacter : Character //Character that fires bullets
    {
        List<Bullet> bullets = new List<Bullet>();
        const int numOfBullets = 20;
        public FireCharacter()
        {
            
        }

        public override void Initialize()
        {
            if (bullets.Count == 0)
            {
                for (int i = 0; i < numOfBullets; i++)
                {
                    bullets.Add(new Bullet());
                }
            }
            base.Initialize();
        }

        public override void Load(ContentManager content)
        {
            for (int i = 0; i < numOfBullets; i++)
            {
                bullets[i].Load(content);
            }
            base.Load(content);
        }

        public override void Update(List<GameObject> objects, Map map)
        {
            for (int i = 0; i < numOfBullets; i++)
            {
                bullets[i].Update(objects, map);
            }
            base.Update(objects, map);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < numOfBullets; i++)
            {
                bullets[i].Draw(spriteBatch);
            }
            base.Draw(spriteBatch);
        }

        public void Fire()
        {
            for (int i = 0; i < numOfBullets; i++)
            {
                if(bullets[i].active == false)
                {
                    bullets[i].Fire(this, position, direction); //Las del character, si mira hacia la derecha pues dispara a la derecha (direccion)
                    break;
                }
            }
        }
    }
}
