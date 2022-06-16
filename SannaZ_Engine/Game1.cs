﻿using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;
using System.Diagnostics;
using SDL2;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Data.Common;
using System.Threading;

namespace SannaZ_Engine
{
#if DEBUG
    public partial class Game1 : MonoGame.Forms.Controls.MonoGameControl
    {

        #region DLL Functions
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]

        static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        #endregion

        //GraphicsDeviceManager graphics;
        //GraphicsDevice graphicsDevice;
        //SpriteBatch spriteBatch;

        public List<GameObject> objects = new List<GameObject>();
        public List<Light> lights = new List<Light>();
        public Map map = new Map();
        public Score score = new Score();

        public GameHUD gameHUD = new GameHUD();
        public Editor editor;

        //RenderTarget2D lightsTarget;
        //RenderTarget2D mainTarget;
        //Effect effect;

        private float lastCameraPositionX = -1;

        public string nummero="";

        /*
        public Game1()
        {
            //graphics = new GraphicsDeviceManager(this);
            //graphics.SynchronizeWithVerticalRetrace = true;
            //IsFixedTimeStep = true;


            //Window.AllowUserResizing = true;
            //Window.ClientSizeChanged += new System.EventHandler<EventArgs>(ChangeResolution);
            //Window.Title = "SannaZ_Engine";
            //graphics.IsFullScreen = false;
           // graphics.ApplyChanges();

            //Window.BeginScreenDeviceChange(true);

            //graphics.GraphicsProfile = GraphicsProfile.HiDef;
            //GraphicsProfile gp = graphics.GraphicsProfile;
            //PresentationParameters pp = new PresentationParameters();
            //graphicsDevice = new GraphicsDevice(graphics.GraphicsDevice.Adapter, gp, pp);
        }*/

        #region CopyContent

        public void inputCopy()
        {
            string sourceDirectory = @"E:\PROGRAMMARE\GameEngine\SannaZ_Engine Versione Unita\SannaZ_Engine\SannaZ_Engine\bin\DesktopGL\AnyCPU\Debug\Content";
            string targetDirectory = @"E:\PROGRAMMARE\GameEngine\SannaZ_Engine Versione Unita\SannaZ_Engine\SannaZ_Engine\bin\DesktopGL\AnyCPU\Release\Content";
            Copy(sourceDirectory, targetDirectory);
        }

        public static void Copy(string sourceDirectory, string targetDirectory)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                try
                {
                    fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
                }
                catch {  }
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

        #endregion

        private void ChangeResolution(object sender, EventArgs e)
        {
            //Resolution.SetVirtualResolution(1300, 700);
            //graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
            //graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;

           // var pp = GraphicsDevice.PresentationParameters;
            //lightsTarget = new RenderTarget2D(GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight);
           // mainTarget = new RenderTarget2D(GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight);
        }

        protected override void Initialize()
        {
            //COSTRUTTORE PARTE
            /*
            Resolution.Init(ref graphics);
            Resolution.SetVirtualResolution(1300, 700);
            Resolution.SetResolution(1300, 700, false);
#if DEBUG
            Resolution.SetResolution(900, 700, false);

            Thread copyThread = new Thread(inputCopy);
            copyThread.Start();

#endif
            */
            /*
            //ok tutto sto bordello qua con i dll prima serve per poter posizionare l'app all'inizio
            const short SWP_NOZORDER = 0X4;
            const int SWP_SHOWWINDOW = 0x0040;
            const short SWP_NOSIZE = 1;

            SDL.SDL_SysWMinfo info = new SDL.SDL_SysWMinfo();
            SDL.SDL_GetWindowWMInfo(this.Window.Handle, ref info);
            IntPtr gameWinHandle = info.info.win.window;

            Process[] processes = Process.GetProcessesByName("SannaZ_Engine");
            foreach (var process in processes)
            {
                RECT gameWindow = new RECT();
                GetWindowRect(gameWinHandle, ref gameWindow);

                if (gameWinHandle != IntPtr.Zero)
                {
                    SetWindowPos(gameWinHandle, 0, 0, 80, gameWindow.Right, gameWindow.Top, SWP_NOZORDER | SWP_NOSIZE | SWP_SHOWWINDOW);
                }
            }*/

            base.Initialize();
            Editor.Content.RootDirectory = "Content";

            Camera.Initialize();
            map.Initialize();
            score.Initialize();
            Global.Initialize(this, map, score);
            gameHUD.Initialize();


            //LOAD CONTENT ZONE

            Editor.spriteBatch = new SpriteBatch(GraphicsDevice);
#if DEBUG
            editor.LoadTextures(Editor.Content);
#endif
            score.Initialize();
            LoadLevel("Menu.jorge");
            Camera.Initialize();
            Camera.Update(new Vector2(0, 0));

            map.Load(Editor.Content);
            gameHUD.Load(Editor.Content);

            //effect = Editor.Content.Load<Effect>("Light\\Effect1");

            var pp = GraphicsDevice.PresentationParameters;
            //lightsTarget = new RenderTarget2D(GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight);
            //mainTarget = new RenderTarget2D(GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight);
        }

