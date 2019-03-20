using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeonBit.UI;
using GeonBit.UI.Entities;
using MonoGame.Extended;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NVP.Entities;

namespace NVP.HUD
{
    public class GameHudElements
    {
        public ProgressBar Life { get; set; }
        public Panel ScreenTower { get; set; }
        public 

        void CreateLifeBar()
        {
            Life = new ProgressBar(0, 100, Anchor.TopRight, new Vector2(-50,0));
            Life.Value = 100;
            
        }
        public ProgressBar CreateObjectLifeBar()
        {
            throw new NotImplementedException();
        }
        public void CreateScreenTowerSelection(IList<Texture2D> images, IList<Action> actions)
        {
            Panel panel = new Panel(new Vector2(200, 40), PanelSkin.Fancy, Anchor.TopLeft);
            foreach (var image in images)
            {
                var b = new Button() {
                    OnClick = (Entity e) => {actions.ElementAt(images.IndexOf(image)); }
                };
                panel.AddChild(b.AddChild(new Image(image, anchor: Anchor.AutoInlineNoBreak)));
            }
            ScreenTower = panel;
        }
        public void CreateOptionsButtons(Image select)
        {
            Button button = new Button();
            button.AddChild(select);
        }
        public void CreateItemBag(IList<Texture2D> images, IList<Action> actions)
        {
            Panel panel = new Panel(new Vector2(100, 40), PanelSkin.Fancy, Anchor.BottomRight);
            foreach (var image in images)
            {
                var b = new Button()
                {
                    OnClick = (Entity e) => { actions.ElementAt(images.IndexOf(image)); }
                };
                panel.AddChild(b.AddChild(new Image(image, anchor: Anchor.AutoInlineNoBreak)));
            }
        }

    }
}
