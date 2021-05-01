using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Windows.Forms;
using System.Runtime.Remoting.Messaging;

namespace SannaZ_Engine
{
	public class Character : AnimatedObject
	{
		public Vector2 velocity;

		protected float decel = .18f;
		protected float accel = .18f;
		protected float enemyMaxSpeed = 2f;
		protected float maxSpeed = 6f;

		const float gravity = 1f;
		const float jumpVelocity = 5f;
		const float maxJumpVelocity = 15;
		const float maxFallVelocity = 35;
		private float powerJump;
		const float maxPowerJump = 60;
		protected bool jumping;
		protected bool alreeadyJumping;
		const float enemyMovement = 1f;
		protected bool enemyXCollision;

		private float timer = 0.1f;
		private bool activeTimer;

		public Character()
		{
		}

		public override void Initialize()
		{
			alreeadyJumping = false;
			velocity = Vector2.Zero;
			jumping = false;
			base.Initialize();
		}

		public override void Update(List<GameObject> objects, Map map)
		{
			if(activeTimer)
            {
				timer += 0.1f;
				if(timer > 0.5f)
                {
					timer = 0.1f;
					activeTimer = false;
					offJumping();

				}
            }

			UpdateMovement(objects, map);
			base.Update(objects, map);
		}

		private void UpdateMovement(List<GameObject> objects, Map map)
		{
			CheckCollision(map, objects, true);

			if (velocity.X != 0 && CheckCollision(map, objects, true) == true && collidable == true)
			{
				velocity.X = 0;
			}
			position.X += velocity.X;


			if (velocity.Y != 0 && CheckCollision(map, objects, false) == true && collidable == true)
			{
				velocity.Y = 0;
			}
			position.Y += velocity.Y;

			if (applyGravity == true)
				ApplyGravity(map);

			velocity.X = TendToZero(velocity.X, decel);
			if(applyGravity == false)
				velocity.Y = TendToZero(velocity.Y, decel);
		}


		private void ApplyGravity(Map map)
		{
			if (jumping == true || OnGround(map) == Rectangle.Empty)
			{
				velocity.Y += gravity;
			}
			if (velocity.Y > maxFallVelocity)
				velocity.Y = maxFallVelocity;
		}

		protected void EnemyMove(bool xAsis)
        {
			if (xAsis)
				velocity.X += enemyMovement;
			else
				velocity.X -= enemyMovement;

			if (velocity.X > enemyMaxSpeed)
				velocity.X = enemyMaxSpeed;
			if (velocity.X < 0 && velocity.X < -enemyMaxSpeed)
				velocity.X = -enemyMaxSpeed;
			

			if(velocity.Y > 0)
				direction.X = 1;
			if (velocity.Y < 0)
				direction.X = -1;

			direction.Y = 0;
		}

		protected void EnemyStop()
        {
			if (velocity.Y != 0)
				velocity.Y = 0; 
			if (velocity.X != 0)
				velocity.X = 0;
		}

		protected void MoveRight()
		{
			velocity.X += accel + decel;

			if (velocity.X > maxSpeed)
				velocity.X = maxSpeed;

			direction.X = 1;
			direction.Y = 0;
		}
		protected void MoveLeft()
		{
			velocity.X -= accel + decel;

			if (velocity.X < -maxSpeed)
				velocity.X = -maxSpeed;

			direction.Y = 0;
			direction.X = -1;
		}
		protected void MoveDown()
		{
			velocity.Y += accel + decel;

			if (velocity.Y > maxSpeed)
				velocity.Y = maxSpeed;

			direction.X = 0;
			direction.Y = 1;
		}
		protected void MoveUp()
		{
			velocity.Y -= accel + decel;

			if (velocity.Y < -maxSpeed)
				velocity.Y = -maxSpeed;

			direction.X = 0;
			direction.Y = -1;
		}

