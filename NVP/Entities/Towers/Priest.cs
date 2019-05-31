using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NVP.Entities.Towers
{
    internal class Priest : Tower
    {
        public Priest(Game game, Vector2 position, Texture2D texture, SpriteBatch sprite) : base(game, position, texture, sprite)
        {
            Velocity = 11;
            TotalLife = Life = 20;
            Dano = Life * (3 / 100);
            AttackRadius = new MonoGame.Extended.CircleF(position, 10);
            Cost = 500;
        }
    }
}