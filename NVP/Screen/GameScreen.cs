using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVP.Screen
{
    public abstract class GameScreen : Screen
    {
        protected GameScreen(Game game)
        {
            Game = game;
        }

        public Game Game { get; }
        public ContentManager Content => Game.Content;
        public GraphicsDevice GraphicsDevice => Game.GraphicsDevice;
        public GameServiceContainer Services => Game.Services;
    }
}

