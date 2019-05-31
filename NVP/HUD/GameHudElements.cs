using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NVP.Helpers;
using System.Collections.Generic;

namespace NVP.HUD
{
    public class GameHudElements
    {
        public RichParagraph Money { get; private set; }
        public ProgressBar Life { get; private set; }
        public Image PauseIcon { get; private set; }
        public Panel PausedMenu { get; private set; }
        public Panel WinMenuPanel { get; private set; }

        public void TowerSelectionHUD(Rectangle Screen, double InitialLife, Game game, Panel buttons, Panel radioButtons)
        {
            UserInterface.Active.AddEntity(buttons);
            UserInterface.Active.AddEntity(radioButtons);

            UserInterface.Active.AddEntity(Life = new ProgressBar(0, (uint)InitialLife, new Vector2(Screen.Width * 0.20f, Screen.Height * 0.1f), Anchor.TopLeft));
            RichParagraphStyleInstruction.AddInstruction("BOLD_GOLD", new RichParagraphStyleInstruction(fillColor: Color.Gold, fontStyle: FontStyle.Bold));
            UserInterface.Active.AddEntity(new Image(game.Content.Load<Texture2D>("Sprites/Money"), new Vector2(32, 32), anchor: Anchor.TopLeft, offset: new Vector2(0, Screen.Height * 0.12f)));
            UserInterface.Active.AddEntity(Money = new RichParagraph(@"{{BOLD_GOLD}}   " + MoneyManager.Money, Anchor.TopLeft, offset: new Vector2(20, Screen.Height * 0.12f)));

            Life.Locked = true;
            PauseIcon = new Image(game.Content.Load<Texture2D>("GeonBit.UI/themes/hd/textures/icons/Book"), new Vector2(32, 32), ImageDrawMode.Stretch, Anchor.TopRight);

            UserInterface.Active.AddEntity(PauseIcon);
        }

        public void PauseMenu(Game game, List<Button> buttons)
        {
            var viewport = new MonoGame.Extended.ViewportAdapters.DefaultViewportAdapter(game.GraphicsDevice);
            PausedMenu = Menu.ReturnCreateMenu("Pausa", viewport.BoundingRectangle.Width / 2, viewport.BoundingRectangle.Height / 1.5f, buttons);
        }

        public void WinMenu(Game game, List<Button> buttons)
        {
            var viewport = new MonoGame.Extended.ViewportAdapters.DefaultViewportAdapter(game.GraphicsDevice);
            WinMenuPanel = Menu.ReturnCreateMenu("GANASTE", viewport.BoundingRectangle.Width / 2, viewport.BoundingRectangle.Height / 1.5f, buttons);
        }

        public void Refresh()
        {
            Money.Text = @"{{BOLD_GOLD}}   " + MoneyManager.Money;
        }

        public void RefreshLife(double life)
        {
            Life.Value = (int)(Life.Max * (life / (float)Life.Max));
        }

        private bool ChangePause(bool p)
        {
            return !p;
        }
    }
}