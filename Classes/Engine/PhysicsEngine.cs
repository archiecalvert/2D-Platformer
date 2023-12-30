using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Quase.Classes.GameComponents;

namespace Quase.Classes.Engine
{
    public class PhysicsEngine
    {
        public static float GravityAcceleration = 9.8f;
        public static float TerminalVelocity = 8f;
        public static dynamic debugVar;
        public static void Update()
        {    
            if (!Input.movingRight && Objects.Player.VelocityX < 0)
            {
                Objects.Player.VelocityX += (20f / 144) * Quase.deltaTime;
            }
            if (!Input.movingLeft && Objects.Player.VelocityX > 0)
            {
                Objects.Player.VelocityX -= (20f / 144) * Quase.deltaTime;
            }
            if (!Input.movingRight && !Input.movingLeft)
            {
                if (Objects.Player.VelocityX < 0.3F && Objects.Player.VelocityX > -0.3f)
                {
                    Objects.Player.VelocityX = 0;
                }
            }
            Objects.Player.VelocityY -= ((float)(1.2f * Quase.deltaTime * GravityAcceleration) / 64);
            debugVar = ((float)(1.2f * GravityAcceleration) / 64) * Quase.deltaTime;


            Objects.Player.x += Collisions.MoveX(Objects.Player.VelocityX);
            Objects.Player.y += Collisions.MoveY(Objects.Player.VelocityY);
        }
    }
}
