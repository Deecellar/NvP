using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NVP.Entities.Enemies
{
    public class Cultist : Enemy
    {
        public Cultist(Game game, Vector2 position, Texture2D texture, SpriteBatch sprite) : base(game, position, texture, sprite)
        {
            Velocity = 10;
            TotalLife = Life = 20;
            Dano = Life * (4 / 100);
            AttackRadius = new MonoGame.Extended.CircleF(position, 5);
        }
    }
}