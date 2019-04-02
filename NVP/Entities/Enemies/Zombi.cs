﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NVP.Entities.Enemies
{
    class Zombi : Enemy
    {
        public Zombi(Game game, Vector2 position, Texture2D texture, SpriteBatch sprite) : base(game, position, texture, sprite)
        {
        }
    }
}
