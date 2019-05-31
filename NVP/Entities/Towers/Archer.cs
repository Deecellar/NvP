using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NVP.Entities.Towers
{
    public class Archer : Tower
    {
        public Archer(Game game, Vector2 position, Texture2D texture, SpriteBatch sprite) : base(game, position, texture, sprite)
        {
            Velocity = 12;
            Dano = 20 * (2 / 100);
            AttackRadius = new MonoGame.Extended.CircleF(position, 20);
            TotalLife = Life = 20;
            Cost = 200;
        }

        public override void CreateAnimations()
        {
            base.CreateAnimations();
        }
    }
}