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
    public class Entity : SimpleDrawableGameComponent, IDisposable
    {
        protected bool disposed = false;
        public Entity[] Entities { get; set; }
        public int Life { get; set; }
        public int Daño { get; set; }
        public CircleF BoundingCircle { get; set; }
        public CircleF AttackRadius { get; set; }

        public bool Enemigo { get; set; }
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

        public Entity(Game game, Vector2 position, Texture2D texture, SpriteBatch sprite)
        {
            Game = game;

            Sprite = sprite;
            Position = position;
            Image = texture;
            BoundingCircle = new CircleF(new Point2(texture.Bounds.Width/2,texture.Bounds.Height/2) + position.ToPoint() , texture.Bounds.Width / 2);
            AttackRadius = new CircleF(new Point2(texture.Bounds.Width / 2, texture.Bounds.Height / 2) + position.ToPoint() ,2 * texture.Bounds.Width / 2);
        }
        public override void Draw(GameTime gameTime)
        {
            Sprite.Draw(Image, Position, Color.White);

        }

        public override void Update(GameTime gameTime)
        {
            
        }


        protected override void UnloadContent()
        {
            Image = null;

        }




        public virtual Entity DetectarEnemigos(Entity other)
        {
            return other.BoundingCircle.Intersects(AttackRadius) ? other: this ;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
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
    }
}
