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
    public class Enemy : ScriptCharacter
    {
        private bool enemyDirection = true;
        private Vector2 startPostion;

        public Enemy()
        {
        }
        
        public Enemy(Vector2 inputPosition)
        {
            position = inputPosition;
        }

        public override void Initialize()
        {
            active = true;
            collidable = true;
            layerDepth = 0.3f;
            typeObject = TypeObject.Enemy;
#if DEBUG
            blocca = true;
#endif
#if !DEBUG
            blocca = false;
#endif
            if (startPostion != new Vector2(0, 0))
                position = startPostion;
            base.Initialize();
        }

        public override void Load(ContentManager content)
        {
            spriteName = "EnemyNanoSheet.png";

            string nulla = "";
            if (spriteName!=null)
                image = TextureLoader.Load(spriteName, content, ref nulla);
            if (nulla != "")
            {
                spritePath = nulla;
            }
            animationName = "EnemyAnimation.anm";
            if (animationName != null)
                LoadAnimation(animationName);
            ChangeAnimation(Animations.Camminata);
            //explosion = content.Load<SoundEffect>("Audio\\explosion");

            base.Load(content);
        }

        public override void Update(List<GameObject> objects, Map map)
        {
            if (!blocca)
            {
                if (enemyXCollision)
                {
                    enemyXCollision = false;
                    if (enemyDirection)
                        enemyDirection = false;
                    else
                        enemyDirection = true;
                }
                EnemyMove(enemyDirection);
            }
            else
            {
                startPosition = position;
                EnemyStop();
            }
            base.Update(objects, map);
        }

    }
}
