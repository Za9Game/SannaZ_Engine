using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SannaZ_Engine
{
	public class Tile : ScriptCharacter
	{
		ContentManager provisoryContent;
		string originarySpriteName;
		bool alreadyLoaded = false;

		public Tile()
        {
        }

		public override void Initialize()
		{
			active = true;
			collidable = false;
			typeObject = TypeObject.Tile;
			tileTag = tagsObject;
			applyGravity = false;

			base.Initialize();
		}

		public override void Load(ContentManager content)
		{			
			provisoryContent = content;
			if (tagsObject == null)
				tagsObject = Global.tagsObject[0];
			if (previousTag == null)
				previousTag = Global.tagsObject[0];
			else if(previousTag.tag == "BloccoRandom" && tagsObject.tag == "Nulla")
            {
				spriteName = "BloccoRandomUsattoSpriteSheet";
			}
			if (tagsObject.tag == "BloccoRandom")
			{
				spriteName = "BloccoRandom_SpriteSheet";
            }
			else if (tagsObject.tag == "Muri")
            {
				spriteName = "MuroSaltaSpriteSheet";
			}
			else if (tagsObject.tag == "Pali")
			{
				spriteName = "PaliSpriteSheet";
			}
			else if(tagsObject.tag == "Moneta")
            {
				spriteName = "MonetaSpriteSheet";
            }
			if (tagsObject.tag == "Nulla")
            {
				if(!alreadyLoaded)
					originarySpriteName = spriteName;
				if(originarySpriteName != null)
					spriteName = originarySpriteName;
				LoadAnimation(null);
			}
			if (spriteName != null)
			{
				alreadyLoaded = true;
				string nulla = "";
				content.RootDirectory += spritePath;
				image = TextureLoader.Load(spriteName, content, ref nulla);
				if(nulla != "")
                {
					spritePath = nulla;
				}
				content.RootDirectory = "Content";
			}
			if (tagsObject.tag == "BloccoRandom")
			{
				animationName = "BloccoRandomAnimation.anm";
				LoadAnimation(animationName);
				ChangeAnimation(Animations.BloccoRandomAnimation);
			}
			else if (tagsObject.tag == "Pali")
			{
				animationName = "PaliAnimation.anm";
				LoadAnimation(animationName);
				ChangeAnimation(Animations.Idle);
			}
			else if (tagsObject.tag == "Muri")
			{
				animationName = "MuroSalto.anm";
				LoadAnimation(animationName);
				ChangeAnimation(Animations.Idle);
			}
			else if(previousTag.tag == "BloccoRandom" && tagsObject.tag == "Nulla")
            {
				animationName = "BloccoRandomUsatoSalto.anm";
				LoadAnimation(animationName);
				ChangeAnimation(Animations.Salto);
			}
			else if(tagsObject.tag == "Moneta")
            {
				animationName = "MonetaAnimation.anm";
				LoadAnimation(animationName);
				ChangeAnimation(Animations.Giro);
			}
			base.Load(content);
		}
		int ripetizioni = 3;
		public override void Update(List<GameObject> objects, Map map)
		{
			if (tagsObject == null)
				tagsObject = Global.tagsObject[0];
			if (previousTag == null)
				previousTag = Global.tagsObject[0];
			if (tileTag == null)
				tileTag = tagsObject;

			if (tileTag.key != tagsObject.key && tileTag.tag != tagsObject.tag)
            {
				previousTag = tileTag;
				tileTag = tagsObject;
				if((previousTag.tag != "BloccoRandom") && (tagsObject.tag == "BloccoRandom"))
					position.Y += 20;
				if ((previousTag.tag == "BloccoRandom") && tagsObject.tag != "BloccoRandom")
					position.Y -= 20;
				Load(provisoryContent);
            }
			if(tagsObject.tag == "Moneta")
            {
				if (ripetizioni < 12)
				{
					ripetizioni++;
					Jump(2);
                }
                else
                {
					stillJumping = false;
				}
            }

			base.Update(objects, map);
		}

        public override void bloccoRandomColpito()
        {
			tagsObject = Global.tagsObject[0];
			previousTag = tileTag;
			tileTag = tagsObject;
			if (previousTag.tag == "BloccoRandom" && tagsObject.tag == "Nulla")
				position.Y -= 20;
			if(Layer == 0)//esce moneta
            {
				Tile moneta = new Tile();
				if (Global.TagExist(Global.tagsObject, "Moneta"))
					moneta.tagsObject = Global.tagsObject[Global.TagIndex(Global.tagsObject, "Moneta")];
				else
                {
					Global.tagsObject.Add(new TagObject(Global.tagsObject.Count, "Moneta"));
					moneta.tagsObject = Global.tagsObject[Global.tagsObject.Count-1];
                }
				moneta.layerDepth = 0.4f;
				moneta.typeObject = GameObject.TypeObject.Tile;
				moneta.position = new Vector2(this.position.X + this.center.X/6, this.position.Y);
				moneta.applyGravity = true;
				moneta.collidable = false;
				moneta.Load(Global.game.Content);
				Global.game.objects.Add(moneta);
			}
			Load(provisoryContent);
		}

        public override void bloccoMuroColpito()
        {
			ChangeAnimation(Animations.Salto);
			base.bloccoMuroColpito();
        }

        public override void ActionAnimationComplete()
        {
			if (tagsObject.tag == "Muri")
				if (currentAnimation.name == "Salto")
					ChangeAnimation(Animations.Idle); 
			if (previousTag.tag == "BloccoRandom")
				if (currentAnimation.name == "Salto")
					ChangeAnimation(Animations.Idle);

			base.ActionAnimationComplete();
        }

        public override void autoDestruction()
        {
			DESTRUCTION_ = true;
			Global.game.DestroyObject(this, 0);
            base.autoDestruction();
        }


    }
}
