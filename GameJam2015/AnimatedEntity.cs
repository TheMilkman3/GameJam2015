using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJam2015
{
    public class AnimatedEntity
    {
        public Animation SpriteAnimation;
        public Vector2 Position;
        public Vector2 Velocity;
        public bool Active;
        public bool Solid { get; set; }

        public int Width
        {
            get { return SpriteAnimation.FrameWidth; }
        }
        public int Height
        {
            get { return SpriteAnimation.FrameHeight; }
        }

        /// <summary>
        /// Sets sprite and position of the entity.
        /// </summary>
        /// <param name="texture">Entity's 2d sprite.</param>
        /// <param name="position">X,Y coordinates of the entity on screen.</param>
        public void Initialize(Animation anim, Vector2 position)
        {
            SpriteAnimation = anim;
            Position = position;
            Velocity = Vector2.Zero;
            Active = true;
        }

        /// <summary>
        /// Per frame update of entity.
        /// </summary>
        /// <param name="entities">List of entities in room</param>
        public void Update(GameTime gameTime)
        {
            SpriteAnimation.Position = Position;
            SpriteAnimation.Update(gameTime);
        }

        /// <summary>
        /// Draws entity's sprite.
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteAnimation.Draw(spriteBatch);
        }

        /// <summary>
        /// Returns a list of entities the current entities collides with.
        /// </summary>
        /// <param name="entities">List of entities in the room</param>
        /// <returns></returns>
        public List<Entity> CheckCollision(List<Entity> entities)
        {
            List<Entity> collided_entities = new List<Entity>();
            foreach (Entity e in entities)
            {
                float x1 = Position.X;
                float y1 = Position.Y;
                float x2 = e.Position.X;
                float y2 = e.Position.Y;
                float height1 = Height;
                float width1 = Width;
                float height2 = e.Height;
                float width2 = e.Width;
                if (x1 < x2 + width2 &&
                    x1 + width1 > x2 &&
                    y1 < y2 + height2 &&
                    y1 + height1 > y2)
                {
                    collided_entities.Add(e);
                }

            }
            return collided_entities;
        }
    }
}
