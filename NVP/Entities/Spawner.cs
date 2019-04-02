using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVP.Entities
{
    class Spawner
    {
        private Vector2 Position { get; set; }
        private char Direction { get; set; }
        private int[] Enemigos { get; set; }
        public int Rondas { get; set; }
        private string[] Normal = new string[]

        {
            "Archer", "Knight", "Priest", "TownsFolk"
        };
        private string[] Paranormal = new string[]
        {
            "Zombi", "Ghost", "Lycanthrope", "Cultist"
        };
        private Game Game;
        private SpriteBatch Sprite;
        private bool IsNormal = true;

        public Spawner(Game game, SpriteBatch sprite, Vector2 position, char direction, int[] enemigos,  int rondas, bool isNormal)
        {
            Position = position;
            Direction = direction;
            Enemigos = enemigos;
            Rondas = rondas;
            IsNormal = isNormal;
            Game = game;
            Sprite = sprite;

        }
        
        public bool SpawnEnemies(int round,ref List<Enemies.Enemy> entities)
        {
            Random r = new Random();
            if (IsNormal)
            {
                if(Enemigos[round - 1] < 0)
                {
                    Rondas--;
                    return false;
                }
                Enemigos[round - 1]--;
                
                entities.Add(GetEnemy(Normal[r.Next(0, Normal.Length - 1)]));
                return true;
            }
            else
            {
                if (Enemigos[round - 1] <  0)
                {
                    Rondas--;
                    return false;
                }
                Enemigos[round - 1]--;
                entities.Add(GetEnemy(Paranormal[r.Next(0, Normal.Length - 1)]));
                return true;
            }
        }
        private Enemies.Enemy GetEnemy(string enemy)
        {
            Enemies.Enemy Enemy = new Enemies.Enemy(Game, Position, Game.Content.Load<Texture2D>("Sprites/Towers/32"), Sprite); ;
            switch (enemy)
            {
                case "Archer":
                    Enemy = new Enemies.Archer(Game, Position, Game.Content.Load<Texture2D>("Sprites/Towers/32"), Sprite);
                    break;
                case "Knight":
                    Enemy = new Enemies.Knight(Game, Position, Game.Content.Load<Texture2D>("Sprites/Towers/32"), Sprite);
                    break;
                case "Priest":
                    Enemy = new Enemies.Priest(Game, Position, Game.Content.Load<Texture2D>("Sprites/Towers/32"), Sprite);
                    break;
                case "TownsFolk":
                    Enemy = new Enemies.TownsFolk(Game, Position, Game.Content.Load<Texture2D>("Sprites/Towers/32"), Sprite);
                    break;
                case "Zombi":
                    Enemy = new Enemies.Zombi(Game, Position, Game.Content.Load<Texture2D>("Sprites/Towers/32"), Sprite);
                    break;
                case "Ghost":
                    Enemy = new Enemies.Ghost(Game, Position, Game.Content.Load<Texture2D>("Sprites/Towers/32"), Sprite);
                    break;
                case "Lycanthrope":
                    Enemy = new Enemies.Lycanthrope(Game, Position, Game.Content.Load<Texture2D>("Sprites/Towers/32"), Sprite);
                    break;
                case "Cultist":
                    Enemy = new Enemies.Cultist(Game, Position, Game.Content.Load<Texture2D>("Sprites/Towers/32"), Sprite);
                    break;
            }
            Enemy.DirectionToGo(Direction);
            return Enemy;
        }
    }
}
