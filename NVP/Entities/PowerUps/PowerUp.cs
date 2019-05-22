using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;

namespace NVP.Entities.PowerUps
{
    public class PowerUp : SimpleDrawableGameComponent, IDisposable
    {
        protected bool disposed = false;
        protected float rotationDegrees;
        public float RotationDegrees
        {
            get { return rotationDegrees; }
            set
            {
                rotationDegrees = value;
            }
        }
        public Texture2D Image;

        public Vector2 Position { get; set; }

        public Game Game { get; internal set; }
        protected SpriteBatch Sprite;

        public PowerUp(Game game, Vector2 position, Texture2D texture, SpriteBatch sprite)
        {
            Game = game;

            Sprite = sprite;
            Position = position;
            Image = texture;
        }
        public override void Draw(GameTime gameTime)
        {
            Sprite.Draw(Image, Position, Color.White);

        }


        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                Image = null;
                Sprite = null;
            }

            disposed = true;
        }
        public override void Update(GameTime gameTime)
        {

        }
        protected override void UnloadContent()
        {
            Image = null;

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }
    }
}
