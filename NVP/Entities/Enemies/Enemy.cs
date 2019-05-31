using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Timers;
using System;
using System.Collections.Generic;

namespace NVP.Entities.Enemies
{
    public class Enemy : Entity
    {
        public float Velocity = 30f;
        private Entity ToFire;

        public Enemy(Game game, Vector2 position, Texture2D texture, SpriteBatch sprite) : base(game, position, texture, sprite, (16, 18))
        {
            Enemigo = true;
            Life = 9;
            TotalLife = 9;
            CreateAnimations();
        }

        public override void CreateAnimations()
        {
            AnimationHelper.CreateAnimation(Animations.WalkUp, new[] { (0, 0), (0, 1), (0, 2) });
            AnimationHelper.CreateAnimation(Animations.WalkRight, new[] { (1, 0), (1, 1), (1, 2) });
            AnimationHelper.CreateAnimation(Animations.WalkDown, new[] { (2, 0), (2, 1), (2, 2) });
            AnimationHelper.CreateAnimation(Animations.WalkLeft, new[] { (1, 0), (1, 1), (1, 2) }, true);
            AnimationHelper.Play(Animations.WalkDown, Spritesheet.Repeat.Mode.LoopWithReverse);
        }

        protected string Direction = "";
        private bool fire;
        private CountdownTimer timer = new CountdownTimer(TimeSpan.FromSeconds(3));

        public List<Bullet> bullets { get; private set; }

        public void DirectionToGo(char dir)
        {
            Direction = Convert.ToString(dir);
        }

        public virtual bool Fire(Entity entity)
        {
            if (entity != null)
                if (!entity.Enemigo)
                {
                    var temp = Position - entity.Position;
                    if (bullets.Count <= Convert.ToInt32(NVP.Properties.Resources.BULLETS_LIMIT))

                        bullets.Add(new Bullet(this, Game, Position, Image, Sprite, Velocity, temp.Length() / Velocity, this.rotationDegrees)); return true;
                }
            return false;
        }

        public void GetEntities(Entity[] entities)
        {
            Entities = entities;
        }

        public override void Update(GameTime gameTime)
        {
            if (!disposed)
            {
                Move(Direction, gameTime);
                ToFire = null;
                float lowerdistance = float.MaxValue;
                foreach (Entity e in Entities)
                {
                    float distance = (float)Math.Sqrt(Vector2.Subtract(e.Position, Position).Dot(Vector2.Subtract(e.Position, Position)));

                    if (lowerdistance > (float)distance)
                    {
                        lowerdistance = (float)distance;
                    }
                    if (distance == lowerdistance && !e.Enemigo)
                    {
                        ToFire = e;
                    }
                }
                if (ToFire != null && ToFire.Collider.Intersects(AttackRadius))
                {
                    var temp = ToFire.Position - Position;
                    var angulo = (float)Math.Atan2(ToFire.Position.Y - Position.Y, ToFire.Position.X - Position.X);
                    rotationDegrees = angulo;
                    Cooldown(gameTime, ToFire);
                }
            }
            else
            {
                UnloadContent();
            }

            AnimationHelper.Play(CurrentAnimation, Spritesheet.Repeat.Mode.LoopWithReverse);

            AnimationHelper.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            AnimationHelper.Draw(Position, 0);
            Sprite.DrawCircle(Collider, 12, Color.Azure);
            Sprite.FillRectangle(Position - new Vector2(1, -21), new Size2(22, 6), Color.Black);
            Sprite.FillRectangle(Position - new Vector2(0, -22), new Size2(20 * ((float)Life / (float)TotalLife), 4), Color.Red);
        }

        public void Move(string direction, GameTime gametime)
        {
            if (direction == "u")
            {
                Position += new Vector2(0, -Velocity * (float)gametime.ElapsedGameTime.TotalSeconds);

                DirectionToGo('u');
                CurrentAnimation = Animations.WalkUp;
            }
            else if (direction == "l")
            {
                Position += new Vector2(-Velocity * (float)gametime.ElapsedGameTime.TotalSeconds, 0);
                DirectionToGo('l');
                CurrentAnimation = Animations.WalkLeft;
            }
            else if (direction == "r")
            {
                Position += new Vector2(Velocity * (float)gametime.ElapsedGameTime.TotalSeconds, 0);
                DirectionToGo('r');
                CurrentAnimation = Animations.WalkRight;
            }
            else if (direction == "d")
            {
                Position += new Vector2(0, Velocity * (float)gametime.ElapsedGameTime.TotalSeconds);
                DirectionToGo('d');
                CurrentAnimation = Animations.WalkDown;
            }

            timer.Update(gametime);
            Collider = new CircleF(Position, 16);
            AttackRadius = new CircleF(Position, 150);
        }

        public void SetBullets(ref List<Bullet> Bullets)
        {
            bullets = Bullets;
        }

        public void Cooldown(GameTime gameTime, Entity e)
        {
            if (fire)
            {
                Fire(e);
                fire = false;
            }
            if (!fire)
            {
                if (timer.State == TimerState.Completed)
                {
                    fire = true;
                    timer.Restart();
                }
            }
        }
    }
}