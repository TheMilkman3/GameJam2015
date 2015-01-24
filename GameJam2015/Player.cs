using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJam2015
{
    public class Player : AnimatedEntity
    {
        // Amount of hit points that player has
        public int Health;

        //public int Width
        //{
        //    get { return SpriteAnimation.FrameWidth; }
        //}
        //public int Height
        //{
        //    get { return SpriteAnimation.FrameHeight; }
        //}

        //public void Initialize(Animation anim, Vector2 position)
        //{
        //    SpriteAnimation = anim;
        //    Position = position;
        //    Active = true;
        //    Velocity = Vector2.Zero;
        //    // Set the player health
        //    Health = 100;
        //}

        //public void Draw(SpriteBatch spriteBatch)
        //{
        //    SpriteAnimation.Draw(spriteBatch);
        //}

        //// Update the player animation
        //public void Update(GameTime gameTime)
        //{
        //    SpriteAnimation.Position = Position;
        //    SpriteAnimation.Update(gameTime);
        //}
    }
}
