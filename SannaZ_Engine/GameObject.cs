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
	public class GameObject
	{
		protected Texture2D image;
		public Vector2 position;
		public Color drawColor = Color.White;
		public float scale = 1f, rotation = 0f;
		public float layerDepth = .5f;
		public bool active = true;
		public Vector2 center;

		protected TagObject tileTag;
		public TagObject previousTag;

		public bool collidable = true;
		public bool applyGravity = true;
		protected float boundingBoxWidht, boundingBoxHeight;
		protected Vector2 boundingBoxOffset;
		protected Texture2D boundingBoxImage;
		public const bool drawBoundingBoxes = true;
		protected Vector2 direction = new Vector2(1, 0);

		public Vector2 startPosition = new Vector2(-1, -1);

		public string spriteName;
		public string spritePath;
		public string animationName;

		protected Game1 game1;
#if DEBUG
		protected Editor editor;
#endif
		bool alreadyLoadedGameAndEditor = false;

		public bool blocca = false;
		public bool visible = true;

		public TagObject tagsObject;
		public int Layer;
		public Rectangle BoundingBox
		{
			get 
			{
				return new Rectangle((int)(position.X + boundingBoxOffset.X), (int)(position.Y + boundingBoxOffset.Y), (int)boundingBoxWidht, (int)boundingBoxHeight);
			}
		}

        public enum TypeObject
        {
			Enemy, Player, Tile
		}

		public TypeObject typeObject;
		public bool DESTRUCTION_ = false;

		public GameObject()
        {
			if (Global.game != null)
			{
				alreadyLoadedGameAndEditor = true;
				game1 = Global.game;
#if DEBUG
				if (Global.game.editor != null)
					editor = Global.game.editor;
#endif
			}
        }

		public virtual void Initialize()
		{
			if (startPosition == new Vector2(-1, -1))
				startPosition = position;
		}

		public virtual void SetToDefaultPosition()
        {
			position = startPosition;
        }

		public virtual void Load(ContentManager content)
		{
			string nulla = "";
			boundingBoxImage = TextureLoader.Load("pixel", content, ref nulla);

			CalculateCenter();

			if(image != null)
			{
				boundingBoxWidht = image.Width*scale;
				boundingBoxHeight = image.Height*scale;
			}

		}

		public virtual void Update(List<GameObject> objects, Map map)
		{
			if (!alreadyLoadedGameAndEditor)
			{
				if (game1 == null && Global.game != null)
				{
					game1 = Global.game;
#if DEBUG
					if (editor == null && Global.game.editor != null)
						editor = Global.game.editor;
#endif
					alreadyLoadedGameAndEditor = true;
				}
			}
		}

		public virtual bool CheckCollision(Rectangle input)
		{
			return BoundingBox.Intersects(input);
		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			//Vector2 drawingPosition;
			//Vector2 drawingPos;
			//drawingPosition = position;
			//drawingPos.X = (float)Math.Floor(drawingPosition.X * 128);
			//drawingPos.Y = (float)Math.Floor(drawingPosition.Y * 128);
			
			if (image != null && active == true && visible == true)
				spriteBatch.Draw(image, position, null, drawColor, rotation, Vector2.Zero, scale, SpriteEffects.None, layerDepth);
		}

		private void CalculateCenter()
		{
			if (image == null)
				return;
			center.X = (image.Width / 2)*scale;
			center.Y = (image.Height / 2)*scale;
		}


		public virtual void LoadAnimation()
		{ }
		public virtual void LoadSprite(ContentManager content)
		{ }
		public virtual void bloccoRandomColpito()
		{ }
		public virtual void bloccoMuroColpito()
		{ }

		public virtual void autoDestruction()
        { }


	}
}
