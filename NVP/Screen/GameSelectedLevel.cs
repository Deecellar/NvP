using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;

namespace NVP.Screen
{
    class GameSelectedLevel : GameScreen
    {
        // The tile map
        private TiledMap map;
        // The renderer for the map
        private TiledMapRenderer mapRenderer;

        private Camera2D camera;

        private SpriteBatch spriteBatch;

        public GameSelectedLevel(Game game, int level) : base(game)
        {
            var viewport = new MonoGame.Extended.ViewportAdapters.BoxingViewportAdapter(Game.Window, GraphicsDevice, Game.Window.ClientBounds.X, Game.Window.ClientBounds.Y);
            camera = new Camera2D(viewport);
            spriteBatch = new SpriteBatch(GraphicsDevice); 
            
            ChargeLevel(level);
        }

        private void ChargeLevel(int level)
        {
            map = Content.Load<TiledMap>("Maps/level"+level);
            mapRenderer = new TiledMapRenderer(GraphicsDevice);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(transformMatrix: camera.GetViewMatrix(), samplerState: SamplerState.PointClamp);
            mapRenderer.Draw(map, camera.GetViewMatrix());
        }

        public override void Update(GameTime gameTime)
        {
            mapRenderer.Update(map, gameTime);
        }
    }
}
