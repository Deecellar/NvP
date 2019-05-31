using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using NVP.Entities;
using NVP.Helpers;
using System;

public class Bullet : Entity, IDisposable
{
    public bool IsOutsideHisSpawner = false;
    private float Velocity = 0;
    public bool Impacted { get; internal set; }
    private Entity Sender;
    private int SenderType = 0;
    private Spritesheet.Repeat.Mode Mode = Spritesheet.Repeat.Mode.LoopWithReverse;

    public Bullet(Entity sent, Game game, Vector2 position, Texture2D texture, SpriteBatch sprite, float radius, float velocity, float angle) : base(game, position, texture, sprite)
    {
        Velocity = velocity;
        RotationDegrees = angle;
        Sender = sent;
        Collider = new CircleF(position, 16);
        var SenderTypeIf = Sender.GetType();
        if (SenderTypeIf == typeof(NVP.Entities.Towers.Archer))
        {
            SenderType = 0;
        }
        else if (SenderTypeIf == typeof(NVP.Entities.Towers.Cultist))
        {
            SenderType = 1;
        }
        else if (SenderTypeIf == typeof(NVP.Entities.Towers.Ghost))
        {
            SenderType = 2;
        }
        else if (SenderTypeIf == typeof(NVP.Entities.Towers.Knight))
        {
            SenderType = 3;
        }
        else if (SenderTypeIf == typeof(NVP.Entities.Towers.Lycanthrope))
        {
            SenderType = 4;
        }
        else if (SenderTypeIf == typeof(NVP.Entities.Towers.Priest))
        {
            SenderType = 5;
        }
        else if (SenderTypeIf == typeof(NVP.Entities.Towers.TownsFolk))
        {
            SenderType = 6;
        }
        else if (SenderTypeIf == typeof(NVP.Entities.Towers.Zombi))
        {
            SenderType = 7;
        }
        if (SenderTypeIf == typeof(NVP.Entities.Enemies.Archer))
        {
            SenderType = 0;
        }
        else if (SenderTypeIf == typeof(NVP.Entities.Enemies.Cultist))
        {
            SenderType = 1;
        }
        else if (SenderTypeIf == typeof(NVP.Entities.Enemies.Ghost))
        {
            SenderType = 2;
        }
        else if (SenderTypeIf == typeof(NVP.Entities.Enemies.Knight))
        {
            SenderType = 3;
        }
        else if (SenderTypeIf == typeof(NVP.Entities.Enemies.Lycanthrope))
        {
            SenderType = 4;
        }
        else if (SenderTypeIf == typeof(NVP.Entities.Enemies.Priest))
        {
            SenderType = 5;
        }
        else if (SenderTypeIf == typeof(NVP.Entities.Enemies.TownsFolk))
        {
            SenderType = 6;
        }
        else if (SenderTypeIf == typeof(NVP.Entities.Enemies.Zombi))
        {
            SenderType = 7;
        }
        CreateAnimations();
        AnimationHelper.Play(Animations.Fire, Mode);
    }

    public override void CreateAnimations()
    {
        switch (SenderType)
        {
            case 0:
                Image = Game.Content.Load<Texture2D>("Sprites/Balas/Arrow");
                AnimationHelper = new AnimationHelper<Animations>(Sprite, Image, 10, 15);
                AnimationHelper.CreateAnimation(Animations.Fire, new[] { (0, 0), (1, 0) });
                break;

            case 1:
                Image = Game.Content.Load<Texture2D>("Sprites/Balas/CultistActivate");
                AnimationHelper = new AnimationHelper<Animations>(Sprite, Image, 10, 15);
                AnimationHelper.CreateAnimation(Animations.Fire, new[] { (0, 0), (1, 0), (2, 0), (3, 0) });
                break;

            case 2:
                Image = Game.Content.Load<Texture2D>("Sprites/Balas/GhostBullet");
                AnimationHelper = new AnimationHelper<Animations>(Sprite, Image, 10, 15);
                AnimationHelper.CreateAnimation(Animations.Fire, new[] { (0, 0), (1, 0) });
                break;

            case 3:
                Image = Game.Content.Load<Texture2D>("Sprites/Balas/Magic");
                AnimationHelper = new AnimationHelper<Animations>(Sprite, Image, 10, 15);
                AnimationHelper.CreateAnimation(Animations.Fire, new[] { (0, 0), (1, 0) });
                break;

            case 4:
                Image = Game.Content.Load<Texture2D>("Sprites/Balas/Fang");
                AnimationHelper = new AnimationHelper<Animations>(Sprite, Image, 10, 15);
                AnimationHelper.CreateAnimation(Animations.Fire, new[] { (0, 0) });
                break;

            case 5:
                Image = Game.Content.Load<Texture2D>("Sprites/Balas/PriestActivate");
                AnimationHelper = new AnimationHelper<Animations>(Sprite, Image, 10, 15);
                AnimationHelper.CreateAnimation(Animations.Fire, new[] { (0, 0), (1, 0), (2, 0), (3, 0), (4, 0), (5, 0), (6, 0), (7, 0), (8, 0) });
                break;

            case 6:
                Image = Game.Content.Load<Texture2D>("Sprites/Balas/Jabelin");
                AnimationHelper = new AnimationHelper<Animations>(Sprite, Image, 10, 15);
                AnimationHelper.CreateAnimation(Animations.Fire, new[] { (0, 0), (1, 0), (2, 0) });
                break;

            case 7:
                Image = Game.Content.Load<Texture2D>("Sprites/Balas/ZombeBullet");
                AnimationHelper = new AnimationHelper<Animations>(Sprite, Image, 10, 15);
                AnimationHelper.CreateAnimation(Animations.Fire, new[] { (0, 0) });
                break;

            default:
                Image = Game.Content.Load<Texture2D>("Sprites/Balas/Arrow");
                AnimationHelper = new AnimationHelper<Animations>(Sprite, Image, 10, 15);
                AnimationHelper.CreateAnimation(Animations.Fire, new[] { (0, 0), (1, 0) });
                break;
        }
    }

    public override void Draw(GameTime gameTime)
    {
        AnimationHelper.Draw(Position, 0);
    }

    public override void Update(GameTime gameTime)
    {
        if (!Sender.AttackRadius.Intersects(Collider))
        {
            IsOutsideHisSpawner = true;
        }
        this.Position += new Vector2((float)Math.Cos((double)RotationDegrees) * Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds, (float)Math.Sin((double)RotationDegrees) * Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
        Collider = new CircleF(Position, 16);

        AnimationHelper.Play(Animations.Fire);
        AnimationHelper.Update(gameTime);
    }

    public override void OnCollision(ICollisionableObject collisionableObject)
    {
        var col = collisionableObject as Entity;
        if (col.GetType() != typeof(Bullet))
        {
            if (col.Enemigo && !Sender.Enemigo)
            {
                col.Life -= Sender.Dano;
                Impacted = true;
            }
            else if (!col.Enemigo && Sender.Enemigo)
            {
                col.Life -= Sender.Dano;
                Impacted = true;
            }
        }
    }
}