using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NVP.Entities
{
    public class Bullet : Entity, IDisposable
    {

        private float Lifespan = 0;
        private float Velocity = 0;

        public Bullet(Entity sent, Game game, Vector2 position, Texture2D texture, SpriteBatch sprite, float lifespan, float velocity, float angle, Entity entity) : base(game, position, texture, sprite)
        {
            Lifespan = lifespan;
            Velocity = velocity;
            RotationDegrees = angle;
            Entities = new Entity[] { sent, entity };
        }



        public override void Update(GameTime gameTime)
        {
            Lifespan -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.Position += new Vector2((float)Math.Cos((double)RotationDegrees) * Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds, (float)Math.Sin((double)RotationDegrees) * Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
            if (BoundingCircle.Intersects(Entities[1].BoundingCircle))
            {
                Entities[1].Life -= Entities[0].Daño;
                this.Dispose();
            }
            if (Lifespan < 0)
            {
                this.Dispose();
            }
        }

    }
}
