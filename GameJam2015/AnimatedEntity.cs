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

        public new int Width
        {
            get { return SpriteAnimation.FrameWidth; }
        }
        public new int Height
        {
            get { return SpriteAnimation.FrameHeight; }
        }

        /// <summary>
        /// Sets sprite and position of the entity.
        /// </summary>
        /// <param name="texture">Entity's 2d sprite.</param>
        /// <param name="position">X,Y coordinates of the entity on screen.</param>
        public void Initialize(Animation anim, float scale, Vector2 position)
        {
            base.Initialize(null, scale, position);
            SpriteAnimation = anim;
        }

        /// <summary>
        /// Per frame update of entity.
        /// </summary>
        /// <param name="entities">List of entities in room</param>
        public new void Update(List<Entity> entities, GameTime gameTime)
        {
            base.Update(entities, gameTime);
            SpriteAnimation.Position = Position;
            SpriteAnimation.Update(gameTime);
        }

        /// <summary>
        /// Draws entity's sprite.
        /// </summary>
        public new void Draw(SpriteBatch spriteBatch)
        {
            SpriteAnimation.Draw(spriteBatch);
        }
    }
}
