using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NVP.Entities.PowerUps;
namespace NVP.Entities
{
    public class PowerUpSpawner
    {
        private RectangleF MapBounds;
        private PowerUp[] PowerUps = new PowerUp[8];
        private bool IsAPowerUpOnScreen = false;
        private PowerUp ActivePowerUp = null;

        public PowerUpSpawner(RectangleF Bounds)
        {
            MapBounds = Bounds;

        }


        public void Update(GameTime gameTime)
        {

        }

        public void SetPowerUp()
        {
            
        }
    }
}
