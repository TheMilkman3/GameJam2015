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
        public Vector2 Scale;

        public void Initialize(Texture2D texture, float scaleX, float scaleY, Vector2 position)
        {
            Sprite = texture;
            Scale = new Vector2(scaleX, scaleY);
            Position = position;
            Velocity = Vector2.Zero;
        }

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
