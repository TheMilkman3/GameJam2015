using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJam2015
{
    public class Player : Entity
    {
        // State of the player
        public bool Active;

        // Amount of hit points that player has
        public int Health;

        //public void Initialize(Texture2D texture, Vector2 position)
        //{
        //    // Set the player to be active
        //    Active = true;

        //    // Set the player health
        //    Health = 100;
        //}
    }
}
