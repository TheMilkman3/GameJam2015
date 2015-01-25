using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJam2015
{
    public class Room : Entity
    {
        public List<Entity> roomItems;

        public Room()
        {
            roomItems = new List<Entity>();
        }

        /*public void Initialize(Texture2D sprite, float scale)
        {
            Scale = scale;
            Sprite = sprite;

            roomItems = new List<Entity>();
        }*/

        /*public override float Width()
        {
            return Sprite.Width * Scale;
        }

        public override float Height()
        {
            return Sprite.Height * Scale;
        }*/

        public void addItem(List<Entity> list)
        {
            Console.WriteLine(list.Count);
            foreach (Entity e in list)
            {
                roomItems.Add(e);
            }
        }
    }
}
