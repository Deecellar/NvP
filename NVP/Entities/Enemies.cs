using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NVP.Entities
{
    class Enemies : IEntityBase, IDisposable
    {
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Vector2 Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float RotationDegrees { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Quaternion Rotation { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public float Health => throw new NotImplementedException();

        public bool IsEnemy { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Texture2D Image { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool Enabled => throw new NotImplementedException();

        public int UpdateOrder => throw new NotImplementedException();

        public int DrawOrder => throw new NotImplementedException();

        public bool Visible => throw new NotImplementedException();

        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;
        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> VisibleChanged;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Draw(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void GiveDamage(IEntityBase entity)
        {
            throw new NotImplementedException();
        }

        public void TakeDamage()
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
