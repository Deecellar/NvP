using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NVP.Screen.Screens.OptionsScreens
{
    class VideoScreen : GameScreen
    {
        public VideoScreen(Game game) : base(game)
        {
            List<DisplayMode> displayModes = GraphicsDevice.Adapter.SupportedDisplayModes.ToList();
            List<string> display = new List<string>();
            foreach(var disp in displayModes)
            {
                display.Add(string.Format("{0}x{1}", disp.Width, disp.Height));
            }
            Game.IsFixedTimeStep = false;
            Game1 tb = Game as Game1;
            tb.Graphics.SynchronizeWithVerticalRetrace = false;
        }
        public override void Draw(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
