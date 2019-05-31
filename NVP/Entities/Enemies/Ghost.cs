using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NVP.Entities.Enemies
{
    public class Ghost : Enemy
    {
        public Ghost(Game game, Vector2 position, Texture2D texture, SpriteBatch sprite) : base(game, position, texture, sprite)
        {
            Velocity = 11;
            TotalLife = Life = 45;
            Dano = Life * (1.19 / 100);
            AttackRadius = new MonoGame.Extended.CircleF(position, 7);
        }
    }
}