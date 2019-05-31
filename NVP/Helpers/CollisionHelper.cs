using System.Collections.Generic;

namespace NVP.Helpers
{
    public class CollisionHelper
    {
        private List<ICollisionableObject> Collisionables = new List<ICollisionableObject>();

        public void Initialize(params ICollisionableObject[] collisionableObjects)
        {
            foreach (var c in collisionableObjects)
            {
                Collisionables.Add(c);
            }
        }

        public void Initialize(List<ICollisionableObject> collisionableObjects)
        {
            Collisionables = collisionableObjects;
        }

        public void Update()
        {
            foreach (var c in Collisionables)
            {
                if (!c.IsActive)
                    continue;
                foreach (var c2 in Collisionables)
                {
                    if (!c2.IsActive)
                        continue;
                    if (c.Collider.Intersects(c2.Collider))
                    {
                        c.OnCollision(c2);
                    }
                }
            }
        }
    }
}