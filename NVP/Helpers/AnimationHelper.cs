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
        private Dictionary<T, Animation> AnimationsKeys = new Dictionary<T, Animation>();
        private SpriteBatch Sprite;
        private (int x, int y) size;

        public AnimationHelper(SpriteBatch sprite, Texture2D spritesheet, int width, int height)
        {
            size = (width, height);
            Spritesheet = new a.Spritesheet(spritesheet).WithGrid(size);
            Sprite = sprite;
        }

        public void Play(T toPlay, Repeat.Mode mode = Repeat.Mode.LoopWithReverse)
        {
            if (Animation != null)
                if (Animation == AnimationsKeys[toPlay])
                    return;

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

        public void CreateAnimation(T Key, (int x, int y)[] frames, bool flipx = false, bool flipy = false)
        {
            List<(int x, int y)> FixedFrames = new List<(int x, int y)>();
            foreach (var f in frames)
            {
                FixedFrames.Add((f.y, f.x));
            }
            (int x, int y)[] Frames = FixedFrames.ToArray();
            if (flipx && flipy)
            {
                AnimationsKeys.Add(Key, Spritesheet.CreateAnimation(Frames).FlipX().FlipY());
            }
            else if (flipx)
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
            if (Animation != null)
                Animation.Update(gameTime);
        }

        public void Draw(Vector2 Position, float RotationDegrees)
        {
            if (Animation != null)

                Sprite.Draw(Animation, Position, Color.White, RotationDegrees);
        }
    }
}