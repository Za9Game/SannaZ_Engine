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
    public class Light
	{
		protected Texture2D lightMask;
		public Vector2 position;
		public Color drawColor = Color.White;
		public float scale = 1f, rotation = 0f;
		public bool active = true;
		public Vector2 center;
		public Vector2 startPosition = new Vector2(-1, -1);
		public float layerDepth = 0;
		public float intensity = 0.9f;

		public Light()
		{

		}

		public Light(Vector2 position)
        {
			this.position = position;
        }

		public virtual void Initialize()
		{
			if (startPosition == new Vector2(-1, -1))
				startPosition = position;
		}

		public virtual void Load(ContentManager content)
		{
			lightMask = content.Load<Texture2D>("lightmask");
			CalculateCenter();
		}

		public virtual void Update(List<GameObject> objects, Map map)
		{

		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			if (lightMask != null && active == true)
				spriteBatch.Draw(lightMask, new Vector2(position.X - center.X, position.Y - center.Y), null, new Color(255, 235, 235) * intensity, rotation, Vector2.Zero, scale, SpriteEffects.None, layerDepth);
		}

		private void CalculateCenter()
		{
			if (lightMask == null)
				return;
			center.X = (lightMask.Width / 2) * scale;
			center.Y = (lightMask.Height / 2) * scale;
		}

	}
}
