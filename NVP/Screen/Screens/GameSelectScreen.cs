using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NVP.Screen.Transitions;
using System.Collections.Generic;
using System.Linq;

namespace NVP.Screen.Screens
{
    internal class GameSelectScreen : GameScreen
    {
        private List<Button> LevelsN;
        private List<Button> LevelsP;

        public GameSelectScreen(Game game) : base(game)
        {
            LevelsN = new List<Button>();
            LevelsP = new List<Button>();
        }

        public override void Initialize()
        {
            base.Initialize();
            UserInterface.Active.AddEntity(new Image(Content.Load<Texture2D>("Sprites/unknown")));
            ExpandTransition transition = new ExpandTransition(GraphicsDevice, Color.White, 4);
            LevelsN.AddRange(Enumerable.Range(1, 4).Select((int x) =>
            {
                return new Button(string.Format("Nivel {0}", x)) { OnClick = (Entity e) => { UserInterface.Active.Clear(); ScreenManager.LoadScreen(new GameSelectedLevel(Game, x), transition); } };
            }));
            LevelsP.AddRange(Enumerable.Range(1, 4).Select((int x) =>
            {
                return new Button(string.Format("Nivel {0}", x)) { OnClick = (Entity e) => { UserInterface.Active.Clear(); ScreenManager.LoadScreen(new GameSelectedLevel(Game, x + 4), transition); } };
            }));
            var viewport = new MonoGame.Extended.ViewportAdapters.DefaultViewportAdapter(Game.GraphicsDevice);
            HUD.Menu.CreateSelectionMenu("Niveles", 400, 300, LevelsN, LevelsP, offsetY: -30);
        }

        public override void Draw(GameTime gameTime)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}