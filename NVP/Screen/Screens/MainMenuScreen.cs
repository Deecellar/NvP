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
                                    ScreenManager.LoadScreen(new GameSelectScreen(Game), new Transitions.ExpandTransition(GraphicsDevice, Color.Black,5));
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
            Menu.CreateMenu("Menu", 400, 400, buttons);
        }
        public override void Draw(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
