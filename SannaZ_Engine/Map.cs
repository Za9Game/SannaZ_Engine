using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;	 

namespace SannaZ_Engine
{
	public class Map
	{
		public List<BoxCollider> boxesCollider = new List<BoxCollider>();
		Texture2D boxColliderImage;

		public int mapWidth = 15;
		public int mapHeight = 9;
		public int tileSize = 64;
		 
		public void Load(ContentManager content)
		{
			string nulla = "";
			boxColliderImage = TextureLoader.Load("pixel", content, ref nulla);
		}

		public void Initialize()
		{
			for (int i = 0; i < boxesCollider.Count; i++)
				boxesCollider[i].Initialize();
		}

		public void Update()
		{
			for (int i = 0; i < boxesCollider.Count; i++)
				boxesCollider[i].Update();
		}


		public Rectangle CheckCollision(Rectangle input)
		{
			for (int i = 0; i < boxesCollider.Count; i++)
			{
				if (boxesCollider[i] != null && boxesCollider[i].boxCollider.Intersects(input) == true)
					return boxesCollider[i].boxCollider;
			}

			return Rectangle.Empty;
		}
		public BoxCollider CheckCollisionBoxCollider(Rectangle input)
		{
			List<int> listaPossibiliBoxColldier = new List<int>();

			for (int i = 0; i < boxesCollider.Count; i++)
				if (boxesCollider[i] != null && Intersects(boxesCollider[i].boxCollider, input) == true)
					listaPossibiliBoxColldier.Add(i);

			if(listaPossibiliBoxColldier.Count == 1)
				return boxesCollider[listaPossibiliBoxColldier[0]];

            #region RightCollisionDectect
            else if (listaPossibiliBoxColldier.Count > 1)
            {
				int distanza=0;
				int index = 0;
				for(int i=0;i<listaPossibiliBoxColldier.Count;i++)
                {
					if(i==0)
					{
						index = i;
						if ((input.Left > boxesCollider[listaPossibiliBoxColldier[i]].boxCollider.Left && input.Left < boxesCollider[listaPossibiliBoxColldier[i]].boxCollider.Right) && (input.Right > boxesCollider[listaPossibiliBoxColldier[i]].boxCollider.Left && input.Right < boxesCollider[listaPossibiliBoxColldier[i]].boxCollider.Right))
						{
							if (input.Right - boxesCollider[listaPossibiliBoxColldier[i]].boxCollider.Left > boxesCollider[listaPossibiliBoxColldier[i]].boxCollider.Right - input.Left)
							{
								distanza = input.Right - boxesCollider[listaPossibiliBoxColldier[i]].boxCollider.Left;
							}
							else
							{
								distanza = boxesCollider[listaPossibiliBoxColldier[i]].boxCollider.Right - input.Left;
							}
						}
						else if(input.Left > boxesCollider[listaPossibiliBoxColldier[i]].boxCollider.Left && input.Left < boxesCollider[listaPossibiliBoxColldier[i]].boxCollider.Right)
                        {
							distanza = boxesCollider[listaPossibiliBoxColldier[i]].boxCollider.Right - input.Left;
						}
                        else
                        {
							distanza = input.Right - boxesCollider[listaPossibiliBoxColldier[i]].boxCollider.Left;
						}
					}
                    else
                    {
						if ((input.Left > boxesCollider[listaPossibiliBoxColldier[i]].boxCollider.Left && input.Left < boxesCollider[listaPossibiliBoxColldier[i]].boxCollider.Right) && (input.Right > boxesCollider[listaPossibiliBoxColldier[i]].boxCollider.Left && input.Right < boxesCollider[listaPossibiliBoxColldier[i]].boxCollider.Right))
						{
							if (input.Right - boxesCollider[listaPossibiliBoxColldier[i]].boxCollider.Left > boxesCollider[listaPossibiliBoxColldier[i]].boxCollider.Right - input.Left)
							{
								if (distanza < input.Right - boxesCollider[listaPossibiliBoxColldier[i]].boxCollider.Left)
								{
									distanza = input.Right - boxesCollider[listaPossibiliBoxColldier[i]].boxCollider.Left;
									index = i;
								}
							}
							else
							{
								if (distanza < boxesCollider[listaPossibiliBoxColldier[i]].boxCollider.Right - input.Left)
								{
									distanza = boxesCollider[listaPossibiliBoxColldier[i]].boxCollider.Right - input.Left;
									index = i;
								}
							}
						}
						else if(input.Left > boxesCollider[listaPossibiliBoxColldier[i]].boxCollider.Left && input.Left < boxesCollider[listaPossibiliBoxColldier[i]].boxCollider.Right)
                        {
							if (distanza < boxesCollider[listaPossibiliBoxColldier[i]].boxCollider.Right - input.Left)
							{
								distanza = boxesCollider[listaPossibiliBoxColldier[i]].boxCollider.Right - input.Left;
								index = i;
							}
						}
                        else
                        {
							if (distanza < input.Right - boxesCollider[listaPossibiliBoxColldier[i]].boxCollider.Left)
							{
								distanza = input.Right - boxesCollider[listaPossibiliBoxColldier[i]].boxCollider.Left;
								index = i;
							}
						}
					}

				}
				return boxesCollider[listaPossibiliBoxColldier[index]];
			}
            #endregion

            return null;
		}

		private bool Intersects(Rectangle input1, Rectangle input2)
        {
			if ((input2.Left > input1.Left && input2.Left < input1.Right) || (input2.Right > input1.Left && input2.Right < input1.Right))
            {
				if((input2.Top > input1.Top && input2.Top < input1.Bottom) || (input2.Bottom > input1.Top && input2.Bottom < input1.Bottom))
                {
					return true;
                }
            }
			return false;
		}

		public void DrawBoxesCollider(SpriteBatch spriteBatch)
		{
#if DEBUG
			for (int i = 0; i < boxesCollider.Count; i++)
			{
				if (boxesCollider[i] != null && boxesCollider[i].active == true)
					spriteBatch.Draw(boxColliderImage, new Vector2(boxesCollider[i].boxCollider.X, boxesCollider[i].boxCollider.Y), boxesCollider[i].boxCollider, new Color(80, 80, 100, 80), 0f, Vector2.Zero, 1f, SpriteEffects.None, .1f);
			}
#endif
		}

		public Point GetTileIndex(Vector2 inputPosition)
        {
			if (inputPosition == new Vector2(-1, -1))
				return new Point(-1, -1);

			return new Point((int)inputPosition.X / tileSize, (int)inputPosition.Y / tileSize);
        }


	}

	public class BoxCollider
	{
		public virtual bool CheckCollision(Rectangle input)
		{
			return boxCollider.Intersects(input);
		}

		public Rectangle boxCollider;
		public bool active = true;
		public TagObject tagBoxCollider;

		public BoxCollider()
		{	}

		public BoxCollider(Rectangle inputRectangle)
		{
			boxCollider = inputRectangle;
		}

		public void Initialize()
        {	}

		public void Update()
        {
			if (tagBoxCollider == null)
			{
				tagBoxCollider = Global.tagsBoxCollider[0]; //(null)
			}
#if !DEBUG
			if(tagBoxCollider.tag == "InizioLivello")
				if (boxCollider.X < Camera.screenRect.Left)
					boxCollider.X = Camera.screenRect.Left;
#endif
        }


	}
}
