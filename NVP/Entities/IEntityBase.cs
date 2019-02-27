using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVP.Entities
{
    public interface IEntityBase : IDrawable, IUpdateable
    {
        string Name { get; set; }
        Vector2 Position { get; set; }
        float RotationDegrees { get; set; }
        Quaternion Rotation { get; set; }
        float Health { get;}
        bool IsEnemy { get; set; }
        Texture2D Image { get; set; }

        void TakeDamage();
        void GiveDamage(IEntityBase entity);

    }
}
