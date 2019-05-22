using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Input.InputListeners;
using MonoGame.Extended.Tiled;
using NVP.Entities;
using NVP.Entities.Enemies;
using NVP.Helpers;
using NVP.HUD;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NVP.Screen
{
    class GameSelectedLevel : GameScreen
    {
        // The tile map
        private TiledHelper tiledHelper;
        private CollisionHelper collisionHelper = new CollisionHelper();
        private Camera2D camera;

        private SpriteBatch spriteBatch;

        private InputManager InputManager;
        private List<Tower> towers = new List<Tower>();
        private List<Tower> TowerToRemove = new List<Tower>();

        private List<RectangleF> Caminos = new List<RectangleF>();
        private List<Intersection> intersections = new List<Intersection>();
        private List<InstersectionP> intersectionsProb = new List<InstersectionP>();
        private List<Spawner> spawners = new List<Spawner>();
        private List<CircleF> Final = new List<CircleF>();
        private List<Enemy> enemies = new List<Enemy>();
        private List<Enemy> enemiesToRemove = new List<Enemy>();

        float delta = 0f;
        private int TowerSelected = 0;
        private int Life = 100;
        private int Money = 6000;
        private int round = 1;
        private bool NextRound = false;


        private List<Bullet> bulletsToRemove;
        private List<Bullet> bulletsTower;

        public GameHudElements Hud { get; }

        private List<Bullet> bulletsEnemies;

        public GameSelectedLevel(Game game, int level) : base(game)
        {
            GeonBit.UI.UserInterface.Active.Dispose();
            GeonBit.UI.UserInterface.Initialize(Content);
            var viewport = new MonoGame.Extended.ViewportAdapters.DefaultViewportAdapter(GraphicsDevice);
            camera = new Camera2D(viewport);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            MoneyManager.Initialize(Money, false, Game, spriteBatch);

            tiledHelper = new TiledHelper(game, camera);
            ChargeLevel(level);
            Texture2D[] texture2Ds = new Texture2D[] { Content.Load<Texture2D>("Sprites/Towers/32"), Content.Load<Texture2D>("Sprites/Towers/32"), Content.Load<Texture2D>("Sprites/Towers/32"), Content.Load<Texture2D>("Sprites/Towers/32") };
            Action[] actions = new Action[] { () => { TowerSelected = 0; }, () => { TowerSelected = 1; }, () => { TowerSelected = 2; }, () => { TowerSelected = 3; } };
            InputManager = new InputManager(game);
            InputManager.MouseFunc = MouseFunc;
            InputManager.KeyboardFunc = KeyboardFunc;
            InputManager.MouseDragFunc = MouseDragFunc;
            Hud = new GameHudElements();
            Hud.TowerSelectionHUD(texture2Ds, actions);

            bulletsEnemies = new List<Bullet>();
            bulletsToRemove = new List<Bullet>();
            bulletsTower = new List<Bullet>();

        }

        public override void Initialize()
        {
            TiledMapObject[] tiledMapObjects = tiledHelper.Map.GetLayer<TiledMapObjectLayer>("Objetos").Objects.ToArray();
            foreach (var obj in tiledMapObjects)
            {
                if (obj.Name == "Camino")
                {
                    Caminos.Add(new RectangleF(obj.Position.ToPoint(), obj.Size));
                }
                if (obj.Name == "Interseccion")
                {
                    intersections.Add(new Intersection(obj.Position, new RectangleF(obj.Position, obj.Size), Convert.ToChar(obj.Properties.Where(x => x.Key == "Interseccion").First().Value)));
                }
                if (obj.Name == "Interseccion p")
                {
                    intersectionsProb.Add(new InstersectionP(obj.Position, new RectangleF(obj.Position, obj.Size), obj.Properties.Where(x => x.Key == "Interseccion").First().Value, Convert.ToSingle(obj.Properties.Where(x => x.Key == "probabilidad").First().Value)));
                }
                if (obj.Name == "spawner")
                {
                    spawners.Add(new Spawner(Game, spriteBatch, obj.Position, Convert.ToChar(obj.Properties.Where(x => x.Key == "Direccion").First().Value), obj.Properties.Where(x => x.Key == "cantidad enemigos").First().Value.Split(',').Select(x => int.Parse(x)).ToArray(), Convert.ToInt32(obj.Properties.Where(x => x.Key == "cantidad rondas").First().Value), true));
                }
                if (obj.Name == "fin")
                {
                    Final.Add(new CircleF(obj.Position.ToPoint(), 4));
                }
            }
            camera.LookAt(new RectangleF(tiledHelper.Map.GetLayer<TiledMapObjectLayer>("Camara").Objects.First().Position, tiledHelper.Map.GetLayer<TiledMapObjectLayer>("Camara").Objects.First().Size).Center);
        }
        private void MouseDragFunc(MouseEventArgs args)
        {
            camera.Move(-args.DistanceMoved);
        }

        private void MouseFunc(MouseEventArgs args)
        {
            bool clickcable = false;
            Point pos = (camera.ScreenToWorld(args.Position.ToVector2()) + new Vector2(-32, -32)).ToPoint();
            foreach (var item in Caminos)
            {
                clickcable |= item.Contains(pos);
            }
            foreach (var item in intersections)
            {
                clickcable |= item.Bounds.Contains(pos);
            }
            foreach (var item in intersectionsProb)
            {
                clickcable |= item.Bounds.Contains(pos);
            }
            if (args.Button == MouseButton.Left && !clickcable)
            {
                Vector2 Position = pos.ToVector2();
                Tower tower;
                if (MoneyManager.PayTower(TowerSelected, out tower, Position))
                {
                    tower.SetBullets(ref bulletsTower);
                    towers.Add(tower);
                    Hud.Money.Text = @"{{BOLD_GOLD}}   " + MoneyManager.Money;
                }

            }




        }

        private void KeyboardFunc(KeyboardEventArgs args)
        {

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
            foreach (var item in enemies)
            {
                item.Draw(gameTime);
            }
            foreach (var item in bulletsEnemies)
            {
                item.Draw(gameTime);
            }
            foreach (var item in bulletsTower)
            {
                item.Draw(gameTime);
            }
            spriteBatch.End();
            GeonBit.UI.UserInterface.Active.Draw(new SpriteBatch(GraphicsDevice));

        }

        public override void Update(GameTime gameTime)
        {
            GeonBit.UI.UserInterface.Active.Update(gameTime);

            tiledHelper.Update(gameTime);

            delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            foreach (var item in spawners)
            {
                item.Spawn(gameTime, ref enemies);

            }
            List<Entity> ent = new List<Entity>();
            ent = ent.Concat(enemies).ToList();
            ent = ent.Concat(towers).ToList();
            foreach (var item in towers)
            {
                if (item.Life < 0)
                {
                    TowerToRemove.Add(item);
                }
                item.GetEntities(ent.ToArray());
                item.Update(gameTime);

            }
            foreach (var item in enemies)
            {
                if (item.Life < 0)
                {
                    enemiesToRemove.Add(item);
                }
                item.GetEntities(ent.ToArray());
                foreach (var inter in intersections)
                {
                    inter.ChangeDirection(item);
                }
                foreach (var inter in intersectionsProb)
                {
                    inter.ChangeDirection(item);
                }
                foreach (var f in Final)
                {
                    if (f.Intersects(item.Collider))
                    {
                        enemiesToRemove.Add(item);
                        Life -= item.Dano;
                    }
                }
                item.Update(gameTime);
            }
            List<Bullet> bullets = new List<Bullet>();
            bullets = bullets.Concat(bulletsEnemies).ToList();
            bullets = bullets.Concat(bulletsTower).ToList();

            foreach (var item in bullets)
            {
                item.Update(gameTime);
                if (item.IsOutsideHisSpawner || item.Impacted)
                {
                    bulletsToRemove.Add(item);
                }

            }

            foreach (var item in TowerToRemove)
            {
                towers.Remove(item);
                item.Dispose();
            }
            foreach (var item in enemiesToRemove)
            {
                enemies.Remove(item);
                item.Dispose();
            }
            foreach (var item in bulletsToRemove)
            {
                if (bulletsEnemies.Contains(item))
                {
                    bulletsEnemies.Remove(item);
                }
                else
                {
                    bulletsTower.Remove(item);

                }
                item.Dispose();
            }
            collisionHelper.Initialize(new List<ICollisionableObject>().Concat(enemies).Concat(bullets).Concat(towers).ToList());
            TowerToRemove.Clear();
            enemiesToRemove.Clear();
            bulletsToRemove.Clear();
            collisionHelper.Update();
        }
    }
}
