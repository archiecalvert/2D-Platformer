using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Quase.Classes.Character_Abilities;

namespace Quase.Classes.Engine
{
    public class QuaseEngine
    {
        public static void LoadContent()
        {
            StateMachine.Initialise();
        }
        public static void Update()
        {       
            StateMachine.Update();
            Input.Update();
            ClampValues();
        }
        public static void Draw()
        {
            StateMachine.Draw();
            if(DebugWindow.isEnabled) DrawColliders();
            Objects.Colliders.Clear();
            Objects.EntityColliders.Clear();
        }    
        static void ClampValues()
        {
            if (Objects.Player == null) return;
            Quase.Zoom = (float)Math.Round(Math.Clamp(Quase.Zoom, 0.3f, 1.5f),2);
        }
        static void DrawColliders()
        {
            foreach (Rectangle rect in Objects.Colliders)
            {
                Quase._spriteBatch.Draw(Quase._content.Load<Texture2D>("Assets/Debug/hitbox"), rect, new Color(255, 0, 0, 50));
            }
            foreach(Rectangle rect in Objects.EntityColliders)
            {
                if (rect == Objects.Player.PlayerHitbox.Box) { Quase._spriteBatch.Draw(Quase._content.Load<Texture2D>("Assets/Debug/hitbox"), rect, new Color(0, 80, 20, 100)); }
                else
                {
                    Quase._spriteBatch.Draw(Quase._content.Load<Texture2D>("Assets/Debug/hitbox"), rect, new Color(50, 50, 255, 256));

                }
            }
        }
    }
    
}
    

