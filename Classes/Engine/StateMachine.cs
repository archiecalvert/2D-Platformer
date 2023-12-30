using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Quase.Classes.GameStates;
using Quase.Classes.GameStates.Menus;

namespace Quase.Classes.Engine
{
    public class StateMachine
    {
        public static string currentState = "Main Menu";
        public static bool Paused = true;
        public static bool inGame = false;
        public static void Initialise()
        {
            MainMenu.LoadContent();
        }
        public static void Update()
        {
            if (inGame && Paused)
            {
                PauseMenu.Update();
            }
            switch (currentState)
            {
                case "Main Menu":
                    MainMenu.Update();
                    break;
                case "Overworld":
                    if (Paused)
                    {
                        break;
                    }          
                    Overworld.Update();
                    Objects.Update();
                    PhysicsEngine.Update();
                    break;
                case "Options":
                    Options.Update();
                    break;
                default: 
                    break;
            } 
        }
        public static void Draw()
        {
            switch (currentState)
            {
                case "Main Menu":
                    MainMenu.Draw();
                    break;
                case "Overworld":     
                    Overworld.Draw();
                    Objects.Draw();
                    break;
                case "Options":
                    Options.Draw();
                    break;
                default: 
                    break;
            }
            if(inGame && Paused)
            {
                PauseMenu.Draw();
            }
        }
    }

}
