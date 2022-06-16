using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SannaZ_Engine
{
    public class BaseHUD
    {
        protected Texture2D texture;
        public string text = "";
        public Vector2 position;
        public Vector2 startPosition = new Vector2(-1, -1);
        public bool enable = true;
        public int scale;
        public bool DESTRUCTION_ = false; //ok se attivi questa e avvi la funzione lei individua questo oggeto come da cancellare
        public int Layer;
        public TagObject tagHud;

        public enum TypeHUD
        {
            Text, Button
        }

        public TypeHUD typeHUD;

        public BaseHUD()
        {

        }

        public void instanceTexture()
        {
#if DEBUG
            texture = Global.game.Editor.Content.Load<Texture2D>("Button");
#endif
        }

        public BaseHUD(Vector2 position)
        {
            this.position = position;
        }

        public virtual void Initialize()
        {
            if(tagHud == null)
                tagHud = Global.tagsHud[0];
            if (startPosition == new Vector2(-1, -1))
                startPosition = position;
        }

        public virtual void Load(ContentManager content)
        {

        }

        public virtual void Update()
        { }

        public virtual void Draw(SpriteBatch spriteBatch, ContentManager content)
        {

        }
    }
}
