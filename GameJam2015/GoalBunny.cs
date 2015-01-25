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
        int AIState = 0;
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
                timeUntilChange = CHANGE_TIME;
                Velocity = new Vector2(BUNNY_SPEED, 0);
                
                if (AIState == 9)
                {
                    AIState = 0;
                }
                else
                {
                    AIState++;
                }

                /*Random rand = new Random();
                AIState = rand.Next(9);
                Console.WriteLine(AIState);*/

                switch (AIState)
                {
                    case 0:
                        RotateCW();
                        break;
                    case 1:
                        RotateCCW();
                        break;
                    case 2:
                        break;
                    case 3:
                        RotateCW();
                        break;
                    case 4:
                        RotateCCW();
                        break;
                    case 5:
                        RotateCCW();
                        break;
                    case 6:
                        RotateCW();
                        break;
                }
            }
            return base.Update(entities, gameTime);

        }
    }
}
