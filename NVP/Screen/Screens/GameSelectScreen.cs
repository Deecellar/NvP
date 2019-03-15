﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using NVP.Screen.Transitions;

namespace NVP.Screen.Screens
{
    class GameSelectScreen : GameScreen
    {
        List<Button> LevelsN;
        List<Button> LevelsP;
        public GameSelectScreen(Game game) : base(game)
        {
            LevelsN = new List<Button>();
            LevelsP = new List<Button>();
        }

        public override void Initialize()
        {
            base.Initialize();
            ExpandTransition transition = new ExpandTransition(GraphicsDevice, Color.White);
            LevelsN.AddRange(Enumerable.Range(1, 4).Select((int x) =>
            {
                return new Button(string.Format("Nivel {0}", x)) { OnClick = (Entity e) => { UserInterface.Active.Clear(); ScreenManager.LoadScreen(new GameSelectedLevel(Game, x), transition); } };
            }));
            LevelsP.AddRange(Enumerable.Range(1, 4).Select((int x) =>
            {
                return new Button(string.Format("Nivel {0}", x)) { OnClick = (Entity e) => { UserInterface.Active.Clear(); ScreenManager.LoadScreen(new GameSelectedLevel(Game, x + 4), transition); } };
            }));

            HUD.Menu.CreateSelectionMenu("Niveles", 400, 400, LevelsN, LevelsP, offsetY: -30);
        }
        public override void Draw(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
