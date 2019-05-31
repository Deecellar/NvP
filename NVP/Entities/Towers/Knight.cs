using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NVP.Entities.Towers
{
    internal class Knight : Tower
    {
        public Knight(Game game, Vector2 position, Texture2D texture, SpriteBatch sprite) : base(game, position, texture, sprite)
        {
            Velocity = 9;
            TotalLife = Life = 40;
            Dano = Life * (3.5 / 100);
            AttackRadius = new MonoGame.Extended.CircleF(position, 8);
            Cost = 400;
        }
    }
}