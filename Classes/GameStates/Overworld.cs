using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Quase.Classes.Engine;
using Quase.Classes.GameComponents;
using Microsoft.Xna.Framework;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework.Graphics;

namespace Quase.Classes.GameStates
{
    public class Overworld
    {
        public static Player player;
        public static Camera GameCam;
        static Tilemap Ground;
        public static bool isLoaded = false;
        public static void LoadContent()
        {
            Quase.WindowColour = new Color(97,165,195,256);
            isLoaded= true;
            player = new Player(new Vector2(3f,0f));
            player.LoadContent();
            GameCam = new Camera(new Vector2(10,0));
            Ground = new Tilemap("Content/Assets/Data/Map/Overworld/ground.json", Quase._content.Load<Texture2D>("Assets/Game/Tilesets/Overworld"), 0.8f, new Vector2(64,64));
        }
        public static void Update()
        {
            Ground.Update();
        }
        public static void Unload()
        {
            player = null;
            GameCam = null;
            Ground = null;
        }
        public static void Draw()
        {
            Ground.Draw();
            player.Draw();
            DebugWindow.Draw();
        }
    }
}
