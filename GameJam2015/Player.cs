using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJam2015
{
    public class Player : Entity
    {
        public new void Initialize(Texture2D texture, Vector2 position)
        {
            base.Initialize(texture, position);
            Solid = true;
        }
    }
}