		protected bool Jump(List<GameObject> objects, Map map)
		{
			if (jumping)
			{
				return false;
			}

			if (velocity.Y - gravity > -maxJumpVelocity && velocity.Y - gravity <= 0)
			{
				alreeadyJumping = true;
				velocity.Y -= jumpVelocity;
				powerJump += -velocity.Y;

				if (powerJump > maxPowerJump)
					jumping = true;

				if (CheckCollision(map, objects, false))
					return false;
				return true;
			}
			return false;
		}

		protected bool stillJumping = false; //per la moneta lo creata che quando inizia a saltare non deve vedere la collisione 
		protected void Jump(int v)
		{
			stillJumping = true;
			alreeadyJumping = true;
			velocity.Y -= v;
			powerJump += -velocity.Y;

			if (powerJump > maxPowerJump)
				jumping = true;
		}

		protected bool JumpAfterKillEnemy(List<GameObject> objects, Map map)
		{
			applyGravity = false;
			float inerzia = velocity.Y*2;
			if (inerzia > 12)
				inerzia = 12;
			velocity.Y = 0;
			position.Y += velocity.Y;
			alreeadyJumping = true;
			velocity.Y -= jumpVelocity + inerzia;
			applyGravity = true;
			if (CheckCollision(map, objects, false))
				return false;
			return true;
		}

		protected virtual bool CheckCollision(Map map, List<GameObject> objects, bool xAsis)
		{
			if (!stillJumping)
			{
				Rectangle futureBoundingBox = BoundingBox;
				int maxX = (int)maxSpeed;
				int maxY = (int)maxSpeed;

				if (applyGravity == true)
					maxY = (int)jumpVelocity;

				if (xAsis == true && velocity.X != 0)
				{
					if (velocity.X > 0)
						futureBoundingBox.X += maxX;
					else
						futureBoundingBox.X -= maxX;
				}
				else if (xAsis == false && velocity.Y != 0)
				{
					if (velocity.Y > 0)
						futureBoundingBox.Y += maxY;
					else
						futureBoundingBox.Y -= maxY;
				}

				BoxCollider boxCollision = new BoxCollider();

				if (typeObject == TypeObject.Player || typeObject == TypeObject.Enemy)
					boxCollision = map.CheckCollisionBoxCollider(futureBoundingBox);
				else
					boxCollision.boxCollider = map.CheckCollision(futureBoundingBox);

				if (boxCollision != null)
				{
					if (boxCollision.boxCollider != Rectangle.Empty)
					{
						if (applyGravity == true && velocity.Y >= gravity && (futureBoundingBox.Bottom > boxCollision.boxCollider.Top - maxSpeed) && (futureBoundingBox.Bottom <= boxCollision.boxCollider.Top + velocity.Y))
						{
							LandResponse(boxCollision.boxCollider);
							return true;
						}
						if (game1 != null && typeObject == TypeObject.Player && boxCollision.tagBoxCollider != Global.tagsBoxCollider[0])
						{
							GameObject objectOnCollision = boxOnWhatTag(boxCollision.boxCollider);
							if (objectOnCollision != null)
							{
								if (objectOnCollision.tagsObject.tag != "Nulla")
								{
									if (objectOnCollision.tagsObject.tag == "BloccoRandom")
									{
										if ((futureBoundingBox.Top < boxCollision.boxCollider.Bottom + maxSpeed) && (futureBoundingBox.Top >= boxCollision.boxCollider.Bottom + velocity.Y))
										{
											objectOnCollision.bloccoRandomColpito();
										}
									}
									else if (objectOnCollision.tagsObject.tag == "Muri")
									{
										if ((futureBoundingBox.Top < boxCollision.boxCollider.Bottom + maxSpeed) && (futureBoundingBox.Top >= boxCollision.boxCollider.Bottom + velocity.Y))
										{
											objectOnCollision.bloccoMuroColpito();
										}
									}
								}
							}
							if (boxCollision.tagBoxCollider.tag == "Morte")
							{
								game1.RestartLevel("Level1.jorge");
							}
							return true;
						}
						else if (game1 != null && typeObject == TypeObject.Tile && tagsObject.tag == "Moneta" && boxCollision.tagBoxCollider != Global.tagsBoxCollider[0])
                        {
							GameObject objectOnCollision = boxOnWhatTag(boxCollision.boxCollider);
							if (objectOnCollision != null)
							{
								if (objectOnCollision.previousTag.tag == "BloccoRandom" && objectOnCollision.tagsObject.tag == "Nulla")
								{
									Global.score.coins++;
									autoDestruction();
								}
							}
						}
						else
						{
							if (xAsis == true && typeObject == TypeObject.Enemy && boxCollision.tagBoxCollider.tag != "InizioLivello")
							{
								enemyXCollision = true;
								return true;
							}
							else if (typeObject != TypeObject.Enemy)
							{
								return true;
							}
						}
					}
				}

				for (int i = 1; i < objects.Count; i++)
				{
					if (objects[i] != this && objects[i].visible == true && objects[i].active == true && objects[i].collidable == true && objects[i].CheckCollision(futureBoundingBox) == true)
					{
						if (typeObject == TypeObject.Player)
						{
							if (objects[i].typeObject == TypeObject.Enemy)
							{
								if (applyGravity == true && velocity.Y >= gravity && (futureBoundingBox.Bottom > objects[i].BoundingBox.Top - maxSpeed) && (futureBoundingBox.Bottom <= objects[i].BoundingBox.Top + velocity.Y))
								{
									game1.gameHUD.baseHUD.Add(new Text("200", new Vector2(objects[i].position.X - 30 - Camera.screenRect.X, objects[i].position.Y - 75 - Camera.screenRect.Y), Global.tagsHud[5]));
									objects.RemoveAt(i);
									JumpAfterKillEnemy(Global.game.objects, Global.map);
									Global.score.score += 200;
#if DEBUG
									editor.RefreshListBox(editor.returnObjectOnList());
#endif
								}
								else
								{
									game1.RestartLevel("Level1.jorge");
								}
							}

						}
						return true;
					}
				}
			}

			return false;
		}

