using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Quase.Classes.Engine;

namespace Quase.Classes.Character_Abilities
{
    public class Dash
    {
        public static bool canDash = false;
        public static void Begin()
        {
            return;
            canDash = false;
            Vector2 MousePosition =  new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            Vector2 Direction = Vector2.Normalize(new Vector2(1920/2,1080/2) - MousePosition);
            Objects.Player.VelocityX = -Direction.X * 10;
            Objects.Player.VelocityY = Direction.Y * 10;
        }
    }
}
