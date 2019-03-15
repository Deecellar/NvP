using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Input;
using MonoGame.Extended.Input.InputListeners;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;
using NVP.Helpers;
using NVP.Entities;

namespace NVP.Screen
{
    class GameSelectedLevel : GameScreen
    {
        // The tile map
        private TiledHelper tiledHelper;

        private Camera2D camera;

        private SpriteBatch spriteBatch;

        private InputManager InputManager;
        private List<Tower> towers = new List<Tower>();

        float delta = 0f;

        public GameSelectedLevel(Game game, int level) : base(game)
        {

            var viewport = new MonoGame.Extended.ViewportAdapters.BoxingViewportAdapter(Game.Window, GraphicsDevice, Game.Window.ClientBounds.X, Game.Window.ClientBounds.Y);
            camera = new Camera2D(viewport);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            tiledHelper = new TiledHelper(game, camera);
            ChargeLevel(level);
            InputManager = new InputManager(game);
            InputManager.MouseFunc = MouseFunc;
            InputManager.KeyboardFunc = KeyboardFunc;
            InputManager.MouseDragFunc = MouseDragFunc;
        }

        private void MouseDragFunc(MouseEventArgs args)
        {
          if(args.Button == MouseButton.Left)
            {
                camera.Move(-1 * args.DistanceMoved);
            }   
        }

        private void MouseFunc(MouseEventArgs args)
        {
             if(args.Button == MouseButton.Left)
            {
                Vector2 Position = camera.ScreenToWorld(args.Position.ToVector2());
                Tower tower = new Tower(Game, Position, Content.Load<Texture2D>("Sprites/Towers/32"));
                towers.Add(tower);
            }
        }

        private void KeyboardFunc(KeyboardEventArgs args)
        {
            const float movementSpeed = 800;
            if (args.Key == Keys.Up)
                camera.Move(new Vector2(0, -movementSpeed * delta));
            else if (args.Key == Keys.Down)
                camera.Move(new Vector2(0, movementSpeed * delta));
            else if (args.Key == Keys.Right)
                camera.Move(new Vector2(movementSpeed * delta, 0));
            else if (args.Key == Keys.Left)
                camera.Move(new Vector2(-movementSpeed * delta, 0));
        }

        private void ChargeLevel(int level)
        {
            tiledHelper.Initialize(string.Format("Mapas/Level{0}", level));

        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(transformMatrix: camera.GetViewMatrix(), samplerState: SamplerState.PointClamp);
            tiledHelper.Draw(gameTime);
            foreach (var item in towers)
            {
                item.Draw(gameTime);
            }
            spriteBatch.End();

        }

        public override void Update(GameTime gameTime)
        {
            tiledHelper.Update(gameTime);

            delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            foreach (var item in towers)
            {
                item.Update(gameTime);
            }




        }
    }
}
