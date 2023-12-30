using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Quase.Classes.Engine;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json.Bson;
using Quase.Classes.Components.Player_Classes;

namespace Quase.Classes.GameComponents
{
    public class Player
    {
        ContentManager Content = Quase._content;
        Texture2D TexAtlas;
        public Rectangle SourceRectangle;
        public SpriteEffects facingDirection;
        public float x;
        public float y;
        public float VelocityX = 0, VelocityY=0;
        public float acceleration = 2.75F;
        float playerScale = 1.3f;
        public bool facingLeft = false;
        public bool JumpActive = false;
        public float jumpTime = 0.5f;
        public bool touchingGround = false;
        public bool CollidingLeft = false, CollidingRight = false;
        public Hitbox PlayerHitbox;
        public Player(Vector2 Coordinates)
        {          
            x = Coordinates.X;
            y = Coordinates.Y;
            Objects.Player = this;
        }
        public void LoadContent()
        {
            TexAtlas = Content.Load<Texture2D>("Assets/Game/Player/player"); //Loads the character texture atlas into RAM
            SourceRectangle = new Rectangle(); //Used in order to swap between the different frames on the atlas
            PlayerHitbox = new Hitbox(new Vector2(44 * playerScale, 61 * playerScale), Vector2.Zero, true);
        }
        public void Update()
        {
            PlayerHitbox._coords = new Vector2(x - 0.65f, y + 0.77f);
            PlayerHitbox.Update();
            PlayerAnimator.Update();
            y += (float)Math.Round((double)(VelocityY / 64), 3);
            JumpCheck();
        }
        public void Draw()
        {     
            Quase._spriteBatch.Draw(TexAtlas, position: CoordinateEngine.GetScreenPosition(new Vector2((float)Math.Round(x,2), (float)Math.Round(y,2))), sourceRectangle: SourceRectangle, color: Color.White, rotation: 0f, origin: new Vector2(PlayerAnimator.frameWidth * playerScale / 2), scale: playerScale * Quase.Zoom, facingDirection, layerDepth: 0F);
        }
        public void JumpCheck()
        {
            if (JumpActive)
            {
                jumpTime -= 0.1f;
            }
            if(jumpTime <= 0)
            {
                JumpActive = false;
            }
        }
    }
}
