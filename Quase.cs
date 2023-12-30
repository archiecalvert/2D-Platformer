using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Quase.Classes.Engine;
using Quase.Classes.GameComponents;
using Quase.Classes.GameStates;

namespace Quase
{
    public class Quase : Game
    {
        public static GraphicsDeviceManager _graphics;
        public static SpriteBatch _spriteBatch;
        public static ContentManager _content;
        public static GameTime _gameTime;
        public static float FPS;
        public static float VSyncRate = 144;
        RenderTarget2D RenderTarget;
        public static float WindowScale = 1;
        public static float Zoom = 0.8f;
        public static Color WindowColour = new Color(30, 30, 30, 100);
        public static int TileDim = 64;
        public static Vector2 WindowResolution = new(1920,1080);
        public static float deltaTime;
        public Quase()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferMultiSampling = false;
            _graphics.SynchronizeWithVerticalRetrace = false;
            Content.RootDirectory = "Content";
            _content = Content;
            IsMouseVisible = true;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            TargetElapsedTime = TimeSpan.FromSeconds(1.0f / VSyncRate); //Sets the Max FPS
            this.IsFixedTimeStep = true;
            base.Initialize();
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = (int)WindowResolution.X;
            _graphics.PreferredBackBufferHeight = (int)WindowResolution.Y;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();
            
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            RenderTarget = new RenderTarget2D(_graphics.GraphicsDevice, 1920, 1080);
            QuaseEngine.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            TargetElapsedTime = TimeSpan.FromSeconds(1.0f / VSyncRate); //Sets the Max FPS
            QuaseEngine.Update();     
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _gameTime = gameTime;
            FPS = (float)(1f / gameTime.ElapsedGameTime.TotalSeconds);
            deltaTime = 144F/FPS;
            WindowScale = 1f / (1080f / GraphicsDevice.Viewport.Height); //Sets the amount the window needs to be scaled in order to draw at different resolutions
            GraphicsDevice.SetRenderTarget(RenderTarget);
            GraphicsDevice.Clear(WindowColour);
            base.Draw(gameTime);
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null);
            QuaseEngine.Draw();
            //Draws the current state to the screen
            _spriteBatch.End();
            //Code underneath deals with resolution scaling
            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(WindowColour);
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null);
            _spriteBatch.Draw(RenderTarget, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, WindowScale, SpriteEffects.None, 0f);
            _spriteBatch.End();
        }
    }
}