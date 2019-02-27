using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NVP.Screen.Transitions;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace NVP.Screen
{
    public class ScreenManager : MonoGame.Extended.SimpleDrawableGameComponent
    {
        private Screen _activeScreen;
        private bool _isInitializated;
        private bool _isLoaded;
        private Transition _activeTransition;

        public void LoadScreen(Screen screen, Transition transition = null)
        {
            if (_activeTransition != null)
                return;

            _activeTransition = transition;
            _activeTransition.StateChanged += (sender, args) => LoadScreen(screen);
            _activeTransition.Completed += (sender, args) =>
            {
                _activeTransition.Dispose();
                _activeTransition = null;
            };
        }

        public void LoadScreen(Screen screen)
        {
            _activeScreen?.UnloadContent();
            _activeScreen?.Dispose();

            screen.ScreenManager = this;

            screen.Initialize();

            screen.LoadContent();

            _activeScreen = screen;
        }

        public void Initialize()
        {
            _activeScreen?.Initialize();
            _isInitializated = true;
        } 
        public void LoadContent()
        {
            _activeScreen?.LoadContent();
            _isLoaded = true;
        }
        public void UnloadContent()
        {
            _activeScreen?.UnloadContent();
            _isLoaded = false;
        }
        public override void Update(GameTime gameTime)
        {
            _activeScreen?.Update(gameTime);
            _activeTransition?.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            _activeScreen?.Draw(gameTime);
            _activeTransition?.Draw(gameTime);
        }
    }
}
