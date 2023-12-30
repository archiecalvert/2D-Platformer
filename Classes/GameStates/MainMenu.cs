using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Quase.Classes.Engine;
using Quase.Classes.GameStates.Menus;
using Quase.Classes.UI;

namespace Quase.Classes.GameStates
{
    public class MainMenu
    {
        static ContentManager Content = Quase._content;
        static Button OptionsButton;
        static Button StartButton;
        static Button ExitButton;
        static Texture2D Title;
        static float j = 0;
        public static void LoadContent()
        {
            Quase.WindowColour = new Color(48,48,48,255);
            j = 0;
            StartButton = new Button(Content.Load<Texture2D>("Assets/Menu/Main/start"), new Vector2(1920 / 2, 700), 0.5f);
            OptionsButton = new Button(Content.Load<Texture2D>("Assets/Menu/Main/options"), new Vector2(1920 / 2, 800), 0.5f);
            //Title = Content.Load<Texture2D>("Assets/Menu/Main/title");
            ExitButton = new Button(Content.Load<Texture2D>("Assets/Menu/Main/Exit"), new Vector2(1920 / 2, 900), 0.5f);
            DebugWindow.LoadContent();
        }
        public static void Update()
        {
            StartButton.Update();
            OptionsButton.Update();
            ExitButton.Update();
            if (StartButton.isClicked)
            {
                StateMachine.inGame= true;
                StateMachine.Paused = false;
                Overworld.LoadContent();
                StateMachine.currentState = "Overworld";
                StartButton.isClicked = false;
                UnloadContent();
                return;
            }
            if(OptionsButton.isClicked)
            {
                OptionsButton.isClicked = false;
                UnloadContent();
                Options.LoadContent();
                StateMachine.currentState = "Options";
                
            }
            if(ExitButton.isClicked)
            {
                System.Environment.Exit(0);
            }
        }
        public static void UnloadContent()
        {
            StartButton = null;
            OptionsButton = null;
        }
        public static void Draw()
        {
            j += 0.4f;
            //Quase._spriteBatch.Draw(texture: Content.Load<Texture2D>("Assets/temp/background"), new Rectangle(0,0, 1920,1080d), Color.White);
            StartButton.Draw();
            OptionsButton.Draw();
            ExitButton.Draw();
            //Quase._spriteBatch.Draw(texture: Title, position: new Vector2(1920 / 2, 200), null, color: Color.White, rotation: 0f, origin: new Vector2(Title.Width / 2, Title.Height / 2), scale: 1f, SpriteEffects.None, 0f);
            DebugWindow.Draw();
        }
    }
}
