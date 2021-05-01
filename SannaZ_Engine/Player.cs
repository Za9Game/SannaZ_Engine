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
	public class Player : ScriptCharacter
	{
		public static int score;
		private bool alreadyJumping;

		public Player()
		{
		}

		public Player(Vector2 inputPosition)
		{
			position = inputPosition;
		}

		public override void Initialize()
		{
			alreadyJumping = false;
			scale = 0.3f;
			score = 0;
			layerDepth = 0;
			typeObject = TypeObject.Player;
			applyGravity = true;
			base.Initialize();
		}

		public override void Load(ContentManager content)
		{
			spriteName = "MarioNormale";

			string nulla = "";
			if (spriteName != null)
				image = TextureLoader.Load(spriteName, content, ref nulla);

			animationName = "MarioAnimation.anm";
			if (animationName != null)
				LoadAnimation(animationName);
			ChangeAnimation(Animations.Fermo);

			base.Load(content);

			boundingBoxOffset.X = 0; boundingBoxOffset.Y = 0; boundingBoxWidht = animationSet.width; boundingBoxHeight = animationSet.height;
		}



		public override void Update(List<GameObject> objects, Map map)
		{
			CheckInput(objects, map);
			base.Update(objects, map);
		}

		private void CheckInput(List<GameObject> objects, Map map)
		{
			if (applyGravity == false)
			{
				if (Input.IsKeyDown(Keys.D))
					MoveRight();
				else if (Input.IsKeyDown(Keys.A))
					MoveLeft();

				if (Input.IsKeyDown(Keys.S))
					MoveDown();
				else if (Input.IsKeyDown(Keys.W))
					MoveUp();
			}
			else
			{
				if (Input.IsKeyDown(Keys.D) && Input.IsKeyUp(Keys.A))
				{
					flipLeftFramse = false;
					flipRightFrames = true;
					MoveRight();
				}
				else if (Input.IsKeyDown(Keys.A) && Input.IsKeyUp(Keys.D))
				{
					flipLeftFramse = true;
					flipRightFrames = false;
					MoveLeft();
				}
				if (Input.IsKeyUp(Keys.W))
				{
					alreadyJumping = true;
				}
				if(!alreeadyJumping)
                {
					alreadyJumping = false;
                }
				if (Input.IsKeyDown(Keys.W) && !alreadyJumping)
				{
					Jump(objects, map);
				}
			}
		}

        protected override void UpdateAnimations()
        {
            if (currentAnimation == null)
                return;

            base.UpdateAnimations();
			if(jumping)
            {
				if (AnimationIsNot(Animations.Salto))
					ChangeAnimation(Animations.Salto);
            }
            if (velocity.X != 0 && !jumping)
            {
				if(AnimationIsNot(Animations.Corsa))
					ChangeAnimation(Animations.Corsa);
            }
            else if(!jumping)
            {
				if (AnimationIsNot(Animations.Fermo))
					ChangeAnimation(Animations.Fermo);
            }
            //else if (velocity == Vector2.Zero && jumping == false)
            //         {
            //	if (direction.X < 0 && AnimationIsNot(Animations.IdleSinistra))
            //		ChangeAnimation(Animations.IdleSinistra);
            //	else if (direction.X > 0 && AnimationIsNot(Animations.IdleDestra))
            //		ChangeAnimation(Animations.IdleDestra);
            //	else if (direction.Y < 0 && AnimationIsNot(Animations.IdleDietro))
            //		ChangeAnimation(Animations.IdleDietro);
            //	else if (direction.Y > 0 && AnimationIsNot(Animations.IdleDavanti))
            //		ChangeAnimation(Animations.IdleDavanti);

            //}
        }

        public override void LoadAnimation()
        {
			ChangeAnimation(Animations.Fermo);

			base.LoadAnimation();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }


    }
}
