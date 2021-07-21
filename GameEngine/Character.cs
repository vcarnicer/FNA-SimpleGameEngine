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
    public class Character : AnimatedObject
    {
        public Vector2 velocity;

        //Customize the feel of our movement:
        protected float decel = 1.2f; //The lower your decel, the slower you slow down
        protected float accel = .7f; //The lower your accel, the slower you take off
        protected float maxSpeed = 12f;

        const float gravity = 1f; //BUG: Se atasca en el suelo si el valor es menos de 1 ?!?! WHY
        const float jumpVelocity = 24f; //How much we jump.
        const float maxFallVelocity = 32f;

        protected bool jumping;
        public static bool applyGravity = true; //Static means it's always in memory so we can change this value in execution

        public override void Initialize()
        {
            velocity = Vector2.Zero;
            jumping = false;
            base.Initialize();
        }

        public override void Update(List<GameObject> objects, Map map)
        {
            UpdateMovement(objects, map);
            base.Update(objects, map);
        }

        //Important function that uses most methods defined here
        private void UpdateMovement(List<GameObject> objects, Map map)
        {
            //X Axis
            if (velocity.X != 0 && CheckCollisions(map, objects, true) == true)
            {
                velocity.X = 0;
            }

            position.X += velocity.X;

            //Y Axis
            if (velocity.Y != 0 && CheckCollisions(map, objects, false) == true)
            {
                velocity.Y = 0;
            }

            position.Y += velocity.Y;

            //Gravity
            if (applyGravity == true)
            {
                ApplyGravity(map);
            }

            //Tend to zero if nothing is added to velocity (no button presses)
            velocity.X = TendToZero(velocity.X, decel);
            if(applyGravity == false)
            {
                velocity.Y = TendToZero(velocity.Y, decel);
            }
        }

        private void ApplyGravity(Map map)
        {
            if (jumping == true || OnGround(map) == Rectangle.Empty)
            {
                velocity.Y += gravity;
            }

            if(velocity.Y > maxFallVelocity)
            {
                velocity.Y = maxFallVelocity;
            }
        }

        protected void MoveRight()
        {
            //We sum decel to compensate the TendToZero(Velocity.X, decel) on UpdateMovement 
            velocity.X += accel + decel;

            if(velocity.X > maxSpeed)
            {
                velocity.X = maxSpeed;
            }

            direction.X = 1;
        }
        protected void MoveLeft()
        {
            velocity.X -= accel + decel;

            if(velocity.X < -maxSpeed)
            {
                velocity.X = -maxSpeed;
            }

            direction.X = -1;
        }

        protected void MoveDown()
        {
            velocity.Y += accel + decel;

            if (velocity.Y > maxSpeed)
            {
                velocity.Y = maxSpeed;
            }

            direction.Y = 1;
        }
        protected void MoveUp()
        {
            velocity.Y -= accel + decel;

            if (velocity.Y < -maxSpeed)
            {
                velocity.Y = -maxSpeed;
            }

            direction.Y = -1;
        }

        protected bool Jump(Map map)
        {
            if(jumping == true)
            {
                return false;
            }

            if(velocity.Y == 0 && OnGround(map) != Rectangle.Empty)
            {
                velocity.Y -= jumpVelocity;
                jumping = true;
                return true;
            }

            return false;
        }
        protected virtual bool CheckCollisions(Map map, List<GameObject> objects, bool xAxis) //xAxis determines if calculating for xAxis or yAxis
        {
            Rectangle futureBoundingBox = BoundingBox;

            int maxX = (int)maxSpeed;
            int maxY = (int)maxSpeed;

            //For gravity, this is my own (bnktop) fix
            int maxYup = (int)jumpVelocity;
            int maxYdown = (int)maxFallVelocity;

            if(applyGravity == true)
            {
                maxY = (int)jumpVelocity;
            }

            if (xAxis == true && velocity.X != 0)
            {
                if(velocity.X > 0)
                {
                    futureBoundingBox.X += maxX;
                } else
                {
                    futureBoundingBox.X -= maxX;
                }
            } else if (applyGravity == false && xAxis == false && velocity.Y != 0)
            {
                if (velocity.Y > 0)
                {
                    futureBoundingBox.Y += maxY;
                }
                else
                {
                    futureBoundingBox.Y -= maxY;
                }
            } else if (applyGravity == true && xAxis == false && velocity.Y != gravity) //FOR GRAVITY: fixed it myself
            {
                if (velocity.Y > 0)
                {
                    futureBoundingBox.Y += maxYdown;
                }
                else
                {
                    futureBoundingBox.Y -= maxYup;
                }
            }

            //Check WALLS collision
            Rectangle wallCollision = map.CheckCollision(futureBoundingBox);

            //Extra code for gravity
            if (wallCollision != Rectangle.Empty)
            {    
                if (applyGravity == true)
                {
                    if (velocity.Y >= gravity && (futureBoundingBox.Bottom > wallCollision.Top - maxFallVelocity) && (futureBoundingBox.Bottom <= wallCollision.Top + velocity.Y))
                    {
                        //If the character is close to hitting the ground, lock his feet to the floor
                        //Has a buffer distance to prevent the character to get inside the floor
                        LandResponse(wallCollision, 0); //Down                        
                    }
                    if (velocity.Y < 0 && (futureBoundingBox.Top <= wallCollision.Bottom + jumpVelocity) && (futureBoundingBox.Top > wallCollision.Bottom + velocity.Y)){
                        LandResponse(wallCollision, 1); //Up
                        
                    }
                    if (velocity.X < 0 && (futureBoundingBox.Left <= wallCollision.Right + maxSpeed) && (futureBoundingBox.Left > wallCollision.Right + velocity.X))
                    {
                        LandResponse(wallCollision, 2); //Left
                        
                    }
                    if (velocity.X > 0 && (futureBoundingBox.Right > wallCollision.Left - maxSpeed) && (futureBoundingBox.Right <= wallCollision.Left + velocity.X))
                    {
                        LandResponse(wallCollision, 3); //Right
                    }
                    return true;
                }
            }

            //Check for object collisions:
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i] != this && objects[i].active == true && objects[i].collidable == true && objects[i].CheckCollision(futureBoundingBox) == true)
                {
                    return true;
                }
            }

            //If none of the checks against all entities are true, returns false, so there is no collision
            return false;
        }

        public void LandResponse(Rectangle wallCollision, int direction)
        {
            switch (direction)
            {
                case 0: //Land down
                    position.Y = wallCollision.Top - (boundingBoxHeight + boundingBoxOffset.Y);
                    velocity.Y = 0;
                    jumping = false;
                    break;
                case 1: //Hit up
                    position.Y = wallCollision.Bottom + boundingBoxOffset.Y;
                    velocity.Y = 0;
                    jumping = false;
                    break;
                case 2: //Hit left
                    position.X = wallCollision.Right + boundingBoxOffset.X;
                    velocity.X = 0;
                    break;
                case 3: //Hit right
                    position.X = wallCollision.Left - (boundingBoxWidth + boundingBoxOffset.X);
                    velocity.X = 0;
                    break;
            }
            
        }

        //Returns the walls that the character is on at this moment
        protected Rectangle OnGround(Map map)
        {
            Rectangle futureBoundingBox = new Rectangle((int)(position.X + boundingBoxOffset.X), (int)(position.Y + boundingBoxOffset.Y + (velocity.Y + gravity)), boundingBoxWidth, boundingBoxHeight);

            return map.CheckCollision(futureBoundingBox);
        }

        //Gradually moves values to zero
        protected float TendToZero(float val, float amount)
        {
            if (val > 0f && (val -= amount) < 0f) return 0f;
            if (val < 0f && (val += amount) > 0f) return 0f;
            return val;
        }
    }
}
