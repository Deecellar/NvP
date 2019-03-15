using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;
using MonoGame.Extended;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace NVP.Helpers
{
    class TiledHelper
    {
        public TiledMap Map { get; set; }
        public Game Game { get; }
        public ContentManager Content => Game.Content;
        public GraphicsDevice GraphicsDevice => Game.GraphicsDevice;
        public GameServiceContainer Services => Game.Services;
        private TiledMapRenderer maprenderer;
        private Camera2D camara2d;

        public TiledHelper(Game game, Camera2D camera)
        {
            Game = game;
            camara2d = camera;
        }

        public void Initialize(string path)
        {
            Map = Content.Load<TiledMap>(path);
            maprenderer = new TiledMapRenderer(GraphicsDevice);

        }
        public void Update(GameTime gameTime)
        {
            maprenderer.Update(Map, gameTime);
        }
        public void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Blue);
            maprenderer.Draw(Map, camara2d.GetViewMatrix());
        }
    }
}