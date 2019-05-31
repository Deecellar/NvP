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
    private float timeElapsed = 0;
    private Entity ToFire;
    private bool fire = true;
    public int Cost { get; internal set; } = 100;
    private List<Bullet> bullets;

    public Tower(Game game, Vector2 position, Texture2D texture, SpriteBatch sprite) : base(game, position, texture, sprite, (16, 18))
    {
        TotalLife = Life = 9;
        Enemigo = false;
        AttackRadius = new CircleF(Position, 100);
        Collider = new CircleF(Position, 100);
        Dano = 5;
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

    public void SetBullets(ref List<Bullet> Bullets)
    {
        bullets = Bullets;
    }

    public virtual bool Fire(Entity entity, GameTime game)
    {
        if (entity.Enemigo)
        {
            Enemy enemigo = entity as Enemy;
            if (bullets.Count <= Convert.ToInt32(NVP.Properties.Resources.BULLETS_LIMIT))
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
        AnimationHelper.Draw(Position, 0);
        Sprite.FillRectangle(Position - new Vector2(1, -21), new Size2(22, 6), Color.Black);
        Sprite.FillRectangle(Position - new Vector2(0, -22), new Size2(20 * ((float)Life / (float)TotalLife), 4), Color.Green);
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
            rotationDegrees = angulo < 0 ? MathHelper.TwoPi + angulo : angulo;
            Cooldown(gameTime, ToFire);
        }
        AttackRadius = new CircleF(Position, 150);
        Collider = new CircleF(Position, 16);
        if ((Math.Abs(RotationDegrees) >= MathHelper.ToRadians(315) && Math.Abs(RotationDegrees) < MathHelper.ToRadians(360)) || (Math.Abs(RotationDegrees) >= MathHelper.ToRadians(0) && Math.Abs(RotationDegrees) < MathHelper.ToRadians(45)))
        {
            CurrentAnimation = Animations.WalkRight;
        }
        else if (RotationDegrees >= MathHelper.ToRadians(45) && RotationDegrees < MathHelper.ToRadians(135))
        {
            CurrentAnimation = Animations.WalkDown;
        }
        else if (Math.Abs(RotationDegrees) >= MathHelper.ToRadians(135) && Math.Abs(RotationDegrees) < MathHelper.ToRadians(225))
        {
            CurrentAnimation = Animations.WalkLeft;
        }
        else
        {
            CurrentAnimation = Animations.WalkUp;
        }
        AnimationHelper.Play(CurrentAnimation, Spritesheet.Repeat.Mode.LoopWithReverse);

        AnimationHelper.Update(gameTime);
    }
}