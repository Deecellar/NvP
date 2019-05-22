using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NVP.Entities
{
    public class InstersectionP
    {
        public Vector2 Position { get; set; }
        public BoundingRectangle Bounds { get; set; }
        public string[] Direction { get; set; }
        public float Probability { get; set; }
        List<Entity> EntitysThatPassed = new List<Entity>();
        public InstersectionP(Vector2 pos, RectangleF bound, string dir, float probability)
        {
            Position = pos;
            Bounds = bound;
            Direction = dir.Split(',');
            Probability = probability / 10;
        }

        public char GetDirection()
        {
            Random random = new Random();
            var LastValue = Direction.Last();
            float randomV = random.NextSingle();
            if (randomV <= Probability)
            {
                return Convert.ToChar(LastValue);
            }
            else
            {
                return Convert.ToChar(Direction[random.Next(0, Direction.Length - 2)]);
            }
        }
        public void ChangeDirection(Enemies.Enemy enemy)
        {
            if (enemy.Collider.Contains(Bounds.ClosestPointTo(Bounds.Center)) && !EntitysThatPassed.Contains(enemy))
            {
                EntitysThatPassed.Add(enemy);
                enemy.DirectionToGo(GetDirection());
            }
        }
    }
}
