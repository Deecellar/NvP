using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NVP.Entities.Enemies
{
    public class Archer : Enemy
    {
        public Archer(Game game, Vector2 position, Texture2D texture, SpriteBatch sprite) : base(game, position, texture, sprite)
        {
            Velocity = 13;
            Dano = 30 * (2 / 100);
            AttackRadius = new MonoGame.Extended.CircleF(position, 10);
            TotalLife = Life = 30;
        }
    }
}