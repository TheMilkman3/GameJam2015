using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJam2015
{
    class Entity
    {
        public Texture2D Sprite;
        public Vector2 Position;
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
        }

        /// <summary>
        /// Per frame update of entity.
        /// </summary>
        public void Update()
        {
        }

        /// <summary>
        /// Draws entity's sprite.
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
