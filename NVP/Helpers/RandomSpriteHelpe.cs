using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace NVP.Helpers
{
    public static class RandomSpriteHelper
    {
        private static Texture2D CreateSprite(GraphicsDevice device, Texture2D Hair, Texture2D Body, Texture2D Clothes)
        {
            RenderTarget2D NewTexture = new RenderTarget2D(device, Body.Width, Body.Height);
            device.SetRenderTarget(NewTexture);
            device.Clear(Color.Transparent);
            SpriteBatch sprite = new SpriteBatch(device);
            sprite.Begin();
            sprite.Draw(Body, Vector2.Zero, Color.White);
            sprite.Draw(Clothes, Vector2.Zero, Color.White);
            sprite.Draw(Hair, Vector2.Zero, Color.White);

            sprite.End();
            device.SetRenderTarget(null);
            return NewTexture;
        }

        public static Texture2D GenerateRandomSprite(this ContentManager content, GraphicsDevice graphicsDevice, string Class)
        {
            Random r = new Random();
            Texture2D texture2D = content.Load<Texture2D>("Sprites/Premade/Licantropo"); ;
            switch (Class)
            {
                case "Archer":
                    texture2D = CreateSprite(graphicsDevice, content.GetHair(graphicsDevice, false), content.GetBase(), content.Load<Texture2D>("Sprites/Ropa/Archer"));
                    break;

                case "Cultist":
                    texture2D = CreateSprite(graphicsDevice, content.GetHair(graphicsDevice, false), content.GetBase(), content.Load<Texture2D>("Sprites/Ropa/CultistRobe"));
                    break;

                case "Ghost":
                    texture2D = CreateSprite(graphicsDevice, content.GetHair(graphicsDevice, false), content.GetBase(), content.Load<Texture2D>("Sprites/Ropa/PriestRobe"));
                    break;

                case "Knight":
                    texture2D = CreateSprite(graphicsDevice, content.GetHair(graphicsDevice, false), content.GetBase(), content.Load<Texture2D>("Sprites/Ropa/Armor"));
                    break;

                case "Lycanthrope":
                    texture2D = content.Load<Texture2D>("Sprites/Premade/Licantropo");
                    break;

                case "Priest":
                    texture2D = CreateSprite(graphicsDevice, content.GetHair(graphicsDevice, false), content.GetBase(), content.Load<Texture2D>("Sprites/Ropa/PriestRobe"));
                    break;

                case "TownsFolk":
                    texture2D = CreateSprite(graphicsDevice, content.GetHair(graphicsDevice, new bool[] { true, false }[new Random().Next(0, 1)]), content.GetBase(), content.Load<Texture2D>(new string[] { "Sprites/Ropa/Male/Male-TownFolk", "Sprites/Ropa/Female/Female-TownFolk" }[new Random().Next(0, 1)]));
                    break;

                case "Zombi":
                    texture2D = content.Load<Texture2D>("Sprites/Premade/" + new[] { "Male", "Female" }[r.Next(0, 1)] + "-Zombie");
                    break;
            }
            return texture2D;
        }

        public static Texture2D GetBase(this ContentManager content)
        {
            return content.Load<Texture2D>(content.ContentsOnFolder("Sprites/Cuerpo"));
        }

        private static Texture2D GetHair(this ContentManager content, GraphicsDevice device, bool Female = true)
        {
            Texture2D hairs;

            if (Female)
            {
                hairs = content.Load<Texture2D>(content.ContentsOnFolder("Sprites/Pelo/Female"));
            }
            else
            {
                hairs = content.Load<Texture2D>(content.ContentsOnFolder("Sprites/Pelo/Male"));
            }
            var rect = new Rectangle(0 + (48) * new Random().Next(0, 10), 0, 48, 72);
            var rend = new RenderTarget2D(device, 48, 72);
            device.SetRenderTarget(rend);
            device.Clear(Color.Transparent);
            var Sprite = new SpriteBatch(device);
            Sprite.Begin();
            Sprite.Draw(hairs, Vector2.Zero, rect, Color.White);
            Sprite.End();
            device.SetRenderTarget(null);
            return rend;
        }

        public static string ContentsOnFolder(this ContentManager contentManager, string contentFolder)
        {
            DirectoryInfo dir = new DirectoryInfo(contentManager.RootDirectory + "\\" + contentFolder);
            if (!dir.Exists)
                throw new DirectoryNotFoundException();

            FileInfo[] files = dir.GetFiles("*.*");
            string[] result = new string[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                result[i] = Path.GetFileNameWithoutExtension(files[i].Name);
            }

            return contentFolder + "/" + result[new Random().Next(0, result.Length)];
        }
    }
}