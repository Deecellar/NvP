using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using NVP.Entities;
using NVP.Entities.Enemies;
using System;
using System.Collections.Generic;

public class Tower : Entity
{
    public float Velocity = 20f;
    protected float cooldown = 0.9f;
    float timeElapsed = 0;
    Entity ToFire;
    bool fire = true;
    public int Cost { get; internal set; } = 100;
    List<Bullet> bullets;
    public Tower(Game game, Vector2 position, Texture2D texture, SpriteBatch sprite) : base(game, position, texture, sprite)
    {
        Enemigo = false;
        AttackRadius = new CircleF(new Point2(texture.Bounds.Width / 2, texture.Bounds.Height / 2), 100);
        Dano = 5;
    }
    public void SetBullets(ref List<Bullet> Bullets)
    {
        bullets = Bullets;
    }
    public virtual bool Fire(Entity entity, GameTime game)
    {
        if (entity.Enemigo)
        {
            Enemy enemigo = entity as Enemy;

            bullets.Add(new Bullet(this, Game, Position, Image, Sprite, Velocity, AttackRadius.Radius, rotationDegrees));

            return true;
        }
        return false;
    }

    public void Cooldown(GameTime gameTime, Entity e)
    {
        if (fire)
        {
            Fire(e, gameTime);
            fire = false;
        }
        if (!fire)
        {



            timeElapsed += gameTime.GetElapsedSeconds();
            if (timeElapsed > cooldown)
            {
                fire = true;
                timeElapsed = 0;
            }

        }
    }

    public void GetEntities(Entity[] entities)
    {
        Entities = entities;
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        Sprite.DrawCircle(AttackRadius, 50, Color.AliceBlue);
        if (ToFire != null)
            Sprite.DrawLine(Position, ToFire.Position, Color.Black);
    }
    public override void Update(GameTime gameTime)
    {
        ToFire = null;
        float lowerdistance = float.MaxValue;
        foreach (Entity e in Entities)
        {
            float distance = (float)Math.Sqrt(Vector2.Subtract(e.Position, Position).Dot(Vector2.Subtract(e.Position, Position)));

            if (lowerdistance > (float)distance)
            {
                lowerdistance = (float)distance;
            }
            if (distance == lowerdistance && e.Enemigo)
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

        AttackRadius = new CircleF(new Point2(Image.Bounds.Width / 2, Image.Bounds.Height / 2) + Position.ToPoint(), 150);

    }

}