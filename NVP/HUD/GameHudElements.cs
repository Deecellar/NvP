﻿using System;
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
     public static void TowerSelectionHUD(Texture2D[] textures, Action[] action)
        {
            Panel panel = new Panel(new Vector2(640, 100), PanelSkin.Fancy, Anchor.TopCenter);
            panel.AddChild(new Paragraph("",Anchor.AutoInlineNoBreak, new Vector2(20,100)));
            foreach(Texture2D T2D in textures)
            {
                Button button = new Button
                {
                    OnClick = btn => action.ElementAt(textures.ToList().IndexOf(T2D)),
                    Size = new Vector2(60,60),
                    Anchor = Anchor.AutoInlineNoBreak
                };
                button.AddChild(new Image(T2D,new Vector2(T2D.Bounds.Width,T2D.Bounds.Height), drawMode: ImageDrawMode.Panel, anchor: Anchor.Center));
                panel.AddChild(button);
                }

            
            RichParagraphStyleInstruction.AddInstruction("BOLD_GOLD", new RichParagraphStyleInstruction(fillColor: Color.Gold, fontStyle: FontStyle.Bold));
            panel.AddChild(new Image(textures[1], new Vector2(textures[1].Bounds.Width, textures[1].Bounds.Height), anchor: Anchor.AutoInlineNoBreak, offset: new Vector2(10,10)));
            panel.AddChild(new RichParagraph(@"{{BOLD_GOLD}}   6000", Anchor.AutoInlineNoBreak));

            UserInterface.Active.AddEntity(panel);


            }
        }
    }