        public void RestartLevel(string LevelName)
        {
            score.Initialize();
            LoadLevel(LevelName);
            Camera.Initialize();
            Camera.updateYAxis = true;
            if (LevelName != "Menu.jorge")
            {
                Camera.Update(objects[0].position);
            }
            else
            {
                //IsMouseVisible = true;
                Camera.Update(new Vector2(0, 0));
            }
            UpdateCamera();
        }

        float everySeconds = 1f;
        float currentTime = 0f;
        protected override void Update(GameTime gameTime)
        {
            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (currentTime >= everySeconds)
            {
                score.time--;
                currentTime -= everySeconds;
                if(score.time <= 0)
                    RestartLevel("Level1.jorge");
            }

            Input.Update();
            if (Global.levelName != "Menu.jorge")
            {
                if (lastCameraPositionX == -1)
                    lastCameraPositionX = Camera.screenRect.X;
                if (lastCameraPositionX != Camera.screenRect.X)
                {
                    ObjectOutVisible();
                    lastCameraPositionX = Camera.screenRect.X;
                }
            }
            if(Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.P))
            {
                RestartLevel("Menu.jorge");
            }
            
            UpdateObjects();
            UpdateLights();
            UpdateCamera();
            map.Update();
            gameHUD.Update();
#if DEBUG   
            editor.Update(objects, map);
#endif
            base.Update(gameTime);
        }
        
        protected override void Draw()
        {
            //GraphicsDevice.SetRenderTarget(lightsTarget);
           // GraphicsDevice.Clear(Color.Black);
            Editor.spriteBatch = new SpriteBatch(GraphicsDevice);
           // Editor.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Camera.GetTransformMatrix());
           // DrawLights();
           // Editor.spriteBatch.End();

            //GraphicsDevice.SetRenderTarget(mainTarget);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //Resolution.BeginDraw();
            Editor.spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null);
#if DEBUG
            editor.Draw(Editor.spriteBatch);
