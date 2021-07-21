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
    public class Animation //Individual animation (like "Run Left"), many of these usually make up an AnimationSet.
    {
        public string name;
        public List<int> animationOrder = new List<int>();
        public int speed;

        public Animation()
        {

        }

        public Animation(string inputName, int inputSpeed, List<int> inputAnimationOrder)
        {
            this.name = inputName;
            this.animationOrder = inputAnimationOrder;
            this.speed = inputSpeed;
        }
    }

    public class AnimationSet
    {
        public int width;   //Width of each frame
        public int height;  //Height of each frame
        public int gridX;   //How many frames are in the x direction
        public int gridY;   //How many frames are in the y direction
        public List<Animation> animationList = new List<Animation>();


        public AnimationSet()
        {

        }

        public AnimationSet(int inputWidth, int inputHeight, int inputGridX, int inputGridY)
        {
            this.width = inputWidth;
            this.height = inputHeight;
            this.gridX = inputGridX;
            this.gridY = inputGridY;
        }
    }

    public class AnimationData
    {
        public AnimationSet animation { get; set; }
        public string texturePath { get; set; }
    }
}
