using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Quase.Classes.Engine;

namespace Quase.Classes.UI
{
    public class Button
    {
        public Texture2D _texture;
        Vector2 _pos;
        float _scale;
        public Rectangle MouseRect;
        public Rectangle ButtonRect;
        public bool isClicked = false;
        SpriteBatch _spriteBatch = Quase._spriteBatch;
        public bool delete = false;
        public Button(Texture2D Texture, Vector2 ScreenCoordinates, float scale)
        {
            _texture = Texture;
            _pos = new Vector2(ScreenCoordinates.X - (_texture.Width * scale) / 2, ScreenCoordinates.Y - (_texture.Height * scale / 2));
            _scale = scale;
            ButtonRect = new Rectangle((int)(_pos.X), (int)(_pos.Y), (int)(_texture.Width * _scale), (int)(_texture.Height * _scale)); //creates a rectangle in the same location as the button
            Objects.Buttons.Add(this);
        }
        public void Update()
        {
            ButtonRect.X = (int)(_pos.X * Quase.WindowScale);
            ButtonRect.Y = (int)(_pos.Y * Quase.WindowScale);
            MouseRect = new Rectangle(Input.MouseX, Input.MouseY, 1, 1); //creates a rectangle one x one pixel and snaps it to the mouse
            if (ButtonRect.Intersects(MouseRect) && !Input.clickDelayActive && Input.LeftClick) //checks to see if the mouse is touching the button, and if the left click has been pressed.
            {
                isClicked = true;
                Input.clickDelayActive = true;           
            }
        }
        public void Draw()
        {
            _spriteBatch.Draw(texture: _texture, position: _pos, null, rotation: 0f, origin: Vector2.Zero, effects: SpriteEffects.None, color: Color.White, scale: _scale, layerDepth: 0);
        }
    }
}
