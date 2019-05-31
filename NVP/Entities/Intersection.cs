using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace NVP.Entities
{
    public class Intersection
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
            if (enemy.Collider.Contains(Bounds.ClosestPointTo(Bounds.Center)) || Bounds.Intersects(enemy.Collider.ToRectangle()))
            {
                enemy.DirectionToGo(Direction);
            }
        }
    }
}