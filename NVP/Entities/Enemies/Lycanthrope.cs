using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NVP.Entities.Enemies
{
    public class Lycanthrope : Enemy
    {
        public Lycanthrope(Game game, Vector2 position, Texture2D texture, SpriteBatch sprite) : base(game, position, texture, sprite)
        {
            Velocity = 20;
            TotalLife = Life = 45;
            Dano = Life * (3.3 / 100);
            AttackRadius = new MonoGame.Extended.CircleF(position, 5);
        }
    }
}