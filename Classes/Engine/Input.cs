using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Quase.Classes.GameStates.Menus;
using Microsoft.Xna.Framework.Graphics;
using Quase.Classes.Character_Abilities;
namespace Quase.Classes.Engine
{
    public class Input
    {
        //Mouse Variables
        public static int MouseX = 0;
        public static int MouseY = 0;
        public static bool LeftClick = false;
        static float clickDelayAmount = 0.1F;
        public static bool clickDelayActive = false;
        //Keyboard Variables
        static bool keyDelayActive = false;
        static float keyDelayAmount = 0.25f;
        //Player Variables
        public static bool isMoving = false;
        public static bool movingLeft = false;
        public static bool movingRight = false;
        
        static float speed; //measured in tiles per second
        public static bool makeParticle = true;
        public static float particleDelay = 0.2f;
        public static float speedLim = 5f;
        static Random rnd = new Random();
        public static void Update()
        {
            if(Objects.Player != null) { speed = Objects.Player.acceleration; }
            MouseUpdate();
            DelayCheck();
            if (StateMachine.inGame && !StateMachine.Paused)
            {
                WASDUpdate();
            }
            CheckKeyboard();
        }
        static void MouseUpdate()
        {
            MouseX = Mouse.GetState().X; MouseY = Mouse.GetState().Y;
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && !clickDelayActive) //Used on the main menu
            {
                LeftClick = true;
            }
            else
            {
                LeftClick = false;
            }
        }
        static void CheckKeyboard()
        {
            if (GetKey(Keys.L) && !keyDelayActive)
            {
                if(Objects.Camera != null)
                {
                    Objects.Camera.isAttached = !Objects.Camera.isAttached;
                    keyDelayActive = true;
                }
            }
            if(GetKey(Keys.Z) && !keyDelayActive)
            {
                if(Objects.Player != null)
                {
                    Quase.Zoom += 0.01f;
                }
            }
            if (GetKey(Keys.X) && !keyDelayActive)
            {
                if (Objects.Player != null)
                {
                    Quase.Zoom -= 0.01f;
                }
            }
            if(GetKey(Keys.F5) && !keyDelayActive)
            {
                DebugWindow.isEnabled = !DebugWindow.isEnabled;
                keyDelayActive= true;
            }
            if (GetKey(Keys.Escape) && StateMachine.inGame && !keyDelayActive)
            {
                
                StateMachine.Paused = !StateMachine.Paused;
                if (StateMachine.Paused)
                {
                    PauseMenu.LoadContent();
                }
                else
                {
                    PauseMenu.Unload();
                }
                keyDelayActive = true;
            }
        }
        static void WASDUpdate()
        {
            if(Objects.Player == null) { return; }
            isMoving = false;
            movingRight= false;
            movingLeft= false;
            if (GetKey(Keys.W) && !GetKey(Keys.S))
            {
                if (!Objects.Camera.isAttached)
                {
                    Objects.Camera.CameraOffset.Y += Objects.Camera.CameraSpeed;
                }           
            }

            if (GetKey(Keys.A) && !GetKey(Keys.D) && !Objects.Player.CollidingRight)
            {
                if (Objects.Camera.isAttached)
                {
                    isMoving = true;
                    movingRight= true;
                    Objects.Player.facingDirection = SpriteEffects.FlipHorizontally;
                    if (Objects.Player.VelocityX > -speedLim)
                    {
                        Objects.Player.VelocityX -= (speed / 64) * Quase.deltaTime;
                    }
                    Objects.Player.facingLeft = false;
                }
                else
                {
                    Objects.Camera.CameraOffset.X -= Objects.Camera.CameraSpeed * Quase.deltaTime;
                }
            }
            if (GetKey(Keys.S) && !GetKey(Keys.W))
            {
                if (!Objects.Camera.isAttached)
                {
                    Objects.Camera.CameraOffset.Y -= Objects.Camera.CameraSpeed * Quase.deltaTime;
                }
            }
            if (GetKey(Keys.D) && !GetKey(Keys.A) && !Objects.Player.CollidingRight)
            {
                if (Objects.Camera.isAttached)
                {
                    isMoving = true;
                    Objects.Player.facingLeft = true;
                    movingLeft = true;
                    Objects.Player.facingDirection = SpriteEffects.None;
                    if (Objects.Player.VelocityX < speedLim)
                    {
                        Objects.Player.VelocityX += (speed / 64) * Quase.deltaTime;
                    }
                }
                else
                {
                    Objects.Camera.CameraOffset.X += Objects.Camera.CameraSpeed * Quase.deltaTime;
                }
            }
            if ((GetKey(Keys.Space) || GetKey(Keys.W)) && !keyDelayActive && Objects.Player.touchingGround && Objects.Camera.isAttached)
            {
                Objects.Player.JumpActive = true;
                Objects.Player.touchingGround = false;
                Objects.Player.y += 40 / 64;
                Objects.Player.VelocityY = 10;
                keyDelayActive= true;
                
            }
            if(GetKey(Keys.Space) && !keyDelayActive && !Objects.Player.touchingGround && Dash.canDash)
            {
                Dash.Begin();
                keyDelayActive= true;
            }

            if (isMoving)
            {
                if (makeParticle && (Objects.Player.VelocityX > 1.5f|| Objects.Player.VelocityX < -1.5f) && Objects.Player.touchingGround)
                {
                    Particle part = new Particle(new Vector2(Objects.Player.x, Objects.Player.y));
                    makeParticle= false;
                }
            }
            
        }
        static bool GetKey(Keys key)
        {
            return Keyboard.GetState().IsKeyDown(key);
        }
        public static void DelayCheck() //Allows for Key Delays to be created
        {
            if (clickDelayActive)
            {
                if (clickDelayAmount > 0)
                {
                    clickDelayAmount -= 1 * (float)Quase._gameTime.ElapsedGameTime.TotalSeconds; //Scales the delay so that it will subtract one every second
                }
                else
                {
                    clickDelayAmount = 0.1f;
                    clickDelayActive = false;
                }
            }
            if(keyDelayActive)
            {
                if (keyDelayAmount > 0)
                {
                    keyDelayAmount -= 1 * (float)Quase._gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    keyDelayAmount = 0.25f;
                    keyDelayActive = false;
                }
            }
            if(!makeParticle)
            {
                if(particleDelay >= 0)
                {
                    particleDelay -= 1* (float)Quase._gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    particleDelay = 0.01f;
                    makeParticle = true;
                }
            }
        }
    }
}
