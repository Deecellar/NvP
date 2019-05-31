using MonoGame.Extended;

namespace NVP.Helpers
{
    public interface ICollisionableObject
    {
        CircleF Collider { get; set; }
        bool IsActive { get; set; }

        void OnCollision(ICollisionableObject collisionableObject);
    }
}