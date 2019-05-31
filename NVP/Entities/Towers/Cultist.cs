using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NVP.Entities.Towers
{
    public class Cultist : Tower
    {
        public Cultist(Game game, Vector2 position, Texture2D texture, SpriteBatch sprite) : base(game, position, texture, sprite)
        {
            Velocity = 12;
            TotalLife = Life = 18;
            Dano = Life * (4.5 / 100);
            AttackRadius = new MonoGame.Extended.CircleF(position, 15);
            Cost = 500;
        }
    }
}