using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJam2015
{
    public class Entity
    {
        public Texture2D Sprite;
        public Vector2 Position;
        public Vector2 Velocity;
        public bool Solid
        {
            get;
            set;
        }
        public int Width
        {
            get { return Sprite.Width; }
        }
        public int Height
        {
            get { return Sprite.Height; }
        }

        /// <summary>
        /// Sets sprite and position of the entity.
        /// </summary>
        /// <param name="texture">Entity's 2d sprite.</param>
        /// <param name="position">X,Y coordinates of the entity on screen.</param>
        public void Initialize(Texture2D texture, Vector2 position)
        {
            Sprite = texture;
            Position = position;
            Velocity = Vector2.Zero;
        }

        /// <summary>
        /// Per frame update of entity.
        /// </summary>
        /// <param name="entities">List of entities in room</param>
        public void Update(List<Entity>  entities)
        {
            Position += Velocity;
            List<Entity> collided_entities = CheckCollision(entities);
            foreach (Entity e in collided_entities)
            {
                if (Solid && e.Solid)
                {
                    Position -= Velocity;
                }
            }
        }

        /// <summary>
        /// Draws entity's sprite.
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Sprite, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        /// <summary>
        /// Returns a list of entities the current entities collides with.
        /// </summary>
        /// <param name="entities">List of entities in the room</param>
        /// <returns></returns>
        public List<Entity> CheckCollision(List<Entity> entities)
        {
            List<Entity> collided_entities = new List<Entity>();
            foreach(Entity e in entities)
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
