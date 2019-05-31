using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Timers;
using NVP.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NVP.Entities
{
    internal class Spawner
    {
        private Vector2 Position { get; set; }
        private char Direction { get; set; }
        public int[] Enemigos { get; set; }
        public int Rondas { get; set; }
        public bool NextRound { get; private set; }
        public bool IsDone = false;
        private Dictionary<string, int> Normal = new Dictionary<string, int>();
        private Dictionary<string, int> Paranormal = new Dictionary<string, int>();
        private Game Game;
        private SpriteBatch Sprite;
        private bool IsNormal = true;
        private List<Bullet> Bullet;
        private CountdownTimer timerRounds;
        private CountdownTimer enemiesTimer;

        public Spawner(Game game, SpriteBatch sprite, Vector2 position, char direction, int[] enemigos, int rondas, bool isNormal, ref List<Bullet> bullets)
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
            Paranormal.Add("Ghost", 9);
            Paranormal.Add("Lycanthrope", 4);
            Paranormal.Add("Cultist", 3);
            timerRounds = new CountdownTimer(System.TimeSpan.FromSeconds(30 + 2.5 * Enemigos[0]));
            enemiesTimer = new CountdownTimer(System.TimeSpan.FromSeconds(2.5));
            Bullet = bullets;
        }

        public void Spawn(GameTime gameTime, ref List<Enemies.Enemy> entitiees)
        {
            if (NextRound)
            {
                this.Rondas -= 1;
                NextRound = false;
                if (Rondas > 0)
                {
                    timerRounds.Interval = System.TimeSpan.FromSeconds(10 + 2.5 * Enemigos[this.Enemigos.Length - this.Rondas]);
                    timerRounds.Restart();
                }
                else
                {
                    IsDone = true;
                }
            }
            if (this.Rondas > 0)
            {
                if (this.Enemigos[this.Enemigos.Length - this.Rondas] <= 0)
                {
                    if (timerRounds.State == TimerState.Completed)
                    {
                        NextRound = true;
                    }
                }
                else
                {
                    if (enemiesTimer.State == TimerState.Completed)
                    {
                        this.Enemigos[this.Enemigos.Length - this.Rondas] -= 1;
                        this.SpawnEnemies(ref entitiees);
                        enemiesTimer.Restart();
                    }
                }
            }
            timerRounds.Update(gameTime);
            enemiesTimer.Update(gameTime);
        }

        public bool SpawnEnemies(ref List<Enemies.Enemy> entities)
        {
            Random r = new Random();
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
            Enemies.Enemy Enemy = new Enemies.Enemy(Game, Position, RandomSpriteHelper.GenerateRandomSprite(Game.Content, Game.GraphicsDevice, enemy), Sprite); ;
            switch (enemy)
            {
                case "Archer":
                    Enemy = new Enemies.Archer(Game, Position, RandomSpriteHelper.GenerateRandomSprite(Game.Content, Game.GraphicsDevice, enemy), Sprite);
                    break;

                case "Knight":
                    Enemy = new Enemies.Knight(Game, Position, RandomSpriteHelper.GenerateRandomSprite(Game.Content, Game.GraphicsDevice, enemy), Sprite);
                    break;

                case "Priest":
                    Enemy = new Enemies.Priest(Game, Position, RandomSpriteHelper.GenerateRandomSprite(Game.Content, Game.GraphicsDevice, enemy), Sprite);
                    break;

                case "TownsFolk":
                    Enemy = new Enemies.TownsFolk(Game, Position, RandomSpriteHelper.GenerateRandomSprite(Game.Content, Game.GraphicsDevice, enemy), Sprite);
                    break;

                case "Zombi":
                    Enemy = new Enemies.Zombi(Game, Position, RandomSpriteHelper.GenerateRandomSprite(Game.Content, Game.GraphicsDevice, enemy), Sprite);
                    break;

                case "Ghost":
                    Enemy = new Enemies.Ghost(Game, Position, RandomSpriteHelper.GenerateRandomSprite(Game.Content, Game.GraphicsDevice, enemy), Sprite);
                    break;

                case "Lycanthrope":
                    Enemy = new Enemies.Lycanthrope(Game, Position, RandomSpriteHelper.GenerateRandomSprite(Game.Content, Game.GraphicsDevice, enemy), Sprite);
                    break;

                case "Cultist":
                    Enemy = new Enemies.Cultist(Game, Position, RandomSpriteHelper.GenerateRandomSprite(Game.Content, Game.GraphicsDevice, enemy), Sprite);
                    break;
            }
            Enemy.DirectionToGo(Direction);
            Enemy.SetBullets(ref Bullet);
            done = true;
            return Enemy;
        }
    }
}