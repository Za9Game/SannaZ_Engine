using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace SannaZ_Engine
{
    public class AnimatedObject : GameObject
    {
        protected int currentAnimationFrame;
        protected float animationTimer;
        protected int currentAnimationX = -1, currentAnimationY = -1;
        protected AnimationSet animationSet = new AnimationSet();
        protected Animation currentAnimation;

        protected bool flipRightFrames = true;
        public bool flipLeftFramse = true;
        protected SpriteEffects spriteEffects = SpriteEffects.None;

        public AnimatedObject()
        {
        }

        protected enum Animations 
        {
            Corsa, Fermo, Salto, Morto, Frenata, BloccoRandomAnimation, Camminata, Idle, Discesa, Giro
        }

        protected void LoadAnimation(string path)
        {
            AnimationData animationData;

            if (path == null)
            {
                animationSet = null;
            }
            else
            {
                animationData = AnimationLoader.Load(path);
                animationSet = animationData.animation;
                center.X = animationSet.width / 2;
                center.Y = animationSet.height / 2;

                if (animationSet.animationList.Count > 0)
                {
                    currentAnimation = animationSet.animationList[0];

                    currentAnimationFrame = 0;
                    animationTimer = 0f;
                    CalculateFramePosition();
                }
            }
        }

        public override void Update(List<GameObject> objects, Map map)
        {
            base.Update(objects, map);
            if (currentAnimation != null && animationSet != null)
            {
                boundingBoxHeight = animationSet.height * scale;
                boundingBoxWidht = animationSet.width * scale;
                UpdateAnimations();
            }
        }


        protected virtual void UpdateAnimations()
        {
            if (flipRightFrames == false && flipLeftFramse == true && (currentAnimation.name.Contains("Corsa") || currentAnimation.name.Contains("Salto") || currentAnimation.name.Contains("Fermo")))
                spriteEffects = SpriteEffects.FlipHorizontally;
            else
                spriteEffects = SpriteEffects.None;

            if (currentAnimation.animationOrder.Count < 1 || animationSet == null)
                return;

            animationTimer -= 1;

            if (animationTimer <= 0)
            {
                animationTimer = currentAnimation.speed;

                if (AnimationComplete() == true)
                { currentAnimationFrame = 0; ActionAnimationComplete(); }
                else
                    currentAnimationFrame++;

                CalculateFramePosition();
            }

        }

        public virtual void ActionAnimationComplete()
        { }



        protected virtual void ChangeAnimation(Animations newAnimation)
        {
            if (animationSet == null)
                return;
            currentAnimation = GetAnimation(newAnimation);

            if (currentAnimation == null)
                return;

            currentAnimationFrame = 0;
            animationTimer = currentAnimation.speed;

            CalculateFramePosition();
            if (flipRightFrames == false && flipLeftFramse == true && (currentAnimation.name.Contains("Corsa") || currentAnimation.name.Contains("Salto") || currentAnimation.name.Contains("Fermo")))
                spriteEffects = SpriteEffects.FlipHorizontally;
            else
                spriteEffects = SpriteEffects.None;
        }

        private Animation GetAnimation(Animations animation)
        {
            string name = Convert.ToString(animation);

            for (int i=0; i< animationSet.animationList.Count; i++)
            {
                if(animationSet.animationList[i].name == name)
                    return animationSet.animationList[i];
            }

            return null;
        }

        protected void CalculateFramePosition()
        {
            int coordinate = currentAnimation.animationOrder[currentAnimationFrame];

            currentAnimationX = (coordinate % animationSet.gridX) * animationSet.width;
            currentAnimationY = (coordinate / animationSet.gridX) * animationSet.height;
        }

        public bool AnimationComplete()
        {
            return currentAnimationFrame >= currentAnimation.animationOrder.Count -1;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (active == false || visible == false)
                return;

            if (currentAnimationX == -1 || currentAnimationY == -1)
                base.Draw(spriteBatch);

            else if (image != null && animationSet != null)
                spriteBatch.Draw(image, position, new Rectangle(currentAnimationX, currentAnimationY, animationSet.width, animationSet.height), drawColor, rotation, Vector2.Zero, scale, spriteEffects, layerDepth);
            
            else
                base.Draw(spriteBatch);
        }

        //protected string GetAnimationName(Animations animation)
        //{
        //    return AddSpacesToSentence(animation.ToString(), false);
        //}

        protected bool AnimationIsNot(Animations input)
        {
            return currentAnimation != null && Convert.ToString(input) != currentAnimation.name;
        }

        //public string AddSpacesToSentence(string text, bool preserveAcronyms)
        //{
        //    if (string.IsNullOrWhiteSpace(text))
        //        return string.Empty;
        //    StringBuilder newText = new StringBuilder(text.Length * 2);
        //    newText.Append(text[0]);
        //    for (int i = 1; i < text.Length; i++)
        //    {
        //        if (char.IsUpper(text[i]))
        //            if ((text[i - 1] != ' ' && !char.IsUpper(text[i - 1])) ||
        //                (preserveAcronyms && char.IsUpper(text[i - 1]) &&
        //                 i < text.Length - 1 && !char.IsUpper(text[i + 1])))
        //                newText.Append(' ');
        //        newText.Append(text[i]);
        //    }
        //    return newText.ToString();
        //}


    }
}
