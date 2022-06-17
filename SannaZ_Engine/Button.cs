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
    public class Button : BaseHUD
    {
        private SpriteFont font;
        private bool isHovering;
        public Color textColor { get; set; }
        private string lastText;
        private Vector2 buttonScale { get; set; }

        public Rectangle rectangle
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, (int)buttonScale.X, (int)buttonScale.Y);
            }
        }

        public Button()
        { }
        
        public override void Initialize()
        {
            texture = Global.game.Content.Load<Texture2D>("Button");
            font = Global.game.Content.Load<SpriteFont>("Fonts/Emulogic");
            textColor = Color.Black;
            base.Initialize();
        }

        public override void Draw(SpriteBatch spriteBatch, ContentManager content)
        {
            if (isHovering)
                textColor = new Color(191, 191, 191);
            else
                textColor = new Color(255, 255, 255);

            if (font == null)
                font = Global.game.Content.Load<SpriteFont>("Fonts/Emulogic");
            if (font != null)
            {
                if (text == "")
                    text = "texto mancante";

                var x = (rectangle.X + (rectangle.Width / 2)) - (font.MeasureString(text).X / 2);
                var y = (rectangle.Y + (rectangle.Height / 2)) - (font.MeasureString(text).Y / 2);
                spriteBatch.DrawString(font, text, new Vector2(x, y), textColor);
            }

        }

        public override void Update()
        {
            if(texture == null)
                texture = Global.game.Content.Load<Texture2D>("Button");
            if(buttonScale == Vector2.Zero)
                buttonScale = new Vector2(texture.Width, texture.Height);

            var mouseRectangle = new Rectangle((int)Input.MousePositionCamera().X, (int)Input.MousePositionCamera().Y, 1, 1);
            isHovering = false;

            if (lastText == "" || lastText == null || lastText != text)
            {
                resizeButtonArea();
                lastText = text;
            }

            if (mouseRectangle.Intersects(rectangle))
            {
                isHovering = true;

                if (Input.mouseState.LeftButton == ButtonState.Released && Input.lastMouseState.LeftButton == ButtonState.Pressed){
                    setUpEventOnClick();
                }
            }
            if(Layer == 1)
            {
                position = new Vector2(Camera.screenRect.Center.X- buttonScale.X/2, Camera.screenRect.Center.Y*1.5f);
            }
        }

        private void resizeButtonArea()
        {
            if (font == null || text == "")
                return;
            buttonScale = new Vector2(font.MeasureString(text).X, font.MeasureString(text).Y);
        }

        private void setUpEventOnClick()
        {
            if(Layer == 1) // PLAY
            {
                Global.game.RestartLevel("Level1.jorge");
            }
        }


    }
}
