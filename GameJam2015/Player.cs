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
        public bool EndGame = false;
        ControllerInput p1, p2, p3, p4;

        public new void Initialize(Animation anim, float scale, Vector2 position, Game1 game)
        {
            base.Initialize(anim, scale, position);
            Solid = true;
            p1 = new ControllerInput(game, PlayerIndex.One);
            //p2 = new ControllerInput(game, PlayerIndex.Two);
            //p3 = new ControllerInput(game, PlayerIndex.Three);
            //p4 = new ControllerInput(game, PlayerIndex.Four);
        }

        public override List<Entity> Update(List<Entity> entities, GameTime gameTime)
        {
            p1.Update(gameTime);
            //p2.Update(gameTime);
            //p3.Update(gameTime);
            //p4.Update(gameTime);

            List<Entity> collided_entities = base.Update(entities, gameTime);
            if (collided_entities.Count != 0)
            {
                foreach (Entity e in collided_entities)
                {
                    if (e.LevelGoal)
                    {
                        EndGame = true;
                    }
                }
            }
            return collided_entities;
        }
    }
}
