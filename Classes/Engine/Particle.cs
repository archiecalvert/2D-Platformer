using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Quase.Classes.Engine
{
    public class Particle
    {
        Vector2 PositionOnScreen;
        Texture2D _texture;
        public Vector2 _coords;
        public float lifespan = 1f;
        public float remTime = 1;
        public bool shouldDraw = true;
        Random rnd = new Random();
        float _scale = 3f;
        int rot;
        Color colour = Color.White;
        float dropOff = 1;
        public Particle(Vector2 StartCoordinates)
        {     
            _coords = StartCoordinates;
            _texture = Quase._content.Load<Texture2D>("Assets/Effects/particle");
            Objects.Particles.Add(this);
            rot = rnd.Next(50)/10;
            remTime = (rnd.Next(3))/10f;
        }
        public void Update()
        {
            if (Objects.Player.facingLeft)
            {
                PositionOnScreen = CoordinateEngine.GetScreenPosition(_coords) + new Vector2(-30, 30) * Quase.Zoom;
            }
            else
            {
                PositionOnScreen = CoordinateEngine.GetScreenPosition(_coords) + new Vector2(0, 30) * Quase.Zoom;
            }
            colour = new Color(255, 255, 255, 255);
            remTime -= 1 / Quase.FPS;
        }
        public void Draw()
        {
            if (!shouldDraw) { return; }
            Quase._spriteBatch.Draw(texture: _texture, position: PositionOnScreen, null, colour, rot, new Vector2(_texture.Width/2, _texture.Height/2), _scale * Quase.Zoom, SpriteEffects.None, 0f);
        }
    }
}
