using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NVP.Entities.Towers
{
    public class Ghost : Tower
    {
        public Ghost(Game game, Vector2 position, Texture2D texture, SpriteBatch sprite) : base(game, position, texture, sprite)
        {
            Velocity = 10;
            TotalLife = Life = 45;
            Dano = Life * (1 / 100);
            AttackRadius = new MonoGame.Extended.CircleF(position, 10);
            Cost = 200;
        }
    }
}