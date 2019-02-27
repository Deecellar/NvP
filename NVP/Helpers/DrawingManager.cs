
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVP.Helpers
{
    public class DrawingManager 
    {
        List<IDrawable> drawables = new List<IDrawable>();

        public List<IDrawable> Drawables { get => drawables; set => drawables = value; }

    }
}
