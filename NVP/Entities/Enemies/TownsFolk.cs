using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NVP.Entities.Enemies
{
    public class TownsFolk : Enemy
    {
        public TownsFolk(Game game, Vector2 position, Texture2D texture, SpriteBatch sprite) : base(game, position, texture, sprite)
        {
            Velocity = 12;
            TotalLife = Life = 20;
            Dano = Life * (2.3 / 100);
            AttackRadius = new MonoGame.Extended.CircleF(position, 4);
        }
    }
}