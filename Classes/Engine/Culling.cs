using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Quase.Classes.Engine
{
    public class Culling
    {
        public Culling() { }
        public static bool IsVisible(Vector2 Coordinates)
        {
            Vector2 vect = CoordinateEngine.GetScreenPosition(Coordinates);
            if (vect.X >= -64 && vect.X <= (1920 + 64)) //checks to see if the position passed in is horizontally on screen
            {
                if (vect.Y>= -64  && vect.Y <= (1080 + 64)) //checks to see if the position passed in is vertically on screen
                {
                    return true;
                }
            }
            return false;
        }
    }
}
