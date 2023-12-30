using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Quase.Classes.Engine;

namespace Quase.Classes.GameComponents
{
    public class Hitbox
    {
        public Vector2 _size, _position, _screenpos, _coords;
        public Rectangle Box;
        public bool isPlayers = false;
        public Hitbox(Vector2 Size, Vector2 Coordinates, bool Players)
        {
            _coords = Coordinates;
            _size = Size * Quase.Zoom;
            isPlayers = Players;
        }

        public void Update()
        {  
            _screenpos = CoordinateEngine.GetScreenPosition(_coords);
            Box = new Rectangle((int)_screenpos.X, (int)_screenpos.Y, (int)_size.X, (int)_size.Y);
            Objects.EntityColliders.Add(Box);
        }
        public void Draw()
        {
            //Box = new Rectangle((int)_screenpos.X, (int)_screenpos.Y, (int)_size.X, (int)_size.Y);
            Quase._spriteBatch.Draw(texture: Quase._content.Load<Texture2D>("Assets/Debug/hitbox"), Box, Color.White);
        }
    }
}
