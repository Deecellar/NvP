using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using NVP.Entities;
using NVP.Entities.Enemies;
using NVP.Entities.PowerUps;
using NVP.Helpers;
using System;

public class Bullet : Entity, IDisposable
{

    public bool IsOutsideHisSpawner = false;
    private float Velocity = 0;
    public bool Impacted { get; internal set; }
    private Entity Sender;

    public Bullet(Entity sent, Game game, Vector2 position, Texture2D texture, SpriteBatch sprite, float radius, float velocity, float angle) : base(game, position, texture, sprite)
    {

        Velocity = velocity;
        RotationDegrees = angle;
        Sender = sent;
        Collider = new CircleF(new Point2(Image.Bounds.Width / 2, Image.Bounds.Height / 2) + Position.ToPoint(), Image.Bounds.Width / 2);
    }



    public override void Update(GameTime gameTime)
    {
        if (!Sender.AttackRadius.Intersects(Collider))
        {
            IsOutsideHisSpawner = true;
        }
        this.Position += new Vector2((float)Math.Cos((double)RotationDegrees) * Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds, (float)Math.Sin((double)RotationDegrees) * Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
        base.Update(gameTime);
    }
    public override void OnCollision(ICollisionableObject collisionableObject)
    {
        var col = collisionableObject as Entity;
        if ((col.Enemigo && (Sender.GetType() == typeof(Tower))) || (!col.Enemigo && (Sender.GetType() == typeof(Enemy))) && !(Sender.GetType() == typeof(PowerUp)))
        {
            col.Life -= Sender.Dano;
            Impacted = true;
        }
    }
}
