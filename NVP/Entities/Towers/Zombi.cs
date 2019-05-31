using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NVP.Entities.Towers
{
    public class Zombi : Tower
    {
        public Zombi(Game game, Vector2 position, Texture2D texture, SpriteBatch sprite) : base(game, position, texture, sprite)
        {
            Velocity = 7.5f;
            TotalLife = Life = 20;
            Dano = Life * (1.5 / 100);
            AttackRadius = new MonoGame.Extended.CircleF(position, 5);
            Cost = 100;
        }
    }
}