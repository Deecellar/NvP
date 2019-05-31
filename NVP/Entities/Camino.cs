using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace NVP.Entities
{
    public class Camino
    {
        public Vector2 Position { get; set; }
        public BoundingRectangle Bounds { get; set; }

        public Camino(Vector2 pos, RectangleF bound)
        {
            Position = pos;
            Bounds = bound;
        }
    }
}