using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NVP.Entities
{
    class Spawner
    {
        private Vector2 Position { get; set; }
        private char Direction { get; set; }
        public int[] Enemigos { get; set; }
        public int Rondas { get; set; }
        public bool NextRound { get; private set; }

        private Dictionary<string, int> Normal = new Dictionary<string, int>();
        private Dictionary<string, int> Paranormal = new Dictionary<string, int>();
        private Game Game;
        private SpriteBatch Sprite;
        private bool IsNormal = true;

        const int cooldown = 10;
        bool cooldownBewteenEnemies = false;
        const float enemiesCooldown = 2.5f;
        float timeBetweenEnemies = 0;
        float timeElapsed = 0;

        public Spawner(Game game, SpriteBatch sprite, Vector2 position, char direction, int[] enemigos, int rondas, bool isNormal)
        {
            Position = position;
            Direction = direction;
            Enemigos = enemigos;
            Rondas = rondas;
            IsNormal = isNormal;
            Game = game;
            Sprite = sprite;
            Normal.Add("TownsFolk", 9);
            Normal.Add("Archer", 7);
            Normal.Add("Knight", 5);
            Normal.Add("Priest", 4);
            Paranormal.Add("Zombi", 8);
            Paranormal.Add("Ghost", 6);
            Paranormal.Add("Lycanthrope", 4);
            Paranormal.Add("Cultist", 3);
        }

        public void Spawn(GameTime gameTime, ref List<Enemies.Enemy> entitiees)
        {
            if (NextRound)
            {
                this.Rondas -= 1;
                NextRound = false;
            }
            if (this.Rondas > 0)
            {

                if (this.Enemigos[this.Enemigos.Length - this.Rondas] <= 0)
                {
                    timeElapsed += gameTime.GetElapsedSeconds();
                    if (timeElapsed > cooldown)
                    {
                        NextRound = true;
                        timeElapsed = 0;
                    }
                }
                else
                {
                    if (cooldownBewteenEnemies)
                    {
                        this.Enemigos[this.Enemigos.Length - this.Rondas] -= 1;
                        this.SpawnEnemies(ref entitiees);
                        cooldownBewteenEnemies = false;
                    }
                    timeBetweenEnemies += gameTime.GetElapsedSeconds();
                    if (timeBetweenEnemies >= enemiesCooldown)
                    {
                        cooldownBewteenEnemies = true;
                        timeBetweenEnemies = 0;
                    }
                }
            }

        }
        public bool SpawnEnemies(ref List<Enemies.Enemy> entities)
        {
            FastRandom r = new FastRandom();
            int max;
            int target;
            var done = false;
            if (IsNormal)
            {
                max = Paranormal.Values.Sum();
                target = r.Next(1, max);
                foreach (var a in Paranormal)
                {
                    if (done)
                    {
                        return done;
                    }
                    if (target <= a.Value)
                    {
                        Debug.WriteLine("Se ha invocado un " + a.Key);
                        entities.Add(GetEnemy(a.Key, out done));
                    }
                    target -= a.Value;
                }
            }
            else
            {
                max = Normal.Values.Sum();
                target = r.Next(1, max);
                foreach (var a in Normal)
                {
                    if (done)
                    {
                        return done;
                    }
                    if (target <= a.Value)
                    {
                        entities.Add(GetEnemy(a.Key, out done));
                    }
                    target -= a.Value;
                }


            }
            return done;
        }


        private Enemies.Enemy GetEnemy(string enemy, out bool done)
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
            done = true;
            return Enemy;
        }
    }
}
