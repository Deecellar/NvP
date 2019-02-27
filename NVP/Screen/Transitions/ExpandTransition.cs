using GeonBit.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVP.Screen.Transitions
{
    public class ExpandTransition : Transition
    {
        private readonly GraphicsDevice _graphicsDevice;
        private readonly SpriteBatch _spriteBatch;

        public ExpandTransition(GraphicsDevice graphicsDevice, Color color, float duration = 1.0f)
            : base(duration)
        {
            Color = color;

            _graphicsDevice = graphicsDevice;
            _spriteBatch = new SpriteBatch(graphicsDevice);
        }

        public override void Dispose()
        {
            _spriteBatch.Dispose();
        }

        public Color Color { get; }

        public override void Draw(GameTime gameTime)
        {
            var halfWidth = _graphicsDevice.Viewport.Width / 2f;
            var halfHeight = _graphicsDevice.Viewport.Height / 2f;
            var x = halfWidth * (1.0f - Value);
            var y = halfHeight * (1.0f - Value);
            var width = _graphicsDevice.Viewport.Width * Value;
            var height = _graphicsDevice.Viewport.Height * Value;
            var rectangle = new RectangleF(x, y, width, height);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.FillRectangle(rectangle, Color);
            _spriteBatch.End();
        }
    }
}