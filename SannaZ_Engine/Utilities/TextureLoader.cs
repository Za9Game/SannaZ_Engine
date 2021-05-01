using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SannaZ_Engine
{
    public static class TextureLoader
    {
        const bool usingPipeline = false;

        public static Texture2D Load(string filePath, ContentManager content, ref string spritePathRight)
        {
            string estensione = ".xnb";
            string estensione2 = ".png";
            if (filePath.Contains(estensione) || filePath.Contains(estensione2))
                filePath = filePath.Remove(filePath.Length - estensione.Length, estensione.Length);
            Texture2D image;
            
            try{
                image = content.Load<Texture2D>(filePath);
            }
            catch { content.RootDirectory += "//Palette//"; image = content.Load<Texture2D>(filePath); spritePathRight = "//Palette//"; }

            if (usingPipeline == false)
                PremultiplyTexture(image);

            return image;
        }

        private static void PremultiplyTexture(Texture2D texture)
        {
            Color[] buffer = new Color[texture.Width * texture.Height];
            texture.GetData(buffer);
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = Color.FromNonPremultiplied(buffer[i].R, buffer[i].G, buffer[i].B, buffer[i].A);
            }
            texture.SetData(buffer);
        }
    }
}
