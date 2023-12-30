using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Quase.Classes.Engine
{
    public class DebugWindow
    {
        public static bool isEnabled = false;
        static ContentManager Content = Quase._content;
        static SpriteFont font;
        public static void LoadContent()
        {
            font = Content.Load<SpriteFont>("Assets/Fonts/DebugFont");
        }
        public static void Draw()
        {
            if (!isEnabled) return;
            if(StateMachine.currentState != "Main Menu")
            {
                Quase._spriteBatch.DrawString(font, "=============PLAYER=============", new Vector2(0, 0), Color.White);
                Quase._spriteBatch.DrawString(font, "Player Coordinates: " + new Vector2(Objects.Player.x, Objects.Player.y), new Vector2(0, 20), Color.White);
                Quase._spriteBatch.DrawString(font, "Player Velocity: " + new Vector2(Objects.Player.VelocityX, Objects.Player.VelocityY), new Vector2(0, 40), Color.White);
                Quase._spriteBatch.DrawString(font, "Touching Ground?: " + Objects.Player.touchingGround, new Vector2(0, 60), Color.White);
                Quase._spriteBatch.DrawString(font, "=============WORLD=============", new Vector2(0, 80), Color.White);
                Quase._spriteBatch.DrawString(font, "FPS: "+Quase.FPS, new Vector2(0, 100), Color.White);
                Quase._spriteBatch.DrawString(font, "Number of Collidables" + Objects.Colliders.Count, new Vector2(0, 120), Color.White);
                Quase._spriteBatch.DrawString(font, "Delta Time: " + Quase.deltaTime, new Vector2(0, 140), Color.White);
                Quase._spriteBatch.DrawString(font, "Gravity: " + PhysicsEngine.debugVar, new Vector2(0, 160), Color.White);

            }
        }
    }
}