		private GameObject boxOnWhatTag(Rectangle boundingBox)
        {
			GameObject tag;
			Rectangle input = new Rectangle(boundingBox.Left, boundingBox.Top, 1, 1);
			List<int> lista = new List<int>();

			for (int i = 0; i < game1.objects.Count; i++)
				if (game1.objects[i].CheckCollision(input) == true)
					lista.Add(i);

			if (lista.Count < 2 && lista.Count > 0)
			{
				tag = game1.objects[lista[0]];
				return tag;
			}

			else if (lista.Count > 1)
			{
				float layerBase = 1;
				int index = -1;
				for (int i = 0; i < lista.Count; i++)
				{
					if (game1.objects[lista[i]].layerDepth < layerBase)
					{
						layerBase = game1.objects[lista[i]].layerDepth;
						index = lista[i];
					}
				}
				if (index != -1)
				{
					tag = game1.objects[index];
					return tag;
				}
			}

			return null;
        }

		public void LandResponse(Rectangle boxCollision)
		{
			position.Y = boxCollision.Top - (boundingBoxHeight + boundingBoxOffset.Y);
			velocity.Y = 0;
			powerJump = 0;
			alreeadyJumping = false;
			activeTimer = true;
		}

		public void offJumping()
        {
			jumping = false;
		}


		protected Rectangle OnGround(Map map)
		{
			Rectangle futureBoundingBox = new Rectangle((int)(position.X + boundingBoxOffset.X), (int)(position.Y + boundingBoxOffset.Y + (velocity.Y + gravity)), (int)boundingBoxWidht, (int)boundingBoxHeight);

			return map.CheckCollision(futureBoundingBox);
		}

		protected float TendToZero(float val, float amount)
		{
			if (val > 0f && (val -= amount) < 0f) return 0f;
			if (val < 0f && (val += amount) > 0f) return 0f;
			return val;
		}

	}
}
