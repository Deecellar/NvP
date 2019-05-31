using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NVP.Entities.Towers
{
    public class TownsFolk : Tower
    {
        public TownsFolk(Game game, Vector2 position, Texture2D texture, SpriteBatch sprite) : base(game, position, texture, sprite)
        {
            Velocity = 10;
            TotalLife = Life = 15;
            Dano = Life * (1.5 / 100);
            AttackRadius = new MonoGame.Extended.CircleF(position, 7);
            Cost = 100;
        }
    }
}