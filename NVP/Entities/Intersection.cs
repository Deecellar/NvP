using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVP.Entities
{
public    class Intersection
    {
        public Vector2 Position { get; set; }
        public BoundingRectangle Bounds { get; set; }
        public char Direction { get; set; }
        public Intersection(Vector2 pos, RectangleF bound, char dir)
        {
            Position = pos;
            Bounds = bound;
            Direction = dir;
        }

        public void ChangeDirection(Enemies.Enemy enemy)
        {
            if (enemy.BoundingCircle.Contains(Bounds.ClosestPointTo(Bounds.Center)))
            {
                enemy.DirectionToGo(Direction);
            }
        }
    }
}
