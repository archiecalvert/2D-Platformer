using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Quase.Classes.Character_Abilities;
using Quase.Classes.GameComponents;

namespace Quase.Classes.Engine
{
    public class Collisions
    {
        public static bool testCol;
        public static float MoveX(float amount)
        {
            bool Collided = false;
            float checkAhead;
            if (amount > 0)
            {
                checkAhead = Objects.Player.PlayerHitbox.Box.X + Objects.Player.PlayerHitbox.Box.Width + amount;
            }
            else
            {
                checkAhead = Objects.Player.PlayerHitbox.Box.X + amount;
            }
            Rectangle newRect = new Rectangle((int)checkAhead, Objects.Player.PlayerHitbox.Box.Y + 5, 1, Objects.Player.PlayerHitbox.Box.Height - 10);
            Rectangle Collider = new Rectangle();
            foreach (Rectangle item in Objects.Colliders)
            {
                if(newRect.X < item.X + item.Width &&
                    newRect.X + newRect.Width > item.X &&
                    newRect.Y < item.Y + item.Height &&
                    newRect.Y + newRect.Height > item.Y)
                {
                    Collided = true;
                    Collider = item;
                }
            }
            Objects.EntityColliders.Add(newRect);
            if (Collided)
            {
                float dist = 0;
                
                if(amount > 0)
                {
                    dist = (1 + Collider.X - (newRect.X + newRect.Width));
                }
                else
                {
                    dist = -(newRect.X - (Collider.X + Collider.Width) + 1);
                }
                Objects.Player.VelocityX = 0;
                return dist/64;
            }
            else
            {
                return (amount / 64) * Quase.deltaTime;
            }
        }
        public static float MoveY(float amount)
        {
            bool Collided = false;
            float checkAhead;
            if (amount > 0)
            {
                checkAhead = Objects.Player.PlayerHitbox.Box.Y - amount;
            }
            else
            {
                checkAhead = Objects.Player.PlayerHitbox.Box.Y + Objects.Player.PlayerHitbox.Box.Height - amount;
            }

            Rectangle newRect = new Rectangle(Objects.Player.PlayerHitbox.Box.X + 3, (int)checkAhead, Objects.Player.PlayerHitbox.Box.Width - 6, 1);
            Rectangle Collider = new Rectangle();
            foreach (Rectangle item in Objects.Colliders)
            {
                if(newRect.X < item.X + item.Width &&
                    newRect.X + newRect.Width > item.X &&
                    newRect.Y < item.Y + item.Height &&
                    newRect.Y + newRect.Height > item.Y)
                {
                    Collided = true;
                    Collider = item;
                }
            }
            Objects.EntityColliders.Add(newRect);
            if (Collided)
            {
                float dist = 0;        
                if(amount < 0)
                {
                    Objects.Player.touchingGround = true;
                    Dash.canDash = true;
                    dist = -(Collider.Y - (newRect.Y + newRect.Height) + 1);
                }
                else
                {
                    dist = newRect.Y - (Collider.Y + Collider.Height + 1);
                }
                Objects.Player.VelocityY = 0;
                return (dist/64);
            }
            else
            {
                Objects.Player.touchingGround = false;
                return (amount * Quase.deltaTime / 64) ;
            }
        }
    }
}
