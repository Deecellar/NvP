using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;

namespace NVP.HUD
{
    public static class Menu
    {

        public static void  CreateMenu(string title, float x, float y, List<Button> buttons, Anchor anchor = Anchor.Center, PanelSkin panelSkin = PanelSkin.Fancy, float offsetX = 0f, float offsetY = 0f)
        {
            Vector2 vector2 = new Vector2(offsetX, offsetY);
            Panel panel = new Panel(new Vector2(x, y), panelSkin, anchor, vector2);
            panel.PanelOverflowBehavior = PanelOverflowBehavior.VerticalScroll;
            panel.AddChild(new Header(title));
            panel.AddChild(new HorizontalLine());
            foreach (Button button in buttons)
            {
                panel.AddChild(button);

            }
            UserInterface.Active.AddEntity(panel);
        }
        public static void CreateSelectionMenu(string title, float x, float y, List<Button> levelsN, List<Button> levelsP, Anchor anchor = Anchor.Center, PanelSkin panelSkin = PanelSkin.Fancy, float offsetX = 0, float offsetY = 0f)
        {
            Vector2 vector2 = new Vector2(offsetX, offsetY);
            Panel panel = new Panel(new Vector2(x, y), panelSkin, anchor, vector2);


            PanelTabs tabs = new PanelTabs(panelSkin);
            UserInterface.Active.AddEntity(new Header("SELECCION DE NIVELES", Anchor.TopCenter, new Vector2(0,16)));
            var tab1 = tabs.AddTab("Normal", panelSkin);
            var tab2 = tabs.AddTab("Paranormal", panelSkin);
            tab1.panel.PanelOverflowBehavior = PanelOverflowBehavior.VerticalScroll;
            tab2.panel.PanelOverflowBehavior = PanelOverflowBehavior.VerticalScroll;

            foreach (Button button in levelsN)
            {
                button.Anchor = Anchor.Auto;
                tab1.panel.AddChild(button);
            }
            foreach (Button button in levelsP)
            {
                button.Anchor = Anchor.Auto;
                tab2.panel.AddChild(button);
            }
            panel.AddChild(tabs);

            UserInterface.Active.AddEntity(panel);
        }
    }
}
