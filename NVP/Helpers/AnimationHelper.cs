using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spritesheet;
using System.Collections.Generic;
using a = Spritesheet;
namespace NVP.Helpers
{
    public class AnimationHelper<T>
    {
        private bool IsPaused;
        private a.Spritesheet Spritesheet;
        private Animation Animation;
        private Dictionary<T, Animation> AnimationsKeys;
        private SpriteBatch Sprite;
        (int x, int y) size;
        public AnimationHelper(SpriteBatch sprite, Texture2D spritesheet, int width, int height)
        {
            size = (width, height);
            Spritesheet = new a.Spritesheet(spritesheet).WithGrid(size);
            Sprite = sprite;
        }

        public void Play(T toPlay, Repeat.Mode mode = Repeat.Mode.LoopWithReverse)
        {
            Animation = AnimationsKeys[toPlay];
            if (IsPaused)
            {
                Animation.Resume();
                return;
            }
            IsPaused = false;
            Animation.Start(mode);
        }

        public void Pause()
        {
            Animation.Pause();
            IsPaused = true;
        }
        public void Stop()
        {
            Animation.Stop();
            IsPaused = false;
        }
        public void Reset()
        {
            Animation.Reset();
            IsPaused = false;

        }

        public void FlipX()
        {
            Animation.FlipX();
        }
        public void FlipY()
        {
            Animation.FlipY();
        }

        public void CreateAnimation(T Key, (int x, int y)[] Frames, bool flipx = false, bool flipy = false)
        {

            if (flipx)
            {
                AnimationsKeys.Add(Key, Spritesheet.CreateAnimation(Frames).FlipX());
            }
            else if (flipy)
            {
                AnimationsKeys.Add(Key, Spritesheet.CreateAnimation(Frames).FlipY());

            }
            else
            {
                AnimationsKeys.Add(Key, Spritesheet.CreateAnimation(Frames));

            }
        }
        public void Update(GameTime gameTime)
        {
            Animation.Update(gameTime);
        }
        public void Draw(Vector2 Position, float RotationDegrees)
        {
            Sprite.Draw(Animation, Position, Color.White, RotationDegrees);

        }
    }
}
