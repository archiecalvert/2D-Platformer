using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Quase.Classes.Engine
{
    public class Camera
    {
        //public Vector2 Coordinates;
        public bool isAttached = true;
        public float CameraSpeed = 15f;
        public Vector2 CameraOffset = new Vector2(0,0);
        public Camera(Vector2 StartCoordinates)
        {
            CameraOffset = StartCoordinates;
            Objects.Camera = this;
        }
        public void Update()
        {
            if(isAttached)
            {
                CameraOffset = Vector2.Zero;
            }
        }
    }
}
