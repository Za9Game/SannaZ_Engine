using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SannaZ_Engine
{
    public class ScriptCharacter : Character
    {
        public ScriptCharacter()
        {
        }

        public override void Initialize()
        {

            base.Initialize();
        }

        public override void Load(ContentManager content)
        {

            base.Load(content);
        }

        public override void Update(List<GameObject> objects, Map map)
        {
            base.Update(objects, map);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            base.Draw(spriteBatch);
        }


        public override void LoadAnimation()
        {
            LoadAnimation(animationName);
            
            if (animationName != null)
            {
                boundingBoxOffset.X = 0; boundingBoxOffset.Y = 0; boundingBoxWidht = animationSet.width; boundingBoxHeight = animationSet.height;
            }
        }

        public override void LoadSprite(ContentManager content)
        {
            if (spriteName != null)
            {
                string nulla = "";
                image = TextureLoader.Load(spriteName, content, ref nulla);
                spritePath = content.RootDirectory.Remove(0, 7);
                content.RootDirectory = "Content";
                boundingBoxOffset.X = 0; boundingBoxOffset.Y = 0; boundingBoxWidht = image.Width; boundingBoxHeight = image.Height;
            }
            else
                image = null;
        }
    }
}
