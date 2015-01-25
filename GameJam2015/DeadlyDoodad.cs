using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using System.Threading;

namespace GameJam2015
{
    class DeadlyDoodad : Entity
    {
        static readonly int CHANGE_TIME = 1000;
        static readonly int DOODAD_SPEED = 2;
        int timeUntilChange = CHANGE_TIME;
        Random rand;

        public override void Initialize(Texture2D texture, float scale, Vector2 position)
        {
            base.Initialize(texture, scale, position);
            Velocity.X = DOODAD_SPEED;
            Deadly = true;
            rand = new Random();
        }

        public override List<Entity> Update(List<Entity> entities, GameTime gameTime)
        {
            timeUntilChange -= gameTime.ElapsedGameTime.Milliseconds;
            if (timeUntilChange <= 0)
            {
                int n = rand.Next(2);
                if (n == 0)
                {
                    RotateCW();
                }
                else if (n == 1)
                {
                    RotateCCW();
                }
                timeUntilChange = CHANGE_TIME;
            }
            return base.Update(entities, gameTime);

        }
    }
}
