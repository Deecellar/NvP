using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace NVP.Screen.Screens
{
    public class OptionsScreen : GameScreen
    {
        public OptionsScreen(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            List<Button> buttons = new List<Button>();
            buttons.Add(new Button("Controles") { OnClick = btn => { } });
            buttons.Add(new Button("Audio") { OnClick = btn => { } });
            buttons.Add(new Button("Video") { OnClick = btn => { } });
            buttons.Add(new Button("Volver")
            {
                OnClick = btn =>
                {
                    UserInterface.Active.Clear();
                    ScreenManager.LoadScreen(new MainMenuScreen(Game), new Transitions.FadeTransition(GraphicsDevice, Color.Black, 4));
                }
            });
            HUD.Menu.CreateMenu("Opciones", 400, 400, buttons);
        }

        public override void Draw(GameTime gameTime)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}