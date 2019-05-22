using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NVP.Entities.Towers
{

    public class Archer : Tower
    {

        public Archer(Game game, Vector2 position, Texture2D texture, SpriteBatch sprite) : base(game, position, texture, sprite)
        {

        }
        public override void CreateAnimations()
        {
            base.CreateAnimations();
        }

    }
}
