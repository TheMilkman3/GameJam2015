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
        int AIState = 0;

        public override void Initialize(Texture2D texture, float scale, Vector2 position)
        {
            base.Initialize(texture, scale, position);
            Velocity.X = DOODAD_SPEED;
        }

        public override List<Entity> Update(List<Entity> entities, GameTime gameTime)
        {
            timeUntilChange -= gameTime.ElapsedGameTime.Milliseconds;
            if (timeUntilChange <= 0)
            {
                switch (AIState)
                {
                    case 0:
                        RotateCW();
                        RotateCW();
                        Console.WriteLine("X: " + Velocity.X + ", Y: " + Velocity.Y);
                        break;
                    case 1:
                        RotateCW();
                        Console.WriteLine("X: " + Velocity.X + ", Y: " + Velocity.Y);
                        break;
                }
                timeUntilChange = CHANGE_TIME;
                if (AIState == 1)
                {
                    AIState = 0;
                }
                else
                {
                    AIState++;
                }
            }
            return base.Update(entities, gameTime);

        }
    }
}
