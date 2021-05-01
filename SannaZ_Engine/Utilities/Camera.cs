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

namespace SannaZ_Engine
{
    public static class Camera
    {
        static private Matrix transformMatrix;
        static private Vector2 position;
        static public float rotation;
        static private float zoom;
        static public Rectangle screenRect;
       
        static public bool updateYAxis = true;
        static public bool updateXAxis = true;
        static public bool primaUpdate = false;


        public static void Initialize()
        {
            zoom = 0.8f;
            rotation = 0.0f;

            position = new Vector2(Resolution.VirtualWidth / 2, Resolution.VirtualHeight / 2);
        }
        
        public static Rectangle ScreenRect 
        {
            get { return screenRect; }
        }

        public static float Zoom
        {
            get { return zoom; }
            set
            {
                zoom = value; if (zoom < 0.1f) zoom = 0.1f;
            }
        }

        public static void Update(Vector2 follow)
        {
            UpdateMovement(follow);
            CalculateMatrixAndRectangle();
        }

        private static void UpdateMovement(Vector2 follow)
        {
#if !DEBUG
            if (Global.levelName != "Menu.jorge")
            {
                if (primaUpdate == true)
                    if (updateXAxis == true && follow.X - position.X > 0)
                        position.X += follow.X - position.X;
                    else if (primaUpdate == false)
                        if (updateXAxis == true)
                            position.X += follow.X - position.X;

                if (primaUpdate == false)
                    primaUpdate = true;

                if (updateYAxis == true)
                {
                    position.Y += follow.Y - position.Y - 282;
                    updateYAxis = false;
                }
            }
            else
            {
                if (updateXAxis == true)
                    position.X += follow.X - position.X;

                if (updateYAxis == true)
                {
                    position.Y += follow.Y - position.Y - 282;
                    updateYAxis = false;
                }
            }
#endif
#if DEBUG
            if (updateXAxis == true)
                position.X += follow.X - position.X;

            if (updateYAxis == true)
            {
                position.Y += follow.Y - position.Y - 282;
                updateYAxis = false;
            }
#endif
        }
        
        public static void LookAt(Vector2 lookAt)
        {
            if (updateXAxis == true)
                position.X = lookAt.X;
            position.Y = lookAt.Y;
        }

        private static void CalculateMatrixAndRectangle()
        {
            transformMatrix = Matrix.CreateTranslation(new Vector3(-position, 0)) * Matrix.CreateRotationZ(rotation) *
                        Matrix.CreateScale(new Vector3(zoom, zoom, 1)) * Matrix.CreateTranslation(new Vector3(Resolution.VirtualWidth
                            * 0.5f, Resolution.VirtualHeight * 0.5f, 0));

            transformMatrix = transformMatrix * Resolution.getTransformationMatrix();

             transformMatrix.M41 = (float)Math.Round(transformMatrix.M41, 0);
            transformMatrix.M42 = (float)Math.Round(transformMatrix.M42, 0);

            screenRect = VisibleArea();
        }

        private static Rectangle VisibleArea()
        {
            Matrix inverseViewMatrix = Matrix.Invert(transformMatrix);
            Vector2 tl = Vector2.Transform(Vector2.Zero, inverseViewMatrix);
            Vector2 tr = Vector2.Transform(new Vector2(Resolution.VirtualWidth, 0), inverseViewMatrix);
            Vector2 bl = Vector2.Transform(new Vector2(0, Resolution.VirtualHeight), inverseViewMatrix);
            Vector2 br = Vector2.Transform(new Vector2(Resolution.VirtualWidth, Resolution.VirtualHeight), inverseViewMatrix);
            Vector2 min = new Vector2(
                MathHelper.Min(tl.X, MathHelper.Min(tr.X, MathHelper.Min(bl.X, br.X))),
                MathHelper.Min(tl.Y, MathHelper.Min(tr.Y, MathHelper.Min(bl.Y, br.Y))));
            Vector2 max = new Vector2(
                MathHelper.Max(tl.X, MathHelper.Max(tr.X, MathHelper.Max(bl.X, br.X))),
                MathHelper.Max(tl.Y, MathHelper.Max(tr.Y, MathHelper.Max(bl.Y, br.Y))));
            return new Rectangle((int)min.X, (int)min.Y, (int)(Resolution.VirtualWidth / zoom), (int)(Resolution.VirtualHeight / zoom));
        }

        public static Matrix GetTransformMatrix()
        {
            return transformMatrix;
        }
    }
}
