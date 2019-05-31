using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NVP.Entities.Enemies
{
    public class Priest : Enemy
    {
        public Priest(Game game, Vector2 position, Texture2D texture, SpriteBatch sprite) : base(game, position, texture, sprite)
        {
            Velocity = 13;
            TotalLife = Life = 30;
            Dano = Life * (4 / 100);
            AttackRadius = new MonoGame.Extended.CircleF(position, 6);
        }
    }
}