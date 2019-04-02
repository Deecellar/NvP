using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NVP.Entities.Towers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace NVP.Entities.Enemies
{
    public class Enemy : Entity
    {
        public float Velocity = 30f;
        public Enemy(Game game, Vector2 position, Texture2D texture, SpriteBatch sprite) : base(game, position, texture, sprite)
        {
            Enemigo = true;

        }
        protected string Direction = "";
        public void DirectionToGo(char dir)
        {
            Direction = Convert.ToString(dir);
        }
        public virtual bool Fire(Entity entity)
        {
            if (!entity.Enemigo)
            {
                var temp = Position - entity.Position;
                var angulo = (float)Math.Atan2(temp.X, temp.Y);
                rotationDegrees = (angulo);
                new Bullet(this, Game, Position, Image, Sprite, Velocity, temp.Length() / Velocity, angulo, entity);
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
        
        public void Move(string direction, GameTime gametime)
        {
            if(direction == "u")
            {
                Position += new Vector2(0, -Velocity * (float)gametime.ElapsedGameTime.TotalSeconds);

            }
            else if (direction == "l")
            {
                Position += new Vector2(-Velocity * (float)gametime.ElapsedGameTime.TotalSeconds, 0);
            }
            else if (direction == "r")
            {
                Position += new Vector2(Velocity * (float)gametime.ElapsedGameTime.TotalSeconds,0);

            }
            else if (direction == "d")
            {
                Position += new Vector2(0, Velocity * (float)gametime.ElapsedGameTime.TotalSeconds);

            }
            BoundingCircle =  new CircleF(new Point2(Image.Bounds.Width / 2, Image.Bounds.Height / 2) + Position.ToPoint(), Image.Bounds.Width / 2);
            AttackRadius = new CircleF(new Point2(Image.Bounds.Width / 2, Image.Bounds.Height / 2) + Position.ToPoint(), Image.Bounds.Width * 20);

        }
    }
}
