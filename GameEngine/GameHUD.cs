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
    public class GameHUD
    {
        SpriteFont font;

        public GameHUD()
        {
        }

        public void Load(ContentManager content)
        {
            font = content.Load<SpriteFont>("Fonts\\Arial");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Resolution.getTransformationMatrix()); //Back to Front draws the sprites based on their layer depth from 0 (closest) to 1 (furthest)
            spriteBatch.DrawString(font, "Scorsese: " + Player.score.ToString(), Vector2.Zero, Color.White);
            spriteBatch.End();
        }
    }
}
