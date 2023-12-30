using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Quase.Classes.Engine
{
    public class CoordinateEngine
    {
        public static Vector2 TileDimensions = new Vector2(64,64);
        public static Vector2 GetScreenPosition(Vector2 Coordinates)
        {
            Vector2 vect = new Vector2(Coordinates.X - (float)Math.Round(Objects.Player.x,2) - Objects.Camera.CameraOffset.X, -Coordinates.Y + (float)Math.Round(Objects.Player.y,2) + Objects.Camera.CameraOffset.Y);
            return new Vector2(1920/2, 1080/2) + vect * TileDimensions * Quase.Zoom;
        }
    }
}
