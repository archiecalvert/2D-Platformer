using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Quase.Classes.Engine;
using Quase.Classes.GameComponents;

namespace Quase.Classes.Components.Player_Classes
{
    public class PlayerAnimator
    {
        static bool changeFrame = false;
        static int frame = 0;
        static float timeBetweenFrames = 1f;
        static float animationSpeed = timeBetweenFrames;
        static int increment = 1;
        public static int frameWidth = 64;
        static void WalkAnimataion()
        {
            timeBetweenFrames = 0.065f;
            if (frame == 0 && changeFrame)
            {
                Objects.Player.SourceRectangle = new Rectangle(0, frameWidth, frameWidth, frameWidth); 
                increment = 1;
            }
            else if (frame == 1 && changeFrame)
            {
                Objects.Player.SourceRectangle = new Rectangle(frameWidth, frameWidth, frameWidth, frameWidth);
            }
            else if (frame == 2 && changeFrame)
            {
                Objects.Player.SourceRectangle = new Rectangle(frameWidth * 2, frameWidth, frameWidth, frameWidth);
            }
            else if (frame == 3 && changeFrame)
            {
                Objects.Player.SourceRectangle = new Rectangle(frameWidth * 3, frameWidth, frameWidth, frameWidth);
            }
            else if (frame == 4 && changeFrame)
            {
                Objects.Player.SourceRectangle = new Rectangle(frameWidth * 4, frameWidth, frameWidth, frameWidth);

            }
            else if (frame == 5 && changeFrame)
            {
                Objects.Player.SourceRectangle = new Rectangle(frameWidth * 5, frameWidth, frameWidth, frameWidth);
            }
            else if (frame == 6 && changeFrame)
            {
                Objects.Player.SourceRectangle = new Rectangle(frameWidth * 6, frameWidth, frameWidth, frameWidth);
            }
            else if (frame == 7 && changeFrame)
            {
                Objects.Player.SourceRectangle = new Rectangle(frameWidth * 7, frameWidth, frameWidth, frameWidth);
            }
            else if (frame == 8 && changeFrame)
            {
                Objects.Player.SourceRectangle = new Rectangle(frameWidth * 8, frameWidth, frameWidth, frameWidth);
                frame = 0;
            }
        }
        static void IdleAnimataion()
        {
            if (frame > 3)
            {
                frame = 2;
            }
            timeBetweenFrames = 0.25F;
            if (frame == 0 && changeFrame)
            {
                Objects.Player.SourceRectangle = new Rectangle(0, 0, frameWidth, frameWidth); //sets the current frame drawn to the first frame
                increment = 1;
            }
            else if (frame == 1 && changeFrame)
            {
                Objects.Player.SourceRectangle = new Rectangle(frameWidth, 0, frameWidth, frameWidth); //sets the current frame drawn to the second frame
            }
            else if (frame == 2 && changeFrame)
            {
                Objects.Player.SourceRectangle = new Rectangle(frameWidth * 2, 0, frameWidth, frameWidth); //sets the current frame drawn to the second frame
            }
            else if (frame == 3 && changeFrame)
            {
                Objects.Player.SourceRectangle = new Rectangle(frameWidth * 3, 0, frameWidth, frameWidth); //sets the current frame drawn to the second frame
                frame = 0;
            }
            if (frame > 3)
            {
                frame = 2;
            }
        }
        static void JumpAnimation()
        {
            if (Objects.Player.VelocityY > 4)
            {
                Objects.Player.SourceRectangle = new Rectangle(0, frameWidth * 2, frameWidth, frameWidth);
            }
            else if (Objects.Player.VelocityY <= 4 && Objects.Player.VelocityY >= 3)
            {
                Objects.Player.SourceRectangle = new Rectangle(frameWidth, frameWidth * 2, frameWidth, frameWidth);
            }
            else if (Objects.Player.VelocityY < 3 && Objects.Player.VelocityY > -3)
            {
                Objects.Player.SourceRectangle = new Rectangle(frameWidth * 2, frameWidth * 2, frameWidth, frameWidth);
            }
            else if (Objects.Player.VelocityY < -3)
            {
                Objects.Player.SourceRectangle = new Rectangle(frameWidth * 2, frameWidth * 2, frameWidth, frameWidth);
            }
        }
        public static void Update()
        {

            if (!Input.isMoving && Objects.Player.touchingGround)
            {
                IdleAnimataion();
            }
            if(Input.isMoving && Objects.Player.touchingGround)
            {
                WalkAnimataion();
            }
            if (!Objects.Player.touchingGround)
            {
                JumpAnimation();
            }
            UpdateFrame();
        }
        static void UpdateFrame()
        {
            if (changeFrame && Objects.Player.touchingGround)
            {
                frame += increment;
            }

            if (animationSpeed > 0)
            {
                changeFrame = false;
                animationSpeed -= Quase.deltaTime/144;
            }
            else
            {
                animationSpeed = timeBetweenFrames;
                changeFrame = true;
            }
        }
    }
}
