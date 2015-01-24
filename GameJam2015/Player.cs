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
        public new void Initialize(Animation anim, float scale, Vector2 position)
        {
            base.Initialize(anim, scale, position);
            Solid = true;
        }
    }
}
