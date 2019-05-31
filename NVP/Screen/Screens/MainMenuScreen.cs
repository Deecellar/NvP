using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using NVP.HUD;
using System.Collections.Generic;

namespace NVP.Screen.Screens
{
    internal class MainMenuScreen : GameScreen
    {
        public MainMenuScreen(Game game) : base(game)
        {
            MediaPlayer.Play(Content.Load<Song>(@"Audio/Songs/rim_260kbps"));
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 83;
        }

        public override void Initialize()
        {
            base.Initialize();
            UserInterface.Active.AddEntity(new Image(Content.Load<Texture2D>("Sprites/unknown")));

            List<Button> buttons = new List<Button>();
            buttons.Add(new Button("Empezar")
            {
                OnClick = btn =>
                                {
                                    UserInterface.Active.Clear();
                                    ScreenManager.LoadScreen(new GameSelectScreen(Game), new Transitions.ExpandTransition(GraphicsDevice, Color.Black, 3.5f));
                                }
            });
            buttons.Add(new Button("Opciones")
            {
                OnClick = btn =>
                {
                    UserInterface.Active.Clear();

                    ScreenManager.LoadScreen(new OptionsScreen(Game));
                }
            });
            buttons.Add(new Button("Salir")
            {
                OnClick = btn =>
                {
                    Game.Exit();
                }
            });
            var viewport = new MonoGame.Extended.ViewportAdapters.DefaultViewportAdapter(Game.GraphicsDevice);
            Menu.CreateMenu("Menu", viewport.BoundingRectangle.Width / 2, viewport.BoundingRectangle.Height / 1.5f, buttons);
        }

        public override void Draw(GameTime gameTime)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}