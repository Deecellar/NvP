using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NVP.Helpers
{
    public static class MoneyManager
    {
        public static int Money { get; internal set; }
        private static bool IsParanormal;
        private static Game game;
        private static SpriteBatch sprite;

        public static void Initialize(int money, bool isParanormal, Game Game, SpriteBatch spriteBatch)
        {
            Money = money;
            IsParanormal = isParanormal;
            game = Game;
            sprite = spriteBatch;
        }

        public static bool PayTower(int t, out Tower TowerToSend, Vector2 pos)
        {
            var tower = ReturnTower(t, pos);
            if (Money >= tower.Cost)
            {
                Money -= tower.Cost;
                TowerToSend = tower;
                return true;
            }
            TowerToSend = null;
            return false;
        }

        private static Tower ReturnTower(int tower, Vector2 Position)
        {
            if (IsParanormal)
            {
                tower += 4;
            }

            switch (tower)
            {
                case 0:
                    return new Entities.Towers.Knight(game, Position, RandomSpriteHelper.GenerateRandomSprite(game.Content, game.GraphicsDevice, "Knight"), sprite);

                case 1:
                    return new Entities.Towers.Archer(game, Position, RandomSpriteHelper.GenerateRandomSprite(game.Content, game.GraphicsDevice, "Archer"), sprite);

                case 2:
                    return new Entities.Towers.TownsFolk(game, Position, RandomSpriteHelper.GenerateRandomSprite(game.Content, game.GraphicsDevice, "TownsFolk"), sprite);

                case 3:
                    return new Entities.Towers.Priest(game, Position, RandomSpriteHelper.GenerateRandomSprite(game.Content, game.GraphicsDevice, "Priest"), sprite);

                case 4:
                    return new Entities.Towers.Cultist(game, Position, RandomSpriteHelper.GenerateRandomSprite(game.Content, game.GraphicsDevice, "Cultist"), sprite);

                case 5:
                    return new Entities.Towers.Ghost(game, Position, RandomSpriteHelper.GenerateRandomSprite(game.Content, game.GraphicsDevice, "Ghost"), sprite);

                case 6:
                    return new Entities.Towers.Lycanthrope(game, Position, RandomSpriteHelper.GenerateRandomSprite(game.Content, game.GraphicsDevice, "Lycanthrope"), sprite);

                case 7:
                    return new Entities.Towers.Zombi(game, Position, RandomSpriteHelper.GenerateRandomSprite(game.Content, game.GraphicsDevice, "Zombi"), sprite);
            }
            return null;
        }
    }
}