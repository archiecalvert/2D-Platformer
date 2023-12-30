using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Quase.Classes.Engine;
using Quase.Classes.UI;

namespace Quase.Classes.GameStates.Menus
{
    public class PauseMenu
    {
        public static Texture2D BlankRect;
        static Button MainMenuButton;
        static Button OptionsButton;
        static Texture2D PausedTexture;
        static ContentManager Content = Quase._content;
        public static string currentMenu = null;
        public static void LoadContent()
        {
            BlankRect = new Texture2D(Quase._graphics.GraphicsDevice, 1, 1);
            BlankRect.SetData(new[] { Color.White });
            MainMenuButton = new Button(Content.Load<Texture2D>("Assets/Menu/Paused/mainmenu"), new Vector2(1920 * 4 / 8, 900), .55f);
            PausedTexture = Content.Load<Texture2D>("Assets/Menu/Paused/paused");
            OptionsButton = new Button(Content.Load<Texture2D>("Assets/Menu/Main/options"), new Vector2(1920 / 2, 325), .5f);
        }
        public static void Update()
        {
            switch (currentMenu)
            {
                case null:
                    OptionsButton.Update();
                    MainMenuButton.Update(); 
                    break;
                case "Options":
                    Options.Update(); 
                    break;
                default: 
                    break;
            }
            if (MainMenuButton.isClicked)
            {
                MainMenuButton.isClicked = false;
                MainMenu.LoadContent();
                StateMachine.currentState = "Main Menu";
                StateMachine.Paused = true;
                StateMachine.inGame= false;
                Overworld.Unload();
                Unload();
                return;
            }
            if (OptionsButton.isClicked)
            {
                OptionsButton.isClicked= false;
                currentMenu = "Options";
                Options.LoadContent();
            }
            
        }
        public static void Unload()
        {
            BlankRect = null;
            MainMenuButton = null;
            PausedTexture = null;
            OptionsButton= null;
        }
        public static void Draw()
        {
            
            switch(currentMenu)
            {
                case null:
                    Quase._spriteBatch.Draw(BlankRect, new Rectangle(0, 0, 1920, 1080), new Color(0, 0, 0, 100));
                    MainMenuButton.Draw();
                    OptionsButton.Draw();
                    break;
                case "Options":
                    Quase._spriteBatch.Draw(BlankRect, new Rectangle(0,0,1920,1080), new Color(48,48,48,255));
                    Options.Draw(); break;
                default: break;
            }
        }
    }
}
