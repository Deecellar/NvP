
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Input.InputListeners;
using MonoGame.Extended.Input;
namespace NVP.Helpers
{
    public class InputManager : MonoGame.Extended.SimpleGameComponent
    {
        public Action<GamePadEventArgs> GamePadFunc { get; set; }
        public Action<KeyboardEventArgs> KeyboardFunc { get; set; }
        public Action<MouseEventArgs> MouseFunc { get; set; }
        public Action<MouseEventArgs> MouseDragFunc { get; set; }



        GamePadListener GamePad;
        KeyboardListener KeyboardListener;
        MouseListener MouseListener;
        List<InputListener> InputsList;
        public InputManager(Game game) 
        {


            GamePadListenerSettings gamePad = new GamePadListenerSettings();
            KeyboardListenerSettings keyboard = new KeyboardListenerSettings();
            MouseListenerSettings mouseListener = new MouseListenerSettings();

            

            #region Configurando Controllers
            keyboard.RepeatPress = true;
            gamePad.PlayerIndex = PlayerIndex.One;
            gamePad.VibrationEnabled = false;
            mouseListener.ViewportAdapter = new MonoGame.Extended.ViewportAdapters.DefaultViewportAdapter(game.GraphicsDevice);
            mouseListener.DoubleClickMilliseconds = int.MaxValue;
            mouseListener.DragThreshold = 40;
            #endregion
            #region Poniendolos en lista
            InputsList = new List<InputListener>();
            InputsList.Add( GamePad = gamePad.CreateListener());
            InputsList.Add(KeyboardListener = keyboard.CreateListener());
            InputsList.Add(MouseListener = mouseListener.CreateListener());
            #endregion

            #region Configurando eventos

            GamePad.ButtonDown += GamePad_ButtonDown;
            KeyboardListener.KeyPressed += KeyboardListener_KeyPressed;
            MouseListener.MouseClicked += MouseListener_MouseClicked;
            MouseListener.MouseDrag += MouseListener_MouseDrag;
            
            #endregion

            game.Components.Add(new InputListenerComponent(game, InputsList.ToArray()));
        }

        private void MouseListener_MouseDrag(object sender, MouseEventArgs e)
        {
            MouseDragFunc.Invoke(e);
        }

        private void MouseListener_MouseClicked(object sender, MouseEventArgs e)
        {
            MouseFunc.Invoke(e);
        }

        private void KeyboardListener_KeyPressed(object sender, KeyboardEventArgs e)
        {
            KeyboardFunc.Invoke(e);
        }

        private void GamePad_ButtonDown(object sender, GamePadEventArgs e)
        {
            GamePadFunc.Invoke(e);
        }

       

        public override void Update(GameTime gameTime)
        {
            foreach(var input in InputsList)
            {
                input.Update(gameTime);
            }

        }
    }
}
