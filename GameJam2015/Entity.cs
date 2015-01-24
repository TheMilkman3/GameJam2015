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
        public Vector2 Size
        {
            get;
            set;
        }

        /// <summary>
        /// Performs initialization on entity.
        /// </summary>
        public void Initialize()
        {
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
        public void Draw()
        {
        }
    }
}
