using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Quase.Classes.Engine;
using Quase.Classes.UI;

namespace Quase.Classes.GameStates.Menus
{
    public class Options
    {
        static Button BackButton;
        static Texture2D ResolutionText;
        static Texture2D FullscreenText;
        static ContentManager Content = Quase._content;
        static SpriteBatch _spriteBatch = Quase._spriteBatch;
        static Button VSynceButton;
        static Button ResolutionButton;
        public static void LoadContent()
        {
            BackButton = new Button(Content.Load<Texture2D>("Assets/Menu/back"), new Vector2(1920 / 2, 920), 0.5f);
            FullscreenText = Content.Load<Texture2D>("Assets/Menu/Options/fullscreen");
            ResolutionText = Content.Load<Texture2D>("Assets/Menu/Options/resolution");
            ResolutionButton = new Button(Content.Load<Texture2D>("Assets/Menu/Options/" + Quase.WindowResolution.Y + "p"), new Vector2(1920 * 3 / 4 - 150, 300), 0.5f);
            VSynceButton = new Button(Content.Load<Texture2D>("Assets/Menu/Options/" + Quase.VSyncRate), new Vector2(1920*3/4 - 150, 180), 0.5f);
        } 
        public static void Update()
        {
            BackButton.Update();
            VSynceButton.Update();
            ResolutionButton.Update();
            if(BackButton.isClicked)
            {
                if(StateMachine.currentState == "Options")
                {
                    MainMenu.LoadContent();
                    StateMachine.currentState = "Main Menu";
                    BackButton.isClicked = false;
                    Unload();
                }
                else if(PauseMenu.currentMenu == "Options")
                {
                    BackButton.isClicked = false;
                    PauseMenu.currentMenu= null;      
                    Unload();
                }
            }
            if (VSynceButton.isClicked)
            {
                switch (Quase.VSyncRate)
                {
                    case 144f:
                        Quase.VSyncRate = 60;
                        VSynceButton._texture = Content.Load<Texture2D>("Assets/Menu/Options/60");
                        break;
                    case 60f:
                        Quase.VSyncRate = 144;
                        VSynceButton._texture = Content.Load<Texture2D>("Assets/Menu/Options/144hz");
                        break;
                    default:
                        break;
                }
                Quase._graphics.ApplyChanges();
                VSynceButton.isClicked = false;
            }
            
        }
        public static void Unload()
        {  
            BackButton = null;
        }
        public static void Draw()
        {
            BackButton.Draw();
            VSynceButton.Draw();
            ResolutionButton.Draw();
            _spriteBatch.Draw(texture: ResolutionText, position: new Vector2(1920 / 4 - 50, 180), null, color: Color.White, rotation: 0f, origin: new Vector2(0, ResolutionText.Height/2), scale: 0.6f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(texture: FullscreenText, position: new Vector2(1920 / 4 - 50, 250), null, color: Color.White, rotation: 0f, origin: new Vector2(0, FullscreenText.Height / 2), scale: 0.6f, SpriteEffects.None, 0f);

        }
    }
}
