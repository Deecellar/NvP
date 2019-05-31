using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NVP.Screen.Screens
{
    internal class GameoverScreen : GameScreen
    {
        private SpriteBatch Sprite;

        public GameoverScreen(Game game) : base(game)
        {
            Sprite = new SpriteBatch(GraphicsDevice);
            UserInterface.Active.AddEntity(new Paragraph("GameOver", Anchor.Center));
            UserInterface.Active.AddEntity(new Button("Menu Principal", ButtonSkin.Fancy, Anchor.Center, offset: new Vector2(0, 40)) { OnClick = (e) => { UserInterface.Active.Clear(); ScreenManager.LoadScreen(new MainMenuScreen(game)); } });
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            UserInterface.Active.Draw(Sprite);
        }

        public override void Update(GameTime gameTime)
        {
            UserInterface.Active.Update(gameTime);
        }
    }
}