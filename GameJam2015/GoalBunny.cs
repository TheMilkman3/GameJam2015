using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJam2015
{
    public class GoalBunny : AnimatedEntity
    {
        static readonly int CHANGE_TIME = 500;
        static readonly int BUNNY_SPEED = 5;
        int timeUntilChange = CHANGE_TIME;
        Random rand = new Random();
        public override void Initialize(Animation anim, float scale, Vector2 position)
        {
            base.Initialize(anim, scale, position);
            LevelGoal = true;
            Velocity = new Vector2(BUNNY_SPEED, 0);
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
