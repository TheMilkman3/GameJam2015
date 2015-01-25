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
        public bool Reset = false;

        public new void Initialize(Animation anim, float scale, Vector2 position)
        {
            base.Initialize(anim, scale, position);
            Solid = true;
        }

        public override List<Entity> Update(List<Entity> entities, GameTime gameTime)
        {
            List<Entity> collided_entities = base.Update(entities, gameTime);
            if (collided_entities.Count != 0)
            {
                foreach (Entity e in collided_entities)
                {
                    if (e.LevelGoal)
                    {
                        Reset = true;
                    }
                    if (e.Deadly)
                    {
                        EndGame = true;
                    }
                }
            }
            return collided_entities;
        }
    }
}
