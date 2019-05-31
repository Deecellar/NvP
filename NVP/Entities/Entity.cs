using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using NVP.Helpers;
using System;

namespace NVP.Entities
{
    public enum Animations
    {
        Walk,
        WalkUp,
        WalkRight,
        WalkDown,
        WalkLeft,
        Fire
    }

    public abstract class Entity : SimpleDrawableGameComponent, IDisposable, ICollisionableObject
    {
        protected bool disposed = false;
        public double TotalLife { get; set; }
        public double Life { get; set; }
        public double Dano { get; set; }
        public Entity[] Entities { get; set; }
        public CircleF Collider { get; set; }
        public CircleF AttackRadius { get; set; }
        public bool IsActive { get; set; } = true;
        public bool Enemigo { get; set; }
        protected float rotationDegrees;
        protected Animations CurrentAnimation = Animations.WalkDown;

        protected AnimationHelper<Animations> AnimationHelper;

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
            Collider = new CircleF(new Point2(texture.Bounds.Width / 2, texture.Bounds.Height / 2) + position.ToPoint(), texture.Bounds.Width / 2);
            AttackRadius = new CircleF(new Point2(texture.Bounds.Width / 2, texture.Bounds.Height / 2) + position.ToPoint(), 2 * texture.Bounds.Width / 2);
        }

        public Entity(Game game, Vector2 position, Texture2D texture, SpriteBatch sprite, (int x, int y) size)
        {
            Game = game;

            Sprite = sprite;
            Position = position;
            Image = texture;
            AnimationHelper = new AnimationHelper<Animations>(sprite, texture, size.x, size.y);

            AttackRadius = new CircleF(new Point2(texture.Bounds.Width / 2, texture.Bounds.Height / 2) + position.ToPoint(), 2 * texture.Bounds.Width / 2);
            Collider = new CircleF(new Point2(texture.Bounds.Width / 2, texture.Bounds.Height / 2) + position.ToPoint(), texture.Bounds.Width / 2);
        }

        public virtual void CreateAnimations()
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            CreateAnimations();
        }

        public override void Draw(GameTime gameTime)
        {
            AnimationHelper.Draw(Position, 0);
        }

        public override void Update(GameTime gameTime)
        {
            Collider = new CircleF(new Point2(Image.Bounds.Width / 4, Image.Bounds.Height / 4) + Position.ToPoint(), Image.Bounds.Width / 4);

            AnimationHelper.Update(gameTime);
        }

        protected override void UnloadContent()
        {
            Image = null;
        }

        public virtual Entity DetectarEnemigos(Entity other)
        {
            return other.Collider.Intersects(AttackRadius) ? other : this;
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

        public virtual void OnCollision(ICollisionableObject collisionableObject)
        {
        }
    }
}