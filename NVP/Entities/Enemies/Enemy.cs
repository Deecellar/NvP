using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;

namespace NVP.Entities.Enemies
{
    public class Enemy : Entity
    {
        public float Velocity = 30f;

        public Enemy(Game game, Vector2 position, Texture2D texture, SpriteBatch sprite) : base(game, position, texture, sprite)
        {
            Enemigo = true;
            Life = 9;
        }
        protected string Direction = "";
        public void DirectionToGo(char dir)
        {
            Direction = Convert.ToString(dir);
        }
        public Vector2 GetDirection(GameTime gametime, float seconds)
        {
            Vector2 temp = Vector2.Zero;
            if (Direction == "u")
            {
                temp = Vector2.Add(Position, new Vector2(0, -Velocity * (float)gametime.ElapsedGameTime.TotalSeconds * seconds));

            }
            else if (Direction == "l")
            {
                temp = Vector2.Add(Position, new Vector2(-Velocity * (float)gametime.ElapsedGameTime.TotalSeconds * seconds, 0));
            }
            else if (Direction == "r")
            {
                temp = Vector2.Add(Position, new Vector2(Velocity * (float)gametime.ElapsedGameTime.TotalSeconds * seconds, 0));

            }
            else if (Direction == "d")
            {
                temp = Vector2.Add(Position, new Vector2(0, Velocity * (float)gametime.ElapsedGameTime.TotalSeconds) * seconds);

            }
            return temp;
        }
        public virtual bool Fire(Entity entity)
        {
            if (!entity.Enemigo)
            {
                var temp = Position - entity.Position;
                var angulo = (float)Math.Atan2(temp.X, temp.Y);
                rotationDegrees = (angulo);
                new Bullet(this, Game, Position, Image, Sprite, Velocity, temp.Length() / Velocity, angulo);
                return true;
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
                bool fired = false;
                foreach (Entity e in Entities)
                {
                    fired = Fire(e);
                    if (fired)
                    {
                        break;
                    }
                }
            }
            else
            {
                UnloadContent();
            }


        }

        public override void Draw(GameTime gameTime)
        {
            Sprite.DrawCircle(Collider, 40, Color.Blue, 5);
        }

        public void Move(string direction, GameTime gametime)
        {
            if (direction == "u")
            {
                Position += new Vector2(0, -Velocity * (float)gametime.ElapsedGameTime.TotalSeconds);
                DirectionToGo('u');
            }
            else if (direction == "l")
            {
                Position += new Vector2(-Velocity * (float)gametime.ElapsedGameTime.TotalSeconds, 0);
                DirectionToGo('l');
            }
            else if (direction == "r")
            {
                Position += new Vector2(Velocity * (float)gametime.ElapsedGameTime.TotalSeconds, 0);
                DirectionToGo('r');

            }
            else if (direction == "d")
            {
                Position += new Vector2(0, Velocity * (float)gametime.ElapsedGameTime.TotalSeconds);
                DirectionToGo('d');

            }
            Collider = new CircleF(new Point2(Image.Bounds.Width / 2, Image.Bounds.Height / 2) + Position.ToPoint(), Image.Bounds.Width / 2);
            AttackRadius = new CircleF(new Point2(Image.Bounds.Width / 2, Image.Bounds.Height / 2) + Position.ToPoint(), Image.Bounds.Width * 20);

        }
    }
}
