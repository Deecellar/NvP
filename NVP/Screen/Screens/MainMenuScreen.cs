using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NVP.HUD;
using Microsoft.Xna.Framework;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework.Graphics;
using GeonBit.UI;

namespace NVP.Screen.Screens
{
    class MainMenuScreen : GameScreen
    {

        public MainMenuScreen(Game game) : base(game)
        {
        }
        public override void Initialize()
        {
            base.Initialize();
            List<Button> buttons = new List<Button>();
            buttons.Add(new Button("Empezar")
            {
                OnClick = btn =>
                                {
                                    UserInterface.Active.Clear();
                                    ScreenManager.LoadScreen(new GameSelectScreen(Game), new Transitions.ExpandTransition(GraphicsDevice, Color.Black,3.5f));
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
            var viewport =new MonoGame.Extended.ViewportAdapters.DefaultViewportAdapter(Game.GraphicsDevice);
            Menu.CreateMenu("Menu", viewport.BoundingRectangle.Width /2, viewport.BoundingRectangle.Height / 1.5f, buttons);
        }
        public override void Draw(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
