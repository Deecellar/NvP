using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NVP.Entities.Enemies
{
    public class Knight : Enemy
    {
        public Knight(Game game, Vector2 position, Texture2D texture, SpriteBatch sprite) : base(game, position, texture, sprite)
        {
            Velocity = 11;
            TotalLife = Life = 50;
            Dano = Life * (4.5 / 100);
            AttackRadius = new MonoGame.Extended.CircleF(position, 3);
        }
    }
}