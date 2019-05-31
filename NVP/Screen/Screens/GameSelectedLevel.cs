using GeonBit.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended;
using MonoGame.Extended.Input.InputListeners;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Timers;
using NVP.Entities;
using NVP.Entities.Enemies;
using NVP.Helpers;
using NVP.HUD;
using NVP.Screen.Screens;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NVP.Screen
{
    internal class GameSelectedLevel : GameScreen
    {
        #region Declaraciones

        // The tile map
        private TiledHelper tiledHelper;

        private CollisionHelper collisionHelper = new CollisionHelper();
        private Camera2D camera;
        private SpriteBatch spriteBatch;

        private InputManager InputManager;
        private List<Tower> towers = new List<Tower>();
        private List<Tower> TowerToRemove = new List<Tower>();

        private List<Camino> Caminos = new List<Camino>();
        private List<Intersection> intersections = new List<Intersection>();
        private List<InstersectionP> intersectionsProb = new List<InstersectionP>();
        private List<Spawner> spawners = new List<Spawner>();
        private List<CircleF> Final = new List<CircleF>();
        private List<Enemy> enemies = new List<Enemy>();
        private List<Enemy> enemiesToRemove = new List<Enemy>();
        private RectangleF WorldLimit;
        private float delta = 0f;
        private int TowerSelected = 0;
        private double Life = 15;
        private int Money = 2000;

        private bool clickcable = false;
        private bool paused = false;
        private bool win = false;

        private CountdownTimer TowerTimer = new CountdownTimer(TimeSpan.FromSeconds(2));

        private List<Bullet> bulletsToRemove;
        private List<Bullet> bulletsTower;

        public GameHudElements Hud { get; }
        public bool IsNormal { get; private set; } = true;

        private List<Bullet> bulletsEnemies;
        private float speed = 1;
        private Vector2 LastPosition;
        private bool ThreadNotInit = false;

        #endregion Declaraciones

        public GameSelectedLevel(Game game, int level) : base(game)
        {
            try
            {
                GeonBit.UI.UserInterface.Active.Dispose();
                GeonBit.UI.UserInterface.Initialize(Content);
                var viewport = new MonoGame.Extended.ViewportAdapters.DefaultViewportAdapter(GraphicsDevice);
                camera = new Camera2D(viewport);
                spriteBatch = new SpriteBatch(GraphicsDevice);
                MoneyManager.Initialize(Money, false, Game, spriteBatch);
                tiledHelper = new TiledHelper(game, camera);
                ChargeLevel(level);
                camera.MaximumZoom = 3;
                camera.MinimumZoom = 0.1f;
                InputManager = new InputManager(game);
                InputManager.MouseFunc = MouseFunc;
                InputManager.KeyboardFunc = KeyboardFunc;
                InputManager.MouseDragFunc = MouseDragFunc;
                InputManager.MouseScrollFunc = MouseScrollFunc;
                Hud = new GameHudElements();
                UserInterface.Active.UseRenderTarget = true;
                UserInterface.Active.IncludeCursorInRenderTarget = false;
                MediaPlayer.Stop();
                MediaPlayer.Play(Content.Load<Song>(@"Audio/Songs/determination"));
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Volume = 83;
                List<GeonBit.UI.Entities.RadioButton> radioButtons = new List<GeonBit.UI.Entities.RadioButton>();

                var Panel = new GeonBit.UI.Entities.Panel(new Vector2(GraphicsDevice.Viewport.Bounds.Width, GraphicsDevice.Viewport.Bounds.Height * 0.1f), GeonBit.UI.Entities.PanelSkin.None, GeonBit.UI.Entities.Anchor.BottomCenter);
                if (IsNormal)
                {
                    Panel.AddChild(new GeonBit.UI.Entities.Button("Aldeano") { OnClick = (e) => { TowerSelected = 2; clickcable = true; }, ToolTipText = "tecla 1", Size = new Vector2(GraphicsDevice.Viewport.Bounds.Width / 4, GraphicsDevice.Viewport.Bounds.Height * 0.1f), Anchor = GeonBit.UI.Entities.Anchor.AutoInlineNoBreak, Offset = new Vector2(-10, -30) });
                    Panel.AddChild(new GeonBit.UI.Entities.Button("Arquero") { OnClick = (e) => { TowerSelected = 1; clickcable = true; }, ToolTipText = "tecla 2", Size = new Vector2(GraphicsDevice.Viewport.Bounds.Width / 4, GraphicsDevice.Viewport.Bounds.Height * 0.1f), Anchor = GeonBit.UI.Entities.Anchor.AutoInlineNoBreak, Offset = new Vector2(-10, -30) });
                    Panel.AddChild(new GeonBit.UI.Entities.Button("Caballero") { OnClick = (e) => { TowerSelected = 0; clickcable = true; }, ToolTipText = "tecla 3", Size = new Vector2(GraphicsDevice.Viewport.Bounds.Width / 4, GraphicsDevice.Viewport.Bounds.Height * 0.1f), Anchor = GeonBit.UI.Entities.Anchor.AutoInlineNoBreak, Offset = new Vector2(-10, -30) });
                    Panel.AddChild(new GeonBit.UI.Entities.Button("Sacerdote") { OnClick = (e) => { TowerSelected = 3; clickcable = true; }, ToolTipText = "tecla 4", Size = new Vector2(GraphicsDevice.Viewport.Bounds.Width / 4, GraphicsDevice.Viewport.Bounds.Height * 0.1f), Anchor = GeonBit.UI.Entities.Anchor.AutoInlineNoBreak, Offset = new Vector2(-10, -30) });
                }
                else
                {
                    Panel.AddChild(new GeonBit.UI.Entities.Button("Zombie") { OnClick = (e) => { TowerSelected = 7; clickcable = true; }, ToolTipText = "tecla 1", Size = new Vector2(GraphicsDevice.Viewport.Bounds.Width / 4, GraphicsDevice.Viewport.Bounds.Height * 0.1f), Anchor = GeonBit.UI.Entities.Anchor.AutoInlineNoBreak, Offset = new Vector2(-10, -30) });
                    Panel.AddChild(new GeonBit.UI.Entities.Button("Fantasma") { OnClick = (e) => { TowerSelected = 5; clickcable = true; }, ToolTipText = "tecla 2", Size = new Vector2(GraphicsDevice.Viewport.Bounds.Width / 4, GraphicsDevice.Viewport.Bounds.Height * 0.1f), Anchor = GeonBit.UI.Entities.Anchor.AutoInlineNoBreak, Offset = new Vector2(-10, -30) });
                    Panel.AddChild(new GeonBit.UI.Entities.Button("Hombre Lobo") { OnClick = (e) => { TowerSelected = 6; clickcable = true; }, ToolTipText = "tecla 3", Size = new Vector2(GraphicsDevice.Viewport.Bounds.Width / 4, GraphicsDevice.Viewport.Bounds.Height * 0.1f), Anchor = GeonBit.UI.Entities.Anchor.AutoInlineNoBreak, Offset = new Vector2(-10, -30) });
                    Panel.AddChild(new GeonBit.UI.Entities.Button("Cultista") { OnClick = (e) => { TowerSelected = 4; clickcable = true; }, ToolTipText = "tecla 4", Size = new Vector2(GraphicsDevice.Viewport.Bounds.Width / 4, GraphicsDevice.Viewport.Bounds.Height * 0.1f), Anchor = GeonBit.UI.Entities.Anchor.AutoInlineNoBreak, Offset = new Vector2(-10, -30) });
                }
                GeonBit.UI.Entities.Panel panelSpeed = new GeonBit.UI.Entities.Panel(new Vector2(GraphicsDevice.Viewport.Bounds.Width, GraphicsDevice.Viewport.Bounds.Height * 0.1f), GeonBit.UI.Entities.PanelSkin.None, GeonBit.UI.Entities.Anchor.BottomCenter, offset: new Vector2(0, -GraphicsDevice.Viewport.Bounds.Height * 0.12f));
                panelSpeed.AddChild(new GeonBit.UI.Entities.RadioButton("Velocidad x1") { OnValueChange = (e) => { speed = 1; clickcable = true; }, ToolTipText = "tecla q", Size = new Vector2(GraphicsDevice.Viewport.Bounds.Width / 5, GraphicsDevice.Viewport.Height * 0.19f), Anchor = GeonBit.UI.Entities.Anchor.AutoInline, Offset = new Vector2(0, -GraphicsDevice.Viewport.Bounds.Height * 0.35f) });
                panelSpeed.AddChild(new GeonBit.UI.Entities.RadioButton("Velocidad x2") { OnValueChange = (e) => { speed = 2; clickcable = true; }, ToolTipText = "tecla r", Size = new Vector2(GraphicsDevice.Viewport.Bounds.Width / 5, GraphicsDevice.Viewport.Height * 0.19f), Anchor = GeonBit.UI.Entities.Anchor.AutoInline, Offset = new Vector2(0, -GraphicsDevice.Viewport.Bounds.Height * 0.35f) });
                panelSpeed.AddChild(new GeonBit.UI.Entities.RadioButton("Velocidad x3") { OnValueChange = (e) => { speed = 3; clickcable = true; }, ToolTipText = "tecla t", Size = new Vector2(GraphicsDevice.Viewport.Bounds.Width / 5, GraphicsDevice.Viewport.Height * 0.19f), Anchor = GeonBit.UI.Entities.Anchor.AutoInline, Offset = new Vector2(0, -GraphicsDevice.Viewport.Bounds.Height * 0.35f) });
                panelSpeed.AddChild(new GeonBit.UI.Entities.RadioButton("Velocidad x4") { OnValueChange = (e) => { speed = 4; clickcable = true; }, ToolTipText = "tecla y", Size = new Vector2(GraphicsDevice.Viewport.Bounds.Width / 5, GraphicsDevice.Viewport.Height * 0.19f), Anchor = GeonBit.UI.Entities.Anchor.AutoInline, Offset = new Vector2(0, -GraphicsDevice.Viewport.Bounds.Height * 0.35f) });
                Hud.TowerSelectionHUD(GraphicsDevice.Viewport.Bounds, Life, Game, Panel, panelSpeed);
                bulletsEnemies = new List<Bullet>();
                bulletsToRemove = new List<Bullet>();
                bulletsTower = new List<Bullet>();
                Hud.PauseIcon.OnClick = (e) =>
                {
                    Pause();
                };
                List<GeonBit.UI.Entities.Button> buttons = new List<GeonBit.UI.Entities.Button>();
                buttons.Add(new GeonBit.UI.Entities.Button("Reintentar") { OnClick = (e) => { UserInterface.Active.Clear(); ScreenManager.LoadScreen(new GameSelectedLevel(game, level)); } });
                buttons.Add(new GeonBit.UI.Entities.Button("Seleccion De Niveles") { OnClick = (e) => { UserInterface.Active.Clear(); ScreenManager.LoadScreen(new GameSelectScreen(game)); } });
                buttons.Add(new GeonBit.UI.Entities.Button("Menu Principal") { OnClick = (e) => { UserInterface.Active.Clear(); ScreenManager.LoadScreen(new MainMenuScreen(game)); } });

                Hud.PauseMenu(game, buttons);
                List<GeonBit.UI.Entities.Button> winButtons = new List<GeonBit.UI.Entities.Button>();
                if (!(level == 4 || level == 8))
                {
                    winButtons.Add(new GeonBit.UI.Entities.Button("Siguiente Nivel") { OnClick = (e) => { UserInterface.Active.Clear(); ScreenManager.LoadScreen(new GameSelectedLevel(game, level + 1)); } });
                }
                winButtons.Add(new GeonBit.UI.Entities.Button("Seleccion de Niveles") { OnClick = (e) => { UserInterface.Active.Clear(); ScreenManager.LoadScreen(new GameSelectScreen(game)); } });
                winButtons.Add(new GeonBit.UI.Entities.Button("Menu Principal") { OnClick = (e) => { UserInterface.Active.Clear(); ScreenManager.LoadScreen(new MainMenuScreen(game)); } });
                Hud.WinMenu(game, winButtons);
            }
            catch
            {
            }
        }

        public override void Initialize()
        {
            camera.Zoom = 1.2f;

            TiledMapObject[] tiledMapObjects = tiledHelper.Map.GetLayer<TiledMapObjectLayer>("Objetos").Objects.ToArray();
            foreach (var obj in tiledMapObjects)
            {
                if (obj.Name == "Camino")
                {
                    Caminos.Add(new Camino(obj.Position, new RectangleF(obj.Position, obj.Size)));
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
                    spawners.Add(new Spawner(Game, spriteBatch, obj.Position, Convert.ToChar(obj.Properties.Where(x => x.Key == "Direccion").First().Value), obj.Properties.Where(x => x.Key == "cantidad enemigos").First().Value.Split(',').Select(x => int.Parse(x)).ToArray(), Convert.ToInt32(obj.Properties.Where(x => x.Key == "cantidad rondas").First().Value), IsNormal, ref bulletsTower));
                }
                if (obj.Name == "fin")
                {
                    Final.Add(new CircleF(obj.Position.ToPoint(), 4));
                }
            }
            WorldLimit = new RectangleF(tiledHelper.Map.GetLayer<TiledMapObjectLayer>("Camara").Objects.First().Position, tiledHelper.Map.GetLayer<TiledMapObjectLayer>("Camara").Objects.First().Size);
            camera.LookAt(WorldLimit.Center);
        }

        private void MouseDragFunc(MouseEventArgs args)
        {
            camera.Position += Vector2.Transform(-args.DistanceMoved, Matrix.CreateRotationZ(-camera.Rotation));
            Vector2 cameraWorldMin = Vector2.Transform(Vector2.Zero, camera.GetInverseViewMatrix());
            Vector2 cameraSize = new Vector2(GraphicsDevice.Viewport.Bounds.Width, GraphicsDevice.Viewport.Bounds.Height) / camera.Zoom;
            Vector2 limitWorldMin = new Vector2(WorldLimit.Left, WorldLimit.Top);
            Vector2 limitWorldMax = new Vector2(WorldLimit.Right, WorldLimit.Bottom);
            Vector2 positionOffset = camera.Position - cameraWorldMin;
            camera.Position = Vector2.Clamp(cameraWorldMin, limitWorldMin, limitWorldMax - cameraSize) + positionOffset;
        }

        private void MouseScrollFunc(MouseEventArgs args)
        {
            try
            {
                if (camera.MaximumZoom >= camera.Zoom + (args.ScrollWheelDelta / 1000f) && camera.MinimumZoom <= camera.Zoom - (args.ScrollWheelDelta / 1000f))
                    camera.Zoom += args.ScrollWheelDelta / 1000f;
                float minZoomX = GraphicsDevice.Viewport.Bounds.Width / WorldLimit.Width;
                float minZoomY = GraphicsDevice.Viewport.Bounds.Height / WorldLimit.Height;
                camera.Zoom = MathHelper.Max(camera.Zoom, MathHelper.Max(minZoomX, minZoomY));
            }
            catch
            {

            }

        }

        private void MouseFunc(MouseEventArgs args)
        {
            if (!paused)
            {
                if (!clickcable)
                    clickcable = false;
                Point2 pos = camera.ScreenToWorld(args.Position.ToVector2());
                foreach (var item in Caminos)
                {
                    clickcable |= item.Bounds.Contains(pos);
                }
                foreach (var item in intersections)
                {
                    clickcable |= item.Bounds.Contains(pos);
                }
                foreach (var item in intersectionsProb)
                {
                    clickcable |= item.Bounds.Contains(pos);
                }
                foreach (var item in towers)
                {
                    clickcable |= item.Collider.Contains(pos);
                }
                if (args.Button == MonoGame.Extended.Input.InputListeners.MouseButton.Left && !clickcable)
                {
                    Vector2 Position = pos;
                    Tower tower;
                    if (TowerTimer.State == TimerState.Completed)
                    {
                        if (MoneyManager.PayTower(TowerSelected, out tower, Position))
                        {
                            tower.SetBullets(ref bulletsTower);
                            towers.Add(tower);
                            TowerTimer.Restart();
                            clickcable = false;
                        }
                    }
                }
                clickcable = false;
            }
        }

        private void KeyboardFunc(KeyboardEventArgs args)
        {
            if (!paused)
            {
                if (args.Key == Microsoft.Xna.Framework.Input.Keys.D1)
                {
                    TowerSelected = IsNormal ? 2 : 7; ;
                }
                else if (args.Key == Microsoft.Xna.Framework.Input.Keys.D2)
                {
                    TowerSelected = IsNormal ? 1 : 5; ;
                }
                else if (args.Key == Microsoft.Xna.Framework.Input.Keys.D3)
                {
                    TowerSelected = IsNormal ? 0 : 6; ;
                }
                else if (args.Key == Microsoft.Xna.Framework.Input.Keys.D4)
                {
                    TowerSelected = IsNormal ? 3 : 4; ;
                }
                else if (args.Key == Microsoft.Xna.Framework.Input.Keys.Q) { speed = 1; }
                else if (args.Key == Microsoft.Xna.Framework.Input.Keys.R) { speed = 2; }
                else if (args.Key == Microsoft.Xna.Framework.Input.Keys.T) { speed = 3; }
                else if (args.Key == Microsoft.Xna.Framework.Input.Keys.Y) { speed = 4; }
            }
            if (args.Key == Microsoft.Xna.Framework.Input.Keys.D6)
            {
                Pause();
            }
        }

        private void ChargeLevel(int level)
        {
            tiledHelper.Initialize(string.Format("Mapas/Level{0}", level));
            if (level > 4)
            {
                IsNormal = false;
            }
        }

        public void GameDraw(GameTime gameTime)
        {
            GeonBit.UI.UserInterface.Active.Draw(new SpriteBatch(GraphicsDevice));
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
        }

        public void GameUpdate(GameTime gameTime)
        {
            gameTime = new GameTime(gameTime.ElapsedGameTime, TimeSpan.FromSeconds(gameTime.GetElapsedSeconds() * speed)); ;
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
                    MoneyManager.Money += 100;
                }
                item.GetEntities(towers.ToArray());
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
            List<Bullet> bullets = new List<Bullet>();
            bullets = bullets.Concat(bulletsEnemies).ToList();
            bullets = bullets.Concat(bulletsTower).ToList();

            System.Threading.Thread.CurrentThread.IsBackground = true;

            foreach (var item in bullets)
            {
                item.Update(gameTime);
                if (item.IsOutsideHisSpawner || item.Impacted)
                {
                    bulletsToRemove.Add(item);
                }
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
            bulletsToRemove.Clear();

            if (Life <= 0)
            {
                UserInterface.Active.Clear();
                ScreenManager.LoadScreen(new GameoverScreen(Game));
            }
            var IsDone = false;
            foreach (var spawn in spawners)
            {
                IsDone |= spawn.IsDone;
            }
            if (Life > 0 && IsDone && enemies.Count == 0)
            {
                Win();
            }
            collisionHelper.Initialize(new List<ICollisionableObject>().Concat(enemies).Concat(bullets).Concat(towers).ToList());
            TowerToRemove.Clear();
            enemiesToRemove.Clear();

            TowerTimer.Update(gameTime);
            collisionHelper.Update();
            Hud.Refresh();
            Hud.RefreshLife(Life);
        }

        public void PausedUpdate(GameTime gameTime)
        {
            GeonBit.UI.UserInterface.Active.Update(gameTime);
        }

        public void PausedDraw(GameTime gameTime)
        {
            UserInterface.Active.Draw(spriteBatch);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            GameDraw(gameTime);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.FillRectangle(GraphicsDevice.Viewport.Bounds, Color.Black * 0.4f);
            spriteBatch.End();
            UserInterface.Active.DrawMainRenderTarget(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            if (!win)
            {
                if (paused)
                {
                    PausedUpdate(gameTime);
                }
                else
                {
                    GameUpdate(gameTime);
                }
            }
            else
            {
                WinUpdate(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (!win)
            {
                if (paused)
                {
                    PausedDraw(gameTime);
                }
                else
                {
                    GameDraw(gameTime);
                }
            }
            else
            {
                WinDraw(gameTime);
            }
        }

        private void WinDraw(GameTime gameTime)
        {
            UserInterface.Active.Draw(spriteBatch);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            GameDraw(gameTime);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.FillRectangle(GraphicsDevice.Viewport.Bounds, Color.Black * 0.4f);
            spriteBatch.End();
            UserInterface.Active.DrawMainRenderTarget(spriteBatch);
        }

        private void WinUpdate(GameTime gameTime)
        {
            UserInterface.Active.Update(gameTime);
        }

        private void Pause()
        {
            paused = !paused;
            if (paused)
            {
                GeonBit.UI.UserInterface.Active.AddEntity(Hud.PausedMenu);
            }
            else
            {
                GeonBit.UI.UserInterface.Active.RemoveEntity(Hud.PausedMenu);
            }
        }

        private void Win()
        {
            win = true;
            if (win)
            {
                UserInterface.Active.AddEntity(Hud.WinMenuPanel);
            }
        }
    }
}