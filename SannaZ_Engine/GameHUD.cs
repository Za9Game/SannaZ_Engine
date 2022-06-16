using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SannaZ_Engine
{
    public class GameHUD
    {
        public List<BaseHUD> baseHUD = new List<BaseHUD>();

        public void Initialize()
        {
            for(int i=0; i< baseHUD.Count; i++)
            {
                baseHUD[i].Initialize();
            }
        }

        public void Load(ContentManager content)
        {
            for(int i=0;i< baseHUD.Count;i++)
            {
                baseHUD[i].Load(content);
            }
        }

        public void Update()
        {
            for (int i = 0; i < baseHUD.Count; i++)
            {
                baseHUD[i].Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch, ContentManager content)
        {
            for (int i = 0; i < baseHUD.Count; i++)
            {
                baseHUD[i].Draw(spriteBatch, content);
            }
        }
        public void Draw(SpriteBatch spriteBatch, ContentManager content, GameTime gameTime)
        {
            for (int i = 0; i < baseHUD.Count; i++)
            {
                baseHUD[i].Draw(spriteBatch, content);
            }
        }

    }
}
