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
        public float Scale;
        public bool LevelGoal = false;
        public bool isObstacle = false;

        public bool Solid
        {
            get;
            set;
        }
        public virtual int Width()
        {
            return (int)(Sprite.Width*Scale);
        }
        public virtual int Height()
        {
            return (int)(Sprite.Height * Scale);
        }

        /// <summary>
        /// Sets sprite and position of the entity.
        /// </summary>
        /// <param name="texture">Entity's 2d sprite.</param>
        /// <param name="scale">Scale of the sprite</param>
        /// <param name="position">X,Y coordinates of the entity on screen.</param>
        public void Initialize(Texture2D texture, float scale, Vector2 position)
        {
            Sprite = texture;
            Scale = scale;
            Position = position;
            Velocity = Vector2.Zero;
        }

        /// <summary>
        /// Per frame update of entity.
        /// </summary>
        /// <param name="entities">List of entities in room</param>
        public virtual List<Entity> Update(List<Entity> entities, GameTime gameTime)
        {
            Position += Velocity;
            List<Entity> collided_entities = CheckCollision(entities);
            return collided_entities;
        }

        /// <summary>
        /// Draws entity's sprite.
        /// </summary>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Position, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
        }

        /// <summary>
        /// Returns a list of entities the current entities collides with.
        /// </summary>
        /// <param name="entities">List of entities in the room</param>
        /// <returns></returns>
        public List<Entity> CheckCollision(List<Entity> entities)
        {
            List<Entity> collided_entities = new List<Entity>();
            if(entities == null)
            {
                return null;
            }

            foreach(Entity e in entities)
            {
                float x1 = Position.X;
                float y1 = Position.Y;
                float x2 = e.Position.X;
                float y2 = e.Position.Y;
                float height1 = Height();
                float width1 = Width();
                float height2 = e.Height();
                float width2 = e.Width();
                if (x1 < x2 + width2 &&
                    x1 + width1 > x2 &&
                    y1 < y2 + height2 &&
                    y1 + height1 > y2 &&
                    this != e)
                {
                    collided_entities.Add(e);
                    if (Solid && e.Solid)
                    {
                        if (Velocity.X > 0)
                        {
                            Position.X = x2 - width1;
                        }
                        else if (Velocity.X < 0)
                        {
                            Position.X = x2 + width2;
                        }
                        if (Velocity.Y > 0)
                        {
                            Position.Y = y2 - height1;
                        }
                        else if (Velocity.Y < 0)
                        {
                            Position.Y = y2 + height2;
                        }
                    }
                }

            }
            return collided_entities;
        }

        public void RotateCW()
        {
            if (Math.Abs(Velocity.X) > 0)
            {
                Velocity = new Vector2(0, Velocity.X);
            }
            else if (Math.Abs(Velocity.Y) > 0)
            {
                Velocity = new Vector2(Velocity.Y * -1, 0);
            }
        }

        public void RotateCCW()
        {
            if (Math.Abs(Velocity.X) > 0)
            {
                Velocity = new Vector2(0, Velocity.X * -1);
            }
            if (Math.Abs(Velocity.Y) > 0)
            {
                Velocity = new Vector2(Velocity.Y, 0);
            }
        }
    }
}
