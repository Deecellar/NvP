using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NVP.Entities.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace NVP.Entities.Towers
{
    public class Tower : Entity
    {
        public float Velocity = 20f;
        
        public Tower(Game game, Vector2 position, Texture2D texture, SpriteBatch sprite) : base(game, position, texture, sprite)
        {
            Enemigo = false;
            AttackRadius = new CircleF(new Point2(texture.Bounds.Width / 2, texture.Bounds.Height / 2), 32 * 10);
        }

        public virtual bool Fire(Entity entity)
        {
            if (entity.Enemigo)
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
            bool fired = false;
            foreach (Entity e in Entities)
            {
                if(AttackRadius.Contains(e.BoundingCircle.Center))
                fired = Fire(e);
                if (fired)
                {
                    break;
                }
            }
            AttackRadius = new CircleF(new Point2(Image.Bounds.Width / 2, Image.Bounds.Height / 2) + Position.ToPoint() , 32 * 10);

        }
    }
}
