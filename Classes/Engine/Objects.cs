using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Quase.Classes.Components.Entities;
using Quase.Classes.GameComponents;
using Quase.Classes.UI;

namespace Quase.Classes.Engine
{
    public class Objects
    {
        public static Player Player { get; set; }
        public static Camera Camera { get; set; }
        public static List<Button> Buttons = new List<Button>();
        public static List<Particle> Particles = new List<Particle>();
        public static List<Entity> Entities = new List<Entity>();
        public static List<Rectangle> Colliders = new List<Rectangle>();
        public static List<Rectangle> EntityColliders = new List<Rectangle>();
        
        public static void Update()
        {
            if (StateMachine.inGame && !StateMachine.Paused)
            {
                if (Camera != null)
                {
                    Camera.Update();
                }
                if (Player != null)
                {
                    Player.Update();
                }
            }
            foreach(Particle item in Particles)
            {
                if (item.remTime <= 0)
                {
                    item.shouldDraw = false;
                }
                else
                {
                    item.Update();
                }
                
            }
            for(int i = 0; i < Particles.Count; i++)
            {
                if (!Particles[i].shouldDraw)
                {
                    Particles.Remove(Particles[i]);
                }
            }
        }
        public static void Draw()
        {
            foreach(Particle item in Particles)
            {
                if(item.shouldDraw)
                {
                    item.Draw();
                }
            }
        }
    }
}
