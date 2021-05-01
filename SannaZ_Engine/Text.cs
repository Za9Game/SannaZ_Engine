using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SannaZ_Engine
{
    public class Text : BaseHUD
    {
        private SpriteFont font;
        private Color color = Color.White;
        private float transparent = 1; //più è basso il valore più è trasparente

        public Text()
        { }
        public Text(string text)
        {
            this.text = text;
        }
        public Text(string text, Vector2 position, TagObject tagHud)
        {
            this.text = text;
            this.tagHud = tagHud;
            this.position = position;
        }
        public Text(Vector2 position)
        {
            this.position = position;
        }
        public Text(Vector2 position, string text)
        {
            this.position = position;
            this.text = text;
        }



        public override void Load(ContentManager content)
        {
            font = content.Load<SpriteFont>("Fonts/Emulogic");
            scale = 20;
            base.Load(content);
        }

        public override void Update()
        {
            if (tagHud != null)
            {
                text = TextForTag.Update(text, tagHud);

                if (tagHud.tag == "Scomparsa")
                {
                    transparent -= 0.016f;
                    position.Y -= 0.25f;
                    if (transparent <= 0)
                    {
                        DESTRUCTION_ = true;
                        Global.game.DestroyObject(this, 1);
                    }
                }
            }
            base.Update();
        }
        
        public override void Draw(SpriteBatch spriteBatch, ContentManager content)
        {
            if (enable == true)
            {
                if (font == null)
                    font = content.Load<SpriteFont>("Fonts/Emulogic");
                if (font != null)
                {
                    if(text == "")
                        text = "texto mancante";
                    Vector2 cameraPosition = new Vector2(Camera.screenRect.X, Camera.screenRect.Y);
                    spriteBatch.DrawString(font, text, cameraPosition + position, color * transparent, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

                }
            }
            base.Draw(spriteBatch, content);
        }

    }
}
