using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NVP.Entities.Enemies
{
    internal class Zombi : Enemy
    {
        public Zombi(Game game, Vector2 position, Texture2D texture, SpriteBatch sprite) : base(game, position, texture, sprite)
        {
            Velocity = 8f;
            TotalLife = Life = 20;
            Dano = Life * (1.15 / 100);
            AttackRadius = new MonoGame.Extended.CircleF(position, 2);
        }

        public override void CreateAnimations()
        {
            AnimationHelper.CreateAnimation(Animations.WalkUp, new[] { (0, 0), (0, 0), (0, 1), (0, 1), (0, 2), (0, 2) });
            AnimationHelper.CreateAnimation(Animations.WalkRight, new[] { (1, 0), (1, 0), (1, 1), (1, 1), (1, 2), (1, 2) });
            AnimationHelper.CreateAnimation(Animations.WalkDown, new[] { (2, 0), (2, 0), (2, 1), (2, 1), (2, 2), (2, 2) });
            AnimationHelper.CreateAnimation(Animations.WalkLeft, new[] { (1, 0), (1, 0), (1, 1), (1, 1), (1, 2), (1, 2) }, true);
            AnimationHelper.Play(Animations.WalkDown, Spritesheet.Repeat.Mode.LoopWithReverse);
        }
    }
}