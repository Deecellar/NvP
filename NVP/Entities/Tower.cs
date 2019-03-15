using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVP.Entities
{
    class Tower : SimpleDrawableGameComponent
    {
        public int Life { get; set; }
        public int Daño { get; set; }
        private Quaternion quaternion;
        public Quaternion Rotation
        {
            get { return quaternion; }
            set
            {
                quaternion = value;
                SetDegrees(value);
            }
        }
        private float rotationDegrees;
        public float RotationDegrees
        {
            get { return rotationDegrees; }
            set
            {
                rotationDegrees = value;
                SetDegrees(value);
            }
        }
        public Texture2D Image;

        public Vector2 Position { get; set; }

        public Game Game { get; internal set; }
        SpriteBatch Sprite;

        public Tower(Game game, Vector2 position, Texture2D texture)
        {
            Game = game;

            Sprite = new SpriteBatch(Game.GraphicsDevice);
            Position = position;
            Image = texture;
        }
        public override void Draw(GameTime gameTime)
        {
            Sprite.Begin();
            Sprite.Draw(Image, Position, Color.White);
            Sprite.End();
        }

        public override void Update(GameTime gameTime)
        {
            
        }


        protected override void UnloadContent()
        {
            Image = null;

        }

        private void SetDegrees(float value)
        {
            Rotation = new Quaternion(value, Vector3.Forward.X, Vector3.Forward.Y, Vector3.Forward.Z);
        }

        private void SetDegrees(Quaternion value)
        {
            RotationDegrees = value.X;
        }
    }
}