#endif
            DrawObjects();
            map.DrawBoxesCollider(Editor.spriteBatch);
            gameHUD.Draw(Editor.spriteBatch, Editor.Content);
            Editor.spriteBatch.End();

          //  GraphicsDevice.SetRenderTarget(null);
          //  GraphicsDevice.Clear(Color.CornflowerBlue);
          //  Editor.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

           // effect.Parameters["lightMask"].SetValue(lightsTarget);
           // effect.CurrentTechnique.Passes[0].Apply();
           // Editor.spriteBatch.Draw(mainTarget, Vector2.Zero, Color.White);
           // Editor.spriteBatch.End();
            
            base.Draw();
        }

        public void LoadLevel(string fileName)
        {
            Global.levelName = fileName;

         //   if (Global.levelName == "Menu.jorge")
               // IsMouseVisible = true;
            
            LevelData levelData = XmlHelper.Load("Content\\Levels\\" + fileName);

            objects = levelData.objects;
            gameHUD.baseHUD = levelData.baseHud;
            map.boxesCollider = levelData.boxesCollider;
            lights = levelData.ligths;
            map.mapWidth = levelData.mapWidth;
            map.mapHeight = levelData.mapHeight;

            if (levelData.tagsObject.Count == 0)
            {
                Global.tagsObject.Add(new TagObject { key = Global.tagsObject.Count, tag = "Nulla" });
                Global.tagsObject.Add(new TagObject { key = Global.tagsObject.Count, tag = "BloccoRandom" });
                Global.tagsObject.Add(new TagObject { key = Global.tagsObject.Count, tag = "Pali" });
                Global.tagsObject.Add(new TagObject { key = Global.tagsObject.Count, tag = "Muri" });
            }
            else
                Global.tagsObject = levelData.tagsObject;

            if (levelData.tagsHud.Count == 0)
            {
                Global.tagsHud.Add(new TagObject { key = Global.tagsHud.Count, tag = "Nulla" });
                Global.tagsHud.Add(new TagObject { key = Global.tagsHud.Count, tag = "Score" });
            }
            else
                Global.tagsHud = levelData.tagsHud; 
            
            if (levelData.tagsBoxCollider.Count == 0)
            {
                Global.tagsBoxCollider.Add(new TagObject { key = Global.tagsBoxCollider.Count, tag = "Nulla" });
                Global.tagsBoxCollider.Add(new TagObject { key = Global.tagsBoxCollider.Count, tag = "BloccoRandom" });
                Global.tagsBoxCollider.Add(new TagObject { key = Global.tagsBoxCollider.Count, tag = "Muro" });
                Global.tagsBoxCollider.Add(new TagObject { key = Global.tagsBoxCollider.Count, tag = "InizioLivello" });
                Global.tagsBoxCollider.Add(new TagObject { key = Global.tagsBoxCollider.Count, tag = "Morte" });
            }
            else
                Global.tagsBoxCollider = levelData.tagsBoxCollider;

            LoadObject();
            LoadOLight();
        }

        public void LoadObject()
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].Initialize();
                objects[i].Load(Editor.Content);
                Editor.Content.RootDirectory = "Content";
            }
        }
        public void LoadOLight()
        {
            for (int i = 0; i < lights.Count; i++)
            {
                lights[i].Initialize();
                lights[i].Load(Editor.Content);
            }
        }

        public void UpdateObjects()
        {
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i].visible == true)
                    objects[i].Update(objects, map);
            }
        }
        public void UpdateLights()
        {
            for (int i = 0; i < lights.Count; i++)
            {
                lights[i].Update(objects, map);
            }
        }

        public void DrawObjects()
        {
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i].visible == true)
                    objects[i].Draw(Editor.spriteBatch);
            }
        }
        public void DrawLights()
        {
            for (int i = 0; i < lights.Count; i++)
            {
                lights[i].Draw(Editor.spriteBatch);
            }
        }

        private void UpdateCamera()
        {
            if (objects.Count == 0)
                return;
            if (Global.levelName == "Menu.jorge")
            {
                Camera.Update(new Vector2(0, 0));
                return;
            }
#if DEBUG
            if(editor.paused.Checked == false)
                Camera.Update(new Vector2(objects[0].position.X, objects[0].position.Y));
#endif
#if !DEBUG
            Camera.Update(new Vector2(objects[0].position.X, objects[0].position.Y));
#endif
        }

        private void ObjectOutVisible()
        {
            List<GameObject> oggettiDisattivare = new List<GameObject>();
            oggettiDisattivare = objects.FindAll(delegate (GameObject gameObject) {
                return gameObject.typeObject != GameObject.TypeObject.Enemy && (gameObject.BoundingBox.Right < Camera.screenRect.Left - 325 || gameObject.position.X > Camera.screenRect.Right + 25);
            });
            
            List<GameObject> oggettiAttivare = new List<GameObject>();
            oggettiAttivare = objects.FindAll(delegate (GameObject gameObject) {
                return gameObject.BoundingBox.Right >= Camera.screenRect.Left- 325 && gameObject.position.X <= Camera.screenRect.Right+25;
            });

            oggettiDisattivare.ForEach(DisableObject);
            oggettiAttivare.ForEach(ActiveObject);
        }

        private void DisableObject(GameObject gameObject)
        {
            gameObject.visible = false;
        }
        private void ActiveObject(GameObject gameObject)
        {
            gameObject.visible = true;
        }

        public void DestroyObject<T>(T toDestory, int mode)//0 objects, 1 gameHud
        {
            if (mode == 0)
            {
                for (int i = 0; i < objects.Count; i++)
                {
                    if (objects[i].DESTRUCTION_)
                        objects.RemoveAt(i);
                }
            }
            if (mode == 1)
            {
                for (int i = 0; i < gameHUD.baseHUD.Count; i++)
                {
                    if (gameHUD.baseHUD[i].DESTRUCTION_)
                        gameHUD.baseHUD.RemoveAt(i);
                }
            }
        }

    }
#endif
}
