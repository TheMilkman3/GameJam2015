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
