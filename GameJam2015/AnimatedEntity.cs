using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJam2015
{
    public class AnimatedEntity : Entity
    {
        public Animation SpriteAnimation;

        public override int Width()
        {
            return (int)(SpriteAnimation.FrameWidth * SpriteAnimation.scale);
        }
        public override int Height()
        {
            return (int)(SpriteAnimation.FrameHeight * SpriteAnimation.scale);
        }

        /// <summary>
        /// Sets sprite and position of the entity.
        /// </summary>
        /// <param name="texture">Entity's 2d sprite.</param>
        /// <param name="position">X,Y coordinates of the entity on screen.</param>
        public virtual void Initialize(Animation anim, float scale, Vector2 position)
        {
            base.Initialize(null, scale, position);
            Solid = true;
            SpriteAnimation = anim;
            SpriteAnimation.Position = position;
        }

        /// <summary>
        /// Per frame update of entity.
        /// </summary>
        /// <param name="entities">List of entities in room</param>
        public override List<Entity> Update(List<Entity> entities, GameTime gameTime)
        {
            List<Entity> collided_entities = base.Update(entities, gameTime);
            if (Velocity.X < 0)
            {
                SpriteAnimation.FlipHorizontally = true;
            }
            else if (Velocity.X > 0)
            {
                SpriteAnimation.FlipHorizontally = false;
            }
            SpriteAnimation.Position = Position;
            SpriteAnimation.Update(gameTime);
            return collided_entities;
        }

        /// <summary>
        /// Draws entity's sprite.
        /// </summary>
        public override void Draw(SpriteBatch spriteBatch)
        {
            SpriteAnimation.Draw(spriteBatch);
        }
    }
}
