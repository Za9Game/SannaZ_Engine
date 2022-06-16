using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SDL2;
using Color = Microsoft.Xna.Framework.Color;
using System.Security.AccessControl;
using System.Globalization;
using System.Collections;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;

namespace SannaZ_Engine
{
    public partial class Editor : Form
    {
        public Game1 game;
        IntPtr gameWinHandle; 

        public enum CreateMode { None, BoxesCollider, Objects, Light, HUD };
        public CreateMode mode = CreateMode.None; //che stiamo creando
        public bool placingItem = false; //se sis tanno metendo oggeti nella mappa
        private int indexLastObject;
        private bool TagDropDownActive = false;

        Texture2D grid, pixel;
        Vector2 cameraPosition; //muovere la camera durante la pausa

        private GameObject.TypeObject typeObjectSelected;
        private BaseHUD.TypeHUD typeHUDSelected;

        string savePath = ""; //per salvare file

        string contentFullPath = "E:\\PROGRAMMARE\\GameEngine\\SannaZ_Engine\\SannaZ_Engine\\SannaZ_Engine\\bin\\DesktopGL\\AnyCPU\\Debug\\";
        enum ObjectType
        {
            Enemy, Player, Tile, NumOfObjects,
        }; 
        enum HUDType
        {
            Text, Button, NumOfObjects,
        };

        enum ObjectProperty
        { 
            Sprite, Animation, NumOfObjects
        };

        const string objectsNamespace = "SannaZ_Engine.";

        float timeForRepeat = 0.1f;
        float timeForRepeat2 = 0.1f;
        bool timerActive = false;
        bool timerActive2 = false;
        int lastIndexList = -1;


        public Editor(Game1 inputGame)
        {
            Thread newThread = new Thread(InitializeComponent);
            newThread.Start();
            newThread.Join();

            game = inputGame;

            game.IsMouseVisible = true;

            // con questo possiamo fare quello che ci pare con la finestra di gioco
            SDL.SDL_SysWMinfo info = new SDL.SDL_SysWMinfo();
            SDL.SDL_GetWindowWMInfo(game.Window.Handle, ref info);
            gameWinHandle = info.info.win.window;
            
            //posizione dell'editor
            RECT gameWindow = new RECT();
            GetWindowRect(gameWinHandle, ref gameWindow);
            Location = new System.Drawing.Point(gameWindow.Right-15, gameWindow.Top);

            //aggiungere i tipi alle liste
            PopulateObjectList();
            PopulateHudList();
            PopulatePropertyList();

            //setta i valori di width e height della mappa
            mapHeight.Value = game.map.mapHeight;
            mapWidth.Value = game.map.mapWidth;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            AddButtonClick(0);
        }

        private void AddButtonClick(int j)
        {
            if (objectTypes.SelectedIndex == -1)
                return;

            if (mode == CreateMode.Objects)
            {
                List<GameObject> objectsOnList = returnObjectOnList();
                ObjectType selectedObject = (ObjectType)objectTypes.Items[objectTypes.SelectedIndex];

                Type type = Type.GetType(objectsNamespace + selectedObject.ToString());
                GameObject newObject;

                if (type.Name == "Tile" && listBox.Items.Count > 0)
                {
                    newObject = (GameObject)Activator.CreateInstance(type);
                    newObject.spriteName = objectsOnList[listBox.SelectedIndex].spriteName;
                    newObject.scale = objectsOnList[listBox.SelectedIndex].scale;
                    newObject.LoadSprite(game.Content);
                    newObject.layerDepth = objectsOnList[listBox.SelectedIndex].layerDepth;
                }
                else
                {
                    newObject = (GameObject)Activator.CreateInstance(type);
                }

                if (newObject == null)
                    return;

                if (j == 0 && typeObjectSelected == GameObject.TypeObject.Tile)
                {
                    OpenFileDialog openFileDialog1 = new OpenFileDialog
                    {
                        Filter = "XNB (.xnb)|*.xnb",
                        FilterIndex = 2,
                        Multiselect = false
                    };

                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            string fileDirectory = game.Content.RootDirectory;
                            newObject.spriteName = openFileDialog1.SafeFileName;
                            fileDirectory = openFileDialog1.FileName.Remove(openFileDialog1.FileName.Length - openFileDialog1.SafeFileName.Length, openFileDialog1.SafeFileName.Length);
                            game.Content.RootDirectory = fileDirectory.Remove(0, contentFullPath.Length);
                            newObject.LoadSprite(game.Content);
                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show("Error Loading Image: " + exception.Message);
                        }
                    }
                }

                newObject.Load(game.Content);
                newObject.typeObject = (GameObject.TypeObject)selectedObject;
                game.objects.Add(newObject);

                placingItem = true;

                SetListBox(returnObjectOnList(), false);
            }

            else if (mode == CreateMode.Light)
            {
                Light newObject = new Light(new Vector2(0, 0));

                newObject.Load(game.Content);
                game.lights.Add(newObject);

                placingItem = true;

                SetListBox(game.lights, false);
            }

            else if (mode == CreateMode.HUD)
            {
                HUDType selectedObject = (HUDType)hudTypes.Items[hudTypes.SelectedIndex];

                Type type = Type.GetType(objectsNamespace + selectedObject.ToString());
                BaseHUD newObject;

                newObject = (BaseHUD)Activator.CreateInstance(type);
                newObject.instanceTexture();
                newObject.Load(game.Content);
                newObject.typeHUD = (BaseHUD.TypeHUD)selectedObject;
                game.gameHUD.baseHUD.Add(newObject);

                placingItem = true;

                SetListBox(returnHudOnList(), false);
            }
        }

        public void LoadTextures(ContentManager content)
        {
            string nulla="";
            grid = TextureLoader.Load("128grid", content, ref nulla);
            pixel = TextureLoader.Load("pixel", content, ref nulla);
        }

        public void Update(List<GameObject> objects, Map map)
        {
            List<GameObject> objectsOnList = returnObjectOnList();

            if (lastIndexList == -1)
                lastIndexList = listBox.SelectedIndex;
            bool changedIndex = false;
            if (lastIndexList != listBox.SelectedIndex)
            {
                lastIndexList = listBox.SelectedIndex;
                changedIndex = true;
            }

            if (timerActive == true)
            {
                timeForRepeat += 0.1f;
                if (timeForRepeat > 1.5f)
                { timeForRepeat = 0.1f; timerActive = false; }
            }
            if (timerActive2 == true)
            {
                timeForRepeat2 += 0.1f;
                if (timeForRepeat2 > 1f)
                { timeForRepeat2 = 0.1f; timerActive2 = false; }
            }

            if (Input.MouseLeftDown() == true && GameWindowFocused() == true)
            {
                Vector2 mousePosition = Input.MousePositionCamera();
                Point desiredIndex = map.GetTileIndex(mousePosition);
                if (mode == CreateMode.BoxesCollider)
                {
                    #region Add BoxesCollider
                    if (desiredIndex.X >= 0 && desiredIndex.X < map.mapWidth && desiredIndex.Y >= 0 && desiredIndex.Y < map.mapHeight)
                    {
                        Rectangle newBoxCollider = new Rectangle(desiredIndex.X * map.tileSize, desiredIndex.Y * map.tileSize, map.tileSize, map.tileSize);

                        if (map.CheckCollision(newBoxCollider) == Rectangle.Empty)
                        {
                            Rectangle oldBoxCollider = Rectangle.Empty;

                            for (int i = 0; i < map.boxesCollider.Count; i++)
                            {
                                oldBoxCollider = map.boxesCollider[i].boxCollider;

                                if (map.boxesCollider[i].boxCollider.Intersects(new Rectangle(newBoxCollider.X + map.tileSize, newBoxCollider.Y, newBoxCollider.Width, newBoxCollider.Height))
                                    && map.boxesCollider[i].boxCollider.Y == newBoxCollider.Y && map.boxesCollider[i].boxCollider.Height == newBoxCollider.Height)
                                {
                                    newBoxCollider = new Rectangle(oldBoxCollider.X - map.tileSize, oldBoxCollider.Y, oldBoxCollider.Width + map.tileSize, oldBoxCollider.Height);
                                    map.boxesCollider[i].boxCollider = newBoxCollider;
                                    break;
                                }
                                else if (map.boxesCollider[i].boxCollider.Intersects(new Rectangle(newBoxCollider.X - map.tileSize, newBoxCollider.Y, newBoxCollider.Width, newBoxCollider.Height))
                                    && map.boxesCollider[i].boxCollider.Y == newBoxCollider.Y && map.boxesCollider[i].boxCollider.Height == newBoxCollider.Height)
                                {
                                    newBoxCollider = new Rectangle(oldBoxCollider.X, oldBoxCollider.Y, oldBoxCollider.Width + map.tileSize, oldBoxCollider.Height);
                                    map.boxesCollider[i].boxCollider = newBoxCollider;
                                    break;
                                }
                                if (map.boxesCollider[i].boxCollider.Intersects(new Rectangle(newBoxCollider.X, newBoxCollider.Y + map.tileSize, newBoxCollider.Width, newBoxCollider.Height))
                                    && map.boxesCollider[i].boxCollider.X == newBoxCollider.X && map.boxesCollider[i].boxCollider.Width == newBoxCollider.Width)
                                {
                                    newBoxCollider = new Rectangle(oldBoxCollider.X, oldBoxCollider.Y - map.tileSize, oldBoxCollider.Width, oldBoxCollider.Height + map.tileSize);
                                    map.boxesCollider[i].boxCollider = newBoxCollider;
                                    break;
                                }
                                else if (map.boxesCollider[i].boxCollider.Intersects(new Rectangle(newBoxCollider.X, newBoxCollider.Y - map.tileSize, newBoxCollider.Width, newBoxCollider.Height))
                                    && map.boxesCollider[i].boxCollider.X == newBoxCollider.X && map.boxesCollider[i].boxCollider.Width == newBoxCollider.Width)
                                {
                                    newBoxCollider = new Rectangle(oldBoxCollider.X, oldBoxCollider.Y, oldBoxCollider.Width, oldBoxCollider.Height + map.tileSize);
                                    map.boxesCollider[i].boxCollider = newBoxCollider;
                                    break;
                                }

                                oldBoxCollider = Rectangle.Empty;
                            }

                            if (oldBoxCollider == Rectangle.Empty)
                                map.boxesCollider.Add(new BoxCollider(newBoxCollider));

                            SetListBox(map.boxesCollider, false);
                        }
                        else
                        {
                            for (int i = 0; i < map.boxesCollider.Count; i++)
                            {
                                if (map.boxesCollider[i].boxCollider.Intersects(newBoxCollider))
                                {
                                    listBox.SelectedIndex = i;
                                    break;
                                }
                            }
                        }
                    }
                    #endregion
                }
                else if (mode == CreateMode.Objects)
                {
                    if (placingItem == true)
                    {
                        timerActive = true;
                        placingItem = false;
                        game.objects[game.objects.Count - 1].startPosition = mousePosition;
                        game.objects[game.objects.Count - 1].position = new Vector2(desiredIndex.X * map.tileSize, desiredIndex.Y * map.tileSize);
                        game.objects[game.objects.Count - 1].Initialize();
                        SetListBox(returnObjectOnList(), false);
                    }
                    else if (typeObjectSelected == GameObject.TypeObject.Tile && timerActive == false)
                    {
                        if (listBox.Items.Count >= 1)
                        {
                            AddButtonClick(1);
                        }
                    }
                }
                else if (mode == CreateMode.Light)
                {
                    if (placingItem == true)
                    {
                        placingItem = false;
                        game.lights[game.lights.Count - 1].startPosition = mousePosition;
                        game.lights[game.lights.Count - 1].position = new Vector2(desiredIndex.X * map.tileSize, desiredIndex.Y * map.tileSize);
                        game.lights[game.lights.Count - 1].Initialize();
                        SetListBox(game.lights, false);
                    }
                }
                else if (mode == CreateMode.HUD)
                {
                    if (placingItem == true)
                    {
                        placingItem = false;
                        game.gameHUD.baseHUD[game.gameHUD.baseHUD.Count - 1].startPosition = mousePosition;
                        if (typeHUDSelected == BaseHUD.TypeHUD.Text)
                            game.gameHUD.baseHUD[game.gameHUD.baseHUD.Count - 1].position = new Vector2(Input.MousePositionCamera().X - Camera.screenRect.X, Input.MousePositionCamera().Y - Camera.screenRect.Y);
                        else
                            game.gameHUD.baseHUD[game.gameHUD.baseHUD.Count - 1].position = new Vector2(desiredIndex.X * map.tileSize, desiredIndex.Y * map.tileSize);
                        game.gameHUD.baseHUD[game.gameHUD.baseHUD.Count - 1].Initialize();
                        SetListBox(returnHudOnList(), false);
                    }
                }
            }

            else if (Input.MouseRightDown() == true && GameWindowFocused() == true)
            {
                if (mode == CreateMode.BoxesCollider)
                {
                    Vector2 mousePosition = Input.MousePositionCamera();
                    Rectangle input = new Rectangle((int)mousePosition.X, (int)mousePosition.Y, 1, 1);
                    for (int i = 0; i < game.map.boxesCollider.Count; i++)
                    {
                        if (game.map.boxesCollider[i].boxCollider.Intersects(input) == true)
                        {
                            RemoveBoxCollider(i);
                            break;
                        }
                    }
                }
                if (mode == CreateMode.Objects && typeObjectSelected == GameObject.TypeObject.Tile && !timerActive2)
                {
                    timerActive2 = true;
                    Vector2 mousePosition = Input.MousePositionCamera();
                    Rectangle input = new Rectangle((int)mousePosition.X, (int)mousePosition.Y, 1, 1);
                    List<int> lista = new List<int>();

                    //dunque qui guardo che indici di objects ci sono nell punto cliccato
                    for (int i = 0; i < game.objects.Count; i++)
                        if (game.objects[i].CheckCollision(input) == true)
                            lista.Add(i);

                    //sse ce ne solo uno tolgo quello ed è apposto
                    if (lista.Count < 2 && lista.Count > 0)
                    {
                        game.objects.RemoveAt(lista[0]);
                        RefreshListBox(objectsOnList);
                    }

                    //senno faccio sto bordelloxD e tolgo quello che è piu diciamo verso l'esterno quindi quello più visibile
                    else if (lista.Count > 1)
                    {
                        float layerBase = 1;
                        int index = -1;
                        for (int i = 0; i < lista.Count; i++)
                        {
                            if (game.objects[lista[i]].layerDepth < layerBase)
                            {
                                layerBase = game.objects[lista[i]].layerDepth;
                                index = lista[i];
                            }
                        }
                        if (index != -1)
                            game.objects.RemoveAt(index);
                        RefreshListBox(objectsOnList);
                    }
                }
            }

            else if (Input.MouseCenterDown() == true && GameWindowFocused() == true)
            {
                //questa è simile al coso del remove solo che qua seleziono l'oggetto
                if (mode == CreateMode.Objects && typeObjectSelected == GameObject.TypeObject.Tile)
                {
                    if(game.objects.Count == 1)
                    { listBox.SelectedIndex = 1; return; }

                    Vector2 mousePosition = Input.MousePositionCamera();
                    Rectangle input = new Rectangle((int)mousePosition.X, (int)mousePosition.Y, 1, 1);
                    List<int> lista = new List<int>();

                    for (int i = 0; i < objectsOnList.Count; i++)
                        if (objectsOnList[i].CheckCollision(input) == true)
                            lista.Add(i);

                    if (lista.Count < 2 && lista.Count > 0)
                        listBox.SelectedIndex = lista[0];

                    else if (lista.Count > 1)
                    {
                        float layerBase = 1;
                        int index = -1;
                        for (int i = 0; i < lista.Count; i++)
                        {
                            if (objectsOnList[lista[i]].layerDepth < layerBase)
                            {
                                layerBase = objectsOnList[lista[i]].layerDepth;
                                index = lista[i];
                            }
                        }
                        if (index != -1)
                            listBox.SelectedIndex = index;
                    }
                }
                //ok questo è per i box collider
                else if (mode == CreateMode.BoxesCollider)
                {
                    if (game.map.boxesCollider.Count == 1)
                    { listBox.SelectedIndex = 1; return; }

                    Vector2 mousePosition = Input.MousePositionCamera();
                    Rectangle input = new Rectangle((int)mousePosition.X, (int)mousePosition.Y, 1, 1);

                    for (int i = 0; i < game.map.boxesCollider.Count; i++)
                        if (game.map.boxesCollider[i].CheckCollision(input) == true)
                        { listBox.SelectedIndex = i; break; }
                }
                else if (mode == CreateMode.HUD)
                {
                    if (game.gameHUD.baseHUD.Count == 1)
                    { listBox.SelectedIndex = 1; return; }

                    Vector2 mousePosition = Input.MousePositionCamera();
                    Rectangle input = new Rectangle((int)mousePosition.X, (int)mousePosition.Y, 1, 1);

                    for(int i=0;i<game.gameHUD.baseHUD.Count; i++)
                    {
                        if (game.gameHUD.baseHUD[i].position.X == input.X && game.gameHUD.baseHUD[i].position.Y == input.Y)
                            listBox.SelectedIndex = i;
                    }

                }
            }

            else if (placingItem == true)
            {
                Vector2 mousePosition = Input.MousePositionCamera();
                Point desiredIndex = map.GetTileIndex(mousePosition);
                if (mode == CreateMode.Objects)
                    game.objects[game.objects.Count - 1].position = new Vector2(desiredIndex.X * map.tileSize, desiredIndex.Y * map.tileSize);
                else if (mode == CreateMode.Light)
                    game.lights[game.lights.Count - 1].position = new Vector2(desiredIndex.X * map.tileSize, desiredIndex.Y * map.tileSize);
                else if (mode == CreateMode.HUD)
                {
                    if(typeHUDSelected == BaseHUD.TypeHUD.Text)
                        game.gameHUD.baseHUD[game.gameHUD.baseHUD.Count - 1].position = new Vector2(Input.MousePositionCamera().X - Camera.screenRect.X, Input.MousePositionCamera().Y - Camera.screenRect.Y);
                    else
                        game.gameHUD.baseHUD[game.gameHUD.baseHUD.Count - 1].position = new Vector2(desiredIndex.X * map.tileSize, desiredIndex.Y * map.tileSize);
                }
            }
            if (indexLastObject != game.objects.Count - 1 && placingItem == false && mode == CreateMode.Objects && typeObjectSelected == GameObject.TypeObject.Tile && listBox.Items.Count > 1)
            {
                indexLastObject = game.objects.Count - 1;
                if (collisionObjectsTile() == true)
                {
                    game.objects.RemoveAt(game.objects.Count - 1);
                    SetListBox(returnObjectOnList(), false);
                }
            }


            //quando l'editor è in pausa lo si muove la cam con le frecette
            if (paused.Checked == false && game.objects.Count > 0)
            {
                if (Camera.updateYAxis == true)
                    Camera.updateYAxis = false;
                cameraPosition = game.objects[0].position;
            }
            else
            {
                if (Camera.updateYAxis == false)
                    Camera.updateYAxis = true;
                if (Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right))
                    cameraPosition.X += 15;
                else if (Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left))
                    cameraPosition.X -= 15;

                if (Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Down))
                    cameraPosition.Y += 15;
                else if (Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up))
                    cameraPosition.Y -= 15;

                Camera.Update(cameraPosition);
            }

            if (mode == CreateMode.Objects)
            {
                if (typeObjectSelected != (GameObject.TypeObject)objectTypes.Items[objectTypes.SelectedIndex])
                {
                    typeObjectSelected = (GameObject.TypeObject)objectTypes.Items[objectTypes.SelectedIndex];
                    objectsOnList = returnObjectOnList();

                    if (typeObjectSelected == GameObject.TypeObject.Tile)
                    {
                        UpdateTagDropDownTiles();
                        tagDropDown.Visible = true;
                        tagLabel.Visible = true;
                        removeAllButton.Visible = true;
                        addTagButton.Visible = true;
                        addTagText.Visible = true;
                        removeTagButton.Visible = true;
                        layerValue.Visible = true;
                        layerLabel.Visible = true;
                    }
                    else
                    {
                        layerValue.Visible = false;
                        layerLabel.Visible = false;
                        tagDropDown.Visible = false;
                        tagLabel.Visible = false;
                        addTagButton.Visible = false;
                        addTagText.Visible = false;
                        removeTagButton.Visible = false;
                        removeAllButton.Visible = false;
                    }
                    if (typeObjectSelected == GameObject.TypeObject.Enemy)
                    {
                        enemyStopCheckBox.Visible = true;
                    }
                    else
                    {
                        enemyStopCheckBox.Visible = false;
                    }
                    if (typeObjectSelected == GameObject.TypeObject.Player)
                        gravityCheckBox.Visible = true;
                    else
                        gravityCheckBox.Visible = false;

                    SetListBox(returnObjectOnList(), true);
                    UpdateAnimationText(objectsOnList);
                    UpdateSpriteText(objectsOnList);
                }
                ObjectProperty selectedProperty = (ObjectProperty)propertyTypes.Items[propertyTypes.SelectedIndex];
                if (selectedProperty == ObjectProperty.Animation && animationPath.Visible == false)
                {
                    OpenAnimationType();
                    UpdateAnimationText(objectsOnList);
                }
                else if (selectedProperty == ObjectProperty.Sprite && spritePath.Visible == false)
                {
                    objectsOnList = returnObjectOnList();
                    OpenSpriteType();
                    UpdateSpriteText(objectsOnList); 
                }
                if (changedIndex)
                {
                    if (selectedProperty == ObjectProperty.Animation)
                        UpdateAnimationText(objectsOnList);
                    if (selectedProperty == ObjectProperty.Sprite)
                        UpdateSpriteText(objectsOnList);
                    UpdateScaleText(objectsOnList);
                    UpdateLayerDepthText(objectsOnList);
                    UpdateCollidableCheckBox(objectsOnList);
                    if (typeObjectSelected == GameObject.TypeObject.Player)
                        UpdateGravityCheckBox(objectsOnList);
                    if (typeObjectSelected == GameObject.TypeObject.Enemy)
                        UpdateEnemyStopCheckBox(objectsOnList);
                }
                if (typeObjectSelected == GameObject.TypeObject.Tile)
                {
                    if (listBox.SelectedIndex > -1 && listBox.SelectedIndex < returnObjectOnList().Count && returnObjectOnList()[listBox.SelectedIndex].tagsObject != null)
                    {
                        GameObject i = returnObjectOnList()[listBox.SelectedIndex];
                        if (tagDropDown.SelectedIndex != returnObjectOnList()[listBox.SelectedIndex].tagsObject.key && !TagDropDownActive)
                        {
                            UpdateTagDropDownTiles();
                        }
                    }
                }
            }
            else if (mode == CreateMode.BoxesCollider)
            {
                if (listBox.SelectedIndex > -1 && listBox.SelectedIndex < game.map.boxesCollider.Count && game.map.boxesCollider[listBox.SelectedIndex].tagBoxCollider != null)
                {
                    if (tagDropDown.SelectedIndex != game.map.boxesCollider[listBox.SelectedIndex].tagBoxCollider.key && !TagDropDownActive)
                    {
                        UpdateTagDropDownTiles();
                    }
                }
            }
            else if (mode == CreateMode.Light && changedIndex)
            {
                UpdateLightScaleText();
                UpdateIntensityText();
            }
            else if (mode == CreateMode.HUD)
            {
                if (typeHUDSelected != (BaseHUD.TypeHUD)hudTypes.Items[hudTypes.SelectedIndex])
                {
                    typeHUDSelected = (BaseHUD.TypeHUD)hudTypes.Items[hudTypes.SelectedIndex];

                    changeHudTextBox.Visible = true;
                    changeHudTextLabel.Visible = true;
                    if (listBox.SelectedIndex > 0)
                        changeHudTextBox.Text = returnHudOnList()[listBox.SelectedIndex].text;

                    if (typeHUDSelected == BaseHUD.TypeHUD.Text)
                    {
                        tagLabel.Location = new System.Drawing.Point(2, 409);
                        tagDropDown.Location = new System.Drawing.Point(30, 407);
                        tagDropDown.Visible = true;
                        tagLabel.Visible = true;

                        addTagButton.Visible = true;
                        addTagButton.Location = new System.Drawing.Point(117, 434);

                        addTagText.Visible = true;
                        addTagText.Location = new System.Drawing.Point(30, 435);

                        removeTagButton.Visible = true;
                        removeTagButton.Location = new System.Drawing.Point(133, 407);

                        layerValue.Visible = false;
                        layerLabel.Visible = false;
                    }
                    else if(typeHUDSelected == BaseHUD.TypeHUD.Button)
                    {
                        layerValue.Visible = true;
                        layerLabel.Visible = true;

                        tagDropDown.Visible = false;
                        tagLabel.Visible = false;
                        tagLabel.Location = new System.Drawing.Point(2, 432);
                        tagDropDown.Location = new System.Drawing.Point(30, 430);
                        addTagButton.Visible = false;
                        addTagButton.Location = new System.Drawing.Point(117, 457);
                        addTagText.Visible = false;
                        addTagText.Location = new System.Drawing.Point(30, 458);
                        removeTagButton.Visible = false;
                        removeTagButton.Location = new System.Drawing.Point(133, 430);
                    }
                    SetListBox(returnHudOnList(), true);
                }
                if (listBox.SelectedIndex > -1 && listBox.SelectedIndex < returnHudOnList().Count && returnHudOnList()[listBox.SelectedIndex].tagHud != null)
                {
                    if (tagDropDown.SelectedIndex != returnHudOnList()[listBox.SelectedIndex].tagHud.key && !TagDropDownActive)
                    {
                        UpdateTagDropDownTiles();
                    }
                }
            }
        }

        private bool collisionObjectsTile()
        {
            List<GameObject> objectsOnList = returnObjectOnList();
            for (int i = 0; i < objectsOnList.Count-1; i++)
                if (objectsOnList[i].position == game.objects[game.objects.Count - 1].position && objectsOnList[i].spriteName == game.objects[game.objects.Count - 1].spriteName)
                    return true;

            return false;
        }

        private void tagDropDown_Active(object sender, EventArgs e)
        {
            TagDropDownActive = true;
        }
        private void tagDropDown_Disactive(object sender, EventArgs e)
        {
            TagDropDownActive = false;
        }

        private void UpdateTagDropDownTiles()
        {
            if (mode == CreateMode.Objects)
            {
                List<GameObject> objectOnList = new List<GameObject>();
                objectOnList = returnObjectOnList();
                if (listBox.SelectedIndex < objectOnList.Count)
                {
                    tagDropDown.Items.Clear();
                    for (int i = 0; i < Global.tagsObject.Count; i++)
                    {
                        tagDropDown.Items.Add(Global.tagsObject[i].tag);
                    }
                    if (listBox.SelectedIndex > -1)
                    {
                        if (objectOnList[listBox.SelectedIndex].tagsObject.key >= tagDropDown.Items.Count)
                            objectOnList[listBox.SelectedIndex].tagsObject = Global.tagsObject[0];

                        if (objectOnList[listBox.SelectedIndex].tagsObject != Global.tagsObject[0])
                            tagDropDown.SelectedIndex = objectOnList[listBox.SelectedIndex].tagsObject.key;
                        else
                            tagDropDown.SelectedIndex = Global.tagsObject[0].key; // zero sarabbe nulla
                    }
                    else
                        tagDropDown.SelectedIndex = Global.tagsObject[0].key; // zero sarabbe nulla
                }
            }

            else if (mode == CreateMode.BoxesCollider)
            {
                if (listBox.SelectedIndex < game.map.boxesCollider.Count)
                {
                    tagDropDown.Items.Clear();

                    for (int i = 0; i < Global.tagsBoxCollider.Count; i++)
                    {
                        tagDropDown.Items.Add(Global.tagsBoxCollider[i].tag);
                    }
                    if (listBox.SelectedIndex > -1)
                    {
                        if(game.map.boxesCollider[listBox.SelectedIndex].tagBoxCollider.key >= tagDropDown.Items.Count)
                            game.map.boxesCollider[listBox.SelectedIndex].tagBoxCollider = Global.tagsBoxCollider[0];

                        if (game.map.boxesCollider[listBox.SelectedIndex].tagBoxCollider != Global.tagsBoxCollider[0])
                            tagDropDown.SelectedIndex = game.map.boxesCollider[listBox.SelectedIndex].tagBoxCollider.key;
                        else
                            tagDropDown.SelectedIndex = Global.tagsBoxCollider[0].key;
                    }
                    else
                        tagDropDown.SelectedIndex = Global.tagsBoxCollider[0].key;
                }
            }

            else if (mode == CreateMode.HUD)
            {
                List<BaseHUD> hudOnList = new List<BaseHUD>();
                hudOnList = returnHudOnList();

                if (listBox.SelectedIndex < hudOnList.Count)
                {
                    tagDropDown.Items.Clear();

                    for (int i = 0; i < Global.tagsHud.Count; i++)
                    {
                        tagDropDown.Items.Add(Global.tagsHud[i].tag);
                    }
                    if (listBox.SelectedIndex > -1)
                    {
                        if (hudOnList[listBox.SelectedIndex].tagHud.key >= tagDropDown.Items.Count)
                            hudOnList[listBox.SelectedIndex].tagHud = Global.tagsHud[0];

                        if (hudOnList[listBox.SelectedIndex].tagHud != Global.tagsHud[0])
                            tagDropDown.SelectedIndex = hudOnList[listBox.SelectedIndex].tagHud.key;
                        else
                            tagDropDown.SelectedIndex = Global.tagsHud[0].key;
                    }
                    else
                        tagDropDown.SelectedIndex = Global.tagsHud[0].key;
                }
            }
        }

        private void UpdateAnimationText(List<GameObject> objectsOnList)
        {
            if (listBox.SelectedIndex >= 0)
                animationPath.Text = objectsOnList[listBox.SelectedIndex].animationName;
            else
                animationPath.Text = null;
        }

        private void UpdateSpriteText(List<GameObject> objectsOnList)
        {
            if (listBox.SelectedIndex >= 0 && listBox.SelectedIndex < objectsOnList.Count)
                spritePath.Text = objectsOnList[listBox.SelectedIndex].spriteName;
            else
                spritePath.Text = null;
        }
        private void UpdateScaleText(List<GameObject> objectsOnList)
        {
            if (listBox.SelectedIndex >= 0)
                scale.Value = (decimal)objectsOnList[listBox.SelectedIndex].scale;
            else
                scale.Value = scale.Value.CompareTo(null);
        }
        private void UpdateLightScaleText()
        {
            if (listBox.SelectedIndex >= 0)
                scale.Value = (decimal)game.lights[listBox.SelectedIndex].scale;
            else
                scale.Value = scale.Value.CompareTo(null);
        }

        private void UpdateIntensityText()
        {
            if (listBox.SelectedIndex >= 0)
                intensity.Value = (decimal)game.lights[listBox.SelectedIndex].intensity;
            else
                intensity.Value = intensity.Value.CompareTo(null);
        }

        private void UpdateLayerDepthText(List<GameObject> objectsOnList)
        {
            if (listBox.SelectedIndex >= 0)
                layerDepth.Value = (decimal)objectsOnList[listBox.SelectedIndex].layerDepth;
            else
                layerDepth.Value = layerDepth.Value.CompareTo(null);
        }

        private void UpdateEnemyStopCheckBox(List<GameObject> objectsOnList)
        {
            if (listBox.SelectedIndex >= 0)
                enemyStopCheckBox.Checked = objectsOnList[listBox.SelectedIndex].blocca;
            else
                enemyStopCheckBox.Checked = Convert.ToBoolean(null);
        }

        private void UpdateCollidableCheckBox(List<GameObject> objectsOnList)
        {
            if (listBox.SelectedIndex >= 0)
                collidableCheckBox.Checked = objectsOnList[listBox.SelectedIndex].collidable;
            else
                collidableCheckBox.Checked = Convert.ToBoolean(null);
        }
        private void UpdateGravityCheckBox(List<GameObject> objectsOnList)
        {
            if (listBox.SelectedIndex >= 0)
                gravityCheckBox.Checked = objectsOnList[listBox.SelectedIndex].applyGravity;
            else
                gravityCheckBox.Checked = Convert.ToBoolean(null);
        }
        private void layer_ValueChanged(object sender, EventArgs e)
        {
            if (mode == CreateMode.Objects)
                returnObjectOnList()[listBox.SelectedIndex].Layer = (int)layerValue.Value; 
            if (mode == CreateMode.HUD)
                returnHudOnList()[listBox.SelectedIndex].Layer = (int)layerValue.Value;
        }

        private void OpenAnimationType()
        {
            spritePath.Visible = false;
            spritePathLabel.Visible = false;
            loadSpriteButton.Visible = false;
            removeSpriteButton.Visible = false;

            animationPathLabel.Visible = true;
            animationPath.Visible = true;
            loadAnimationButton.Visible = true;
            removeAnimationButton.Visible = true;
        }

        private void OpenSpriteType()
        {
            animationPathLabel.Visible = false;
            animationPath.Visible = false;
            loadAnimationButton.Visible = false;
            removeAnimationButton.Visible = false;

            spritePath.Visible = true;
            spritePathLabel.Visible = true;
            loadSpriteButton.Visible = true;
            removeSpriteButton.Visible = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //evidenzia l'ogetto selezionato
            DrawSelectedItem(spriteBatch);
            
            if (drawGridCheckBox.Checked == false)
                return;
            //disegna la griglia
            for (int x = 0; x < game.map.mapWidth; x++)
            {
                for (int y = 0; y < game.map.mapHeight; y++)
                    spriteBatch.Draw(grid, new Vector2(x, y) * game.map.tileSize, null, Color.Cyan, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            }
        }

        #region Helpers

        #region DLL Functions
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect); 
        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        #endregion

        private void DrawSelectedItem(SpriteBatch spriteBatch)
        {
            if (drawSelected.Checked == false)
                return;

            if (mode == CreateMode.BoxesCollider)
            {
                if (game.map.boxesCollider.Count == 0 || listBox.SelectedIndex >= game.map.boxesCollider.Count)
                    return;

                BoxCollider selectedBoxCollider = game.map.boxesCollider[listBox.SelectedIndex];
                spriteBatch.Draw(pixel, new Vector2((int)selectedBoxCollider.boxCollider.X, (int)selectedBoxCollider.boxCollider.Y), selectedBoxCollider.boxCollider, Color.SkyBlue, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0);
            }
            else if (mode == CreateMode.Objects)
            {
                if (game.objects.Count == 0 || listBox.SelectedIndex >= game.objects.Count)
                    return;

                if (listBox.SelectedIndex >= 0)
                {
                    GameObject selectedObject = returnObjectOnList()[listBox.SelectedIndex];
                    spriteBatch.Draw(pixel, new Vector2((int)selectedObject.BoundingBox.X, (int)selectedObject.BoundingBox.Y), selectedObject.BoundingBox, new Color(80, 80, 100, 80), 0f, Vector2.Zero, 1f, SpriteEffects.None, selectedObject.layerDepth);
                }
            }
        }

        public void RemoveBoxCollider(int index)
        {
            int bookmarkIndex = listBox.SelectedIndex;
            game.map.boxesCollider.RemoveAt(index);

            SetListBox(game.map.boxesCollider, false);
        }

        public void PopulateObjectList()
        {
            for (int i = 0; i < (int)ObjectType.NumOfObjects; i++)
                objectTypes.Items.Add((ObjectType)i);

            objectTypes.SelectedIndex = 0;
        }
        public void PopulateHudList()
        {
            for (int i = 0; i < (int)HUDType.NumOfObjects; i++)
                hudTypes.Items.Add((HUDType)i);

            hudTypes.SelectedIndex = 0;
        }

        public void PopulatePropertyList()
        {
            for (int i = 0; i < (int)ObjectProperty.NumOfObjects; i++)
                propertyTypes.Items.Add((ObjectProperty)i);

            propertyTypes.SelectedIndex = 0;

        }

        private void ResetEditorList()
        {
            objectsRadioButton.Checked = boxesColliderRadioButton.Checked = false;
            noneRadioButton.Checked = true;
            List<int> nothing = new List<int>();
            SetListBox(nothing, true);
            FocusGameWindow();
        }

        private void LoadLevelContent()
        {
            for (int i = 0; i < game.objects.Count; i++)
            {
                game.objects[i].Initialize();
                game.objects[i].Load(game.Content);
            }
        }
        #endregion

        public void SetListBox<T>(List<T> inputList, bool highlightFirstInList)
        {
            //aggiorna la lista
            listBox.DataSource = null;
            listBox.DataSource = inputList;

            if (highlightFirstInList == true && inputList != null && inputList.Count > 0)
                listBox.SelectedIndex = listBox.TopIndex = 0;
            else if (highlightFirstInList == true && inputList != null)
                listBox.SelectedIndex = listBox.TopIndex = -1;
            else if (listBox.SelectedIndex < 0 && listBox.Items.Count > 0)
                listBox.SelectedIndex = 0;
            else
                listBox.SelectedIndex = listBox.Items.Count - 1;
        }

        public void RefreshListBox<T>(List<T> inputList)
        {
            //la chiamo quando cancello qualcosa e quindi devo aggiornare la lista
            if (listBox.SelectedIndex - 1 >= 0)
                listBox.SelectedIndex--; 

            placingItem = false;

            int bookmarkIndex = listBox.SelectedIndex;
            string displayMember = "";

            if (mode == CreateMode.BoxesCollider)
            {
                if (bookmarkIndex == -1 && game.map.boxesCollider.Count > 0)
                    bookmarkIndex = 0;
            }
            else if (mode == CreateMode.Objects)
            {
                if (bookmarkIndex == -1 && game.objects.Count > 0)
                    bookmarkIndex = 0;
            }
            else if (mode == CreateMode.Light)
            {
                if (bookmarkIndex == -1 && game.lights.Count > 0)
                    bookmarkIndex = 0;
            }
            else if (mode == CreateMode.HUD)
            {
                if (bookmarkIndex == -1 && game.gameHUD.baseHUD.Count > 0)
                    bookmarkIndex = 0;
            }

            int bookmarkTopIndex = listBox.TopIndex;
            listBox.DataSource = null;
            listBox.DataSource = inputList;
            listBox.DisplayMember = displayMember;
            if (listBox.DataSource != null && inputList.Count > 0)
            {
                listBox.SelectedIndex = bookmarkIndex;
                listBox.TopIndex = bookmarkTopIndex;
            }
        }

        private void tagDropDown_IndexChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                if (mode == CreateMode.Objects)
                    returnObjectOnList()[listBox.SelectedIndex].tagsObject = Global.tagsObject[tagDropDown.SelectedIndex];
                if (mode == CreateMode.BoxesCollider)
                    game.map.boxesCollider[listBox.SelectedIndex].tagBoxCollider = Global.tagsBoxCollider[tagDropDown.SelectedIndex];
                if (mode == CreateMode.HUD)
                    game.gameHUD.baseHUD[listBox.SelectedIndex].tagHud = Global.tagsHud[tagDropDown.SelectedIndex];
            }
        }

        private void removeTagButton_Click(object sender, EventArgs e)
        {
            if (tagDropDown.SelectedIndex != 0)
            {
                if (mode == CreateMode.HUD)
                {
                    for (int i = 0; i < game.gameHUD.baseHUD.Count; i++)
                        if (game.gameHUD.baseHUD[i].tagHud == Global.tagsHud[tagDropDown.SelectedIndex])
                            game.gameHUD.baseHUD[i].tagHud = Global.tagsHud[0];

                    Global.tagsHud.RemoveAt(tagDropDown.SelectedIndex);
                    for (int i = 0; i < Global.tagsHud.Count; i++)
                        Global.tagsHud[i].key = i;

                    tagDropDown.SelectedIndex = 0;
                    UpdateTagDropDownTiles();
                }
                if (mode == CreateMode.BoxesCollider)
                {
                    for (int i = 0; i < game.map.boxesCollider.Count; i++)
                        if (game.map.boxesCollider[i].tagBoxCollider == Global.tagsBoxCollider[tagDropDown.SelectedIndex])
                            game.map.boxesCollider[i].tagBoxCollider = Global.tagsBoxCollider[0];

                    Global.tagsBoxCollider.RemoveAt(tagDropDown.SelectedIndex);
                    for (int i = 0; i < Global.tagsBoxCollider.Count; i++)
                        Global.tagsBoxCollider[i].key = i;

                    tagDropDown.SelectedIndex = 0;
                    UpdateTagDropDownTiles();
                }
                else if (mode == CreateMode.Objects)
                {
                    List<GameObject> listObjectOnList = returnObjectOnList();

                    for (int i = 0; i < listObjectOnList.Count; i++)
                        if (listObjectOnList[i].tagsObject == Global.tagsObject[tagDropDown.SelectedIndex])
                            listObjectOnList[i].tagsObject = Global.tagsObject[0];

                    Global.tagsObject.RemoveAt(tagDropDown.SelectedIndex);
                    for (int i = 0; i < Global.tagsObject.Count; i++)
                        Global.tagsObject[i].key = i;

                    tagDropDown.SelectedIndex = 0;
                    UpdateTagDropDownTiles();
                }
            }
        }
        private void addTagButton_Click(object sender, EventArgs e)
        {
            if (addTagText.Text != "")
            {
                if (mode == CreateMode.HUD)
                {
                    Global.tagsHud.Add(new TagObject(Global.tagsHud.Count, addTagText.Text));
                    returnHudOnList()[listBox.SelectedIndex].tagHud = Global.tagsHud[Global.tagsHud.Count - 1];
                    UpdateTagDropDownTiles();
                    tagDropDown.SelectedIndex = Global.tagsHud.Count - 1;
                }
                if (mode == CreateMode.BoxesCollider)
                {
                    Global.tagsBoxCollider.Add(new TagObject(Global.tagsBoxCollider.Count, addTagText.Text));
                    game.map.boxesCollider[listBox.SelectedIndex].tagBoxCollider = Global.tagsBoxCollider[Global.tagsBoxCollider.Count - 1];
                    UpdateTagDropDownTiles();
                    tagDropDown.SelectedIndex = Global.tagsBoxCollider.Count - 1;
                }
                else if (mode == CreateMode.Objects)
                {
                    Global.tagsObject.Add(new TagObject(Global.tagsObject.Count, addTagText.Text));
                    returnObjectOnList()[listBox.SelectedIndex].tagsObject = Global.tagsObject[Global.tagsObject.Count - 1];
                    UpdateTagDropDownTiles();
                    tagDropDown.SelectedIndex = Global.tagsObject.Count - 1;
                }
                addTagText.Text = "";
            }
        }

        private void boxesColliderRadioButtonRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (boxesColliderRadioButton.Checked == true)
            {
                mode = CreateMode.BoxesCollider;
                SetListBox(game.map.boxesCollider, true);

                tagLabel.Location = new System.Drawing.Point(2, 367);
                tagDropDown.Location = new System.Drawing.Point(30, 365);
                tagDropDown.Visible = true;
                tagLabel.Visible = true;
                addTagButton.Visible = true;
                addTagButton.Location = new System.Drawing.Point(117, 394);

                addTagText.Visible = true;
                addTagText.Location = new System.Drawing.Point(30, 395);

                removeTagButton.Visible = true;
                removeTagButton.Location = new System.Drawing.Point(133, 365);
                UpdateTagDropDownTiles();
                height.Enabled = width.Enabled = true;
            }
            else
            {
                tagDropDown.Visible = false;
                tagLabel.Visible = false;
                tagLabel.Location = new System.Drawing.Point(2, 432);
                tagDropDown.Location = new System.Drawing.Point(30, 430);
                addTagButton.Visible = false;
                addTagButton.Location = new System.Drawing.Point(117, 457);
                addTagText.Visible = false;
                addTagText.Location = new System.Drawing.Point(30, 458);
                removeTagButton.Visible = false;
                removeTagButton.Location = new System.Drawing.Point(133, 430);
            }
        }

        private void objectsRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (objectsRadioButton.Checked == true)
            {
                mode = CreateMode.Objects;
                SetListBox(returnObjectOnList(), true);

                objectTypes.Visible = true;
                propertyTypes.Visible = true;
                propertyText.Visible = true;
                scaleLabel.Visible = true;
                scale.Visible = true;
                layerDepthLabel.Visible = true;
                layerDepth.Visible = true;
                collidableCheckBox.Visible = true;
                if (listBox.SelectedIndex >= 0)
                    scale.Value = (decimal)returnObjectOnList()[listBox.SelectedIndex].scale;

                if ((GameObject.TypeObject)objectTypes.Items[objectTypes.SelectedIndex] == GameObject.TypeObject.Player)
                { 
                    gravityCheckBox.Visible = true; 
                    if(listBox.SelectedIndex != -1) 
                        gravityCheckBox.Checked = game.objects[listBox.SelectedIndex].applyGravity; 
                }
                if ((GameObject.TypeObject)objectTypes.Items[objectTypes.SelectedIndex] == GameObject.TypeObject.Tile)
                {
                    removeAllButton.Visible = true;
                    tagDropDown.Visible = true;
                    tagLabel.Visible = true;
                    addTagButton.Visible = true;
                    addTagText.Visible = true;
                    removeTagButton.Visible = true;
                    layerValue.Visible = true;
                    layerLabel.Visible = true;
                    UpdateTagDropDownTiles();
                }
                if ((GameObject.TypeObject)objectTypes.Items[objectTypes.SelectedIndex] == GameObject.TypeObject.Enemy)
                {
                    enemyStopCheckBox.Visible = true;
                }
                width.Visible = false;
                wLabel.Visible = false;
                height.Visible = false;
                hLabel.Visible = false;
                height.Enabled = width.Enabled = false;
            }
            else
            {
                layerValue.Visible = false;
                layerLabel.Visible = false;
                tagLabel.Visible = false;
                tagDropDown.Visible = false;
                addTagButton.Visible = false;
                addTagText.Visible = false;
                removeTagButton.Visible = false;
                objectTypes.Visible = false;
                propertyTypes.Visible = false;
                propertyText.Visible = false;
                animationPathLabel.Visible = false;
                animationPath.Visible = false;
                loadAnimationButton.Visible = false;
                removeAnimationButton.Visible = false;
                spritePath.Visible = false;
                spritePathLabel.Visible = false;
                loadSpriteButton.Visible = false;
                removeSpriteButton.Visible = false;
                scaleLabel.Visible = false;
                scale.Visible = false;
                layerDepthLabel.Visible = false;
                layerDepth.Visible = false;
                enemyStopCheckBox.Visible = false;
                collidableCheckBox.Visible = false;
                removeAllButton.Visible = false;
                gravityCheckBox.Visible = false;
                width.Visible = true;
                wLabel.Visible = true;
                height.Visible = true;
                hLabel.Visible = true;
            }
        }
        
        private void lightRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (lightRadioButton.Checked == true)
            {
                mode = CreateMode.Light;
                SetListBox(game.lights, true);

                scaleLabel.Visible = true;
                scale.Visible = true;

                if (listBox.SelectedIndex >= 0)
                {
                    scale.Value = (decimal)game.lights[listBox.SelectedIndex].scale;
                    intensity.Value = (decimal)game.lights[listBox.SelectedIndex].intensity;
                }
                intensityLabel.Visible = true;
                intensity.Visible = true;

                width.Visible = false;
                wLabel.Visible = false;
                height.Visible = false;
                hLabel.Visible = false;
                height.Enabled = width.Enabled = false;
            }
            else
            {
                scaleLabel.Visible = false;
                scale.Visible = false;
                intensityLabel.Visible = false;
                intensity.Visible = false;
                width.Visible = true;
                wLabel.Visible = true;
                height.Visible = true;
                hLabel.Visible = true;
            }
        }
        
        private void hudRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (hudRadioButton.Checked == true)
            {
                mode = CreateMode.HUD;
                SetListBox(returnHudOnList(), true);

                hudTypes.Visible = true;
                changeHudTextBox.Visible = true;
                changeHudTextLabel.Visible = true;
                if (listBox.SelectedIndex > 0)
                    changeHudTextBox.Text = returnHudOnList()[listBox.SelectedIndex].text;

                if (typeHUDSelected == BaseHUD.TypeHUD.Text)
                {
                    tagLabel.Location = new System.Drawing.Point(2, 409);
                    tagDropDown.Location = new System.Drawing.Point(30, 407);
                    tagDropDown.Visible = true;
                    tagLabel.Visible = true;

                    addTagButton.Visible = true;
                    addTagButton.Location = new System.Drawing.Point(117, 434);

                    addTagText.Visible = true;
                    addTagText.Location = new System.Drawing.Point(30, 435);

                    removeTagButton.Visible = true;
                    removeTagButton.Location = new System.Drawing.Point(133, 407);
                }
                if (typeHUDSelected == BaseHUD.TypeHUD.Button)
                {
                    layerValue.Visible = true;
                    layerLabel.Visible = true;
                }
                UpdateTagDropDownTiles();
                width.Visible = false;
                wLabel.Visible = false;
                height.Visible = false;
                hLabel.Visible = false;
                height.Enabled = width.Enabled = false;
            }
            else
            {
                tagDropDown.Visible = false;
                tagLabel.Visible = false;
                tagLabel.Location = new System.Drawing.Point(2, 432);
                tagDropDown.Location = new System.Drawing.Point(30, 430);
                addTagButton.Visible = false;
                addTagButton.Location = new System.Drawing.Point(117, 457);
                addTagText.Visible = false;
                addTagText.Location = new System.Drawing.Point(30, 458);
                removeTagButton.Visible = false;
                removeTagButton.Location = new System.Drawing.Point(133, 430);
                changeHudTextBox.Visible = false;
                changeHudTextLabel.Visible = false;
                hudTypes.Visible = false;
                layerValue.Visible = false;
                layerLabel.Visible = false;
                width.Visible = true;
                wLabel.Visible = true;
                height.Visible = true;
                hLabel.Visible = true;
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex == -1)
                return;

            if (mode == CreateMode.BoxesCollider)
            {
                game.map.boxesCollider.RemoveAt(listBox.SelectedIndex);
                RefreshListBox(game.map.boxesCollider);
            }
            else if (mode == CreateMode.Objects && listBox.SelectedIndex != -1) //Un controllo in piu per far si che non si stia mai senza un player
            {
                if (Global.levelName == "Level1.jorge")
                {
                    if (returnObjectOnList()[listBox.SelectedIndex] is Player == false || listBox.SelectedIndex > 0)
                    {
                        destroyObjectFromList(listBox.SelectedIndex);
                        RefreshListBox(returnObjectOnList());
                    }
                }
                else
                {
                    destroyObjectFromList(listBox.SelectedIndex);
                    RefreshListBox(returnObjectOnList());
                }
            }
            else if (mode == CreateMode.Light)
            {
                game.lights.RemoveAt(listBox.SelectedIndex);
                RefreshListBox(game.lights);
            }
            else if (mode == CreateMode.HUD)
            {
                destroyHudFromList(listBox.SelectedIndex);
                RefreshListBox(returnHudOnList());            
            }

            placingItem = false;
        }
        private void removeAllButton_Click(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex == -1)
                return;
            
            game.objects.RemoveAll(delegate (GameObject gameObject)
            {
                return gameObject is Tile;
            });
            
            RefreshListBox(returnObjectOnList());
            

            placingItem = false;
        }
        
        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex == -1)
                return;
            if (mode == CreateMode.BoxesCollider)
            {
                Rectangle selectedBoxCollider = game.map.boxesCollider[listBox.SelectedIndex].boxCollider;
                xPosition.Value = selectedBoxCollider.X;
                yPosition.Value = selectedBoxCollider.Y;
                height.Value = selectedBoxCollider.Height;
                width.Value = selectedBoxCollider.Width;
            }
            else if (mode == CreateMode.Objects)
            {
                if (listBox.SelectedIndex < returnObjectOnList().Count)
                {
                    GameObject selectedObject = returnObjectOnList()[listBox.SelectedIndex];
                    xPosition.Value = (int)selectedObject.position.X;
                    yPosition.Value = (int)selectedObject.position.Y;
                    if (typeObjectSelected == GameObject.TypeObject.Tile)
                        layerValue.Value = selectedObject.Layer;
                }
            }
            else if (mode == CreateMode.Light)
            {
                Light selectedObject = game.lights[listBox.SelectedIndex];
                xPosition.Value = (int)selectedObject.position.X;
                yPosition.Value = (int)selectedObject.position.Y;
            }
            else if (mode == CreateMode.HUD)
            {
                BaseHUD selectedObject = game.gameHUD.baseHUD[listBox.SelectedIndex];
                xPosition.Value = (int)selectedObject.position.X;
                yPosition.Value = (int)selectedObject.position.Y;
                changeHudTextBox.Text = returnHudOnList()[listBox.SelectedIndex].text;
            }
        }

        private void xPosition_ValueChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex == -1)
                return;
            if (mode == CreateMode.BoxesCollider)
            {
                Rectangle selectedBoxCollider = game.map.boxesCollider[listBox.SelectedIndex].boxCollider;
                selectedBoxCollider.X = (int)xPosition.Value;
                game.map.boxesCollider[listBox.SelectedIndex].boxCollider = selectedBoxCollider;
            }
            else if (mode == CreateMode.Objects)
                returnObjectOnList()[listBox.SelectedIndex].position.X = (float)xPosition.Value;
            else if (mode == CreateMode.Light)
                game.lights[listBox.SelectedIndex].position.X = (float)xPosition.Value;
            else if (mode == CreateMode.HUD)
                returnHudOnList()[listBox.SelectedIndex].position.X = (float)xPosition.Value;
        }

        private void yPosition_ValueChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex == -1)
                return;

            if (mode == CreateMode.BoxesCollider)
            {
                Rectangle selectedBoxCollider = game.map.boxesCollider[listBox.SelectedIndex].boxCollider;
                selectedBoxCollider.Y = (int)yPosition.Value;
                game.map.boxesCollider[listBox.SelectedIndex].boxCollider = selectedBoxCollider;
            }
            else if (mode == CreateMode.Objects)
                returnObjectOnList()[listBox.SelectedIndex].position.Y = (float)yPosition.Value;
            else if (mode == CreateMode.Light)
                game.lights[listBox.SelectedIndex].position.Y = (float)yPosition.Value;
            else if (mode == CreateMode.HUD)
                returnHudOnList()[listBox.SelectedIndex].position.Y = (float)yPosition.Value;
        }

        private void width_ValueChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex == -1)
                return;

            if (mode == CreateMode.BoxesCollider)
            {
                Rectangle selectedBoxCollider = game.map.boxesCollider[listBox.SelectedIndex].boxCollider;
                selectedBoxCollider.Width = (int)width.Value;
                game.map.boxesCollider[listBox.SelectedIndex].boxCollider = selectedBoxCollider;
            }
        }

        private void height_ValueChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex == -1)
                return;

            if (mode == CreateMode.BoxesCollider)
            {
                Rectangle selectedBoxCollider = game.map.boxesCollider[listBox.SelectedIndex].boxCollider;
                selectedBoxCollider.Height = (int)height.Value;
                game.map.boxesCollider[listBox.SelectedIndex].boxCollider = selectedBoxCollider;
            }
        }

        private void scale_ValueChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex == -1)
                return;

            if (mode == CreateMode.Objects)
            {
                returnObjectOnList()[listBox.SelectedIndex].scale = (float)scale.Value;
                returnObjectOnList()[listBox.SelectedIndex].Load(game.Content);
            }
            if (mode == CreateMode.Light)
            {
                game.lights[listBox.SelectedIndex].scale = (float)scale.Value;
                game.lights[listBox.SelectedIndex].Load(game.Content);
            }
        }
        private void intensity_ValueChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex == -1)
                return;

            if (mode == CreateMode.Light)
            {
                game.lights[listBox.SelectedIndex].intensity = (float)intensity.Value;
            }
        }

        private void layerDepth_ValueChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex == -1)
                return;

            if (mode == CreateMode.Objects)
                returnObjectOnList()[listBox.SelectedIndex].layerDepth = (float)layerDepth.Value;
        }

        private void loadAnimationButton_Click(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex == -1)
                return;

            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Filter = "ANM (.anm)|*.anm",
                FilterIndex = 1,
                Multiselect = false
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    List<GameObject> gameObjectOnList = returnObjectOnList();
                    gameObjectOnList[listBox.SelectedIndex].animationName = openFileDialog1.SafeFileName;
                    gameObjectOnList[listBox.SelectedIndex].LoadAnimation();
                    UpdateAnimationText(gameObjectOnList);
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Error Loading Image: " + exception.Message);
                }
            }
        }
        
        private void removeAnimationButton_Click(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex == -1)
                return;

            List<GameObject> gameObjectOnList = returnObjectOnList();
            gameObjectOnList[listBox.SelectedIndex].animationName = null;
            gameObjectOnList[listBox.SelectedIndex].LoadAnimation();
            UpdateAnimationText(gameObjectOnList);
        }
        
        private void loadSpriteButton_Click(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex == -1)
                return;

            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Filter = "XNB (.xnb)|*.xnb",
                FilterIndex = 2,
                Multiselect = false
            };
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    List<GameObject> gameObjectOnList = returnObjectOnList();
                    string fileDirectory = game.Content.RootDirectory;
                    gameObjectOnList[listBox.SelectedIndex].spriteName = openFileDialog1.SafeFileName;
                    fileDirectory = openFileDialog1.FileName.Remove(openFileDialog1.FileName.Length-openFileDialog1.SafeFileName.Length, openFileDialog1.SafeFileName.Length);
                    game.Content.RootDirectory = fileDirectory.Remove(0, contentFullPath.Length);
                    gameObjectOnList[listBox.SelectedIndex].LoadSprite(game.Content);
                    UpdateSpriteText(gameObjectOnList);
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Error Loading Image: " + exception.Message);
                }
            }
        }
        private void removeSpriteButton_Click(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex == -1)
                return;

            List<GameObject> gameObjectOnList = returnObjectOnList();
            gameObjectOnList[listBox.SelectedIndex].spriteName = null;
            gameObjectOnList[listBox.SelectedIndex].LoadSprite(game.Content);
            UpdateSpriteText(gameObjectOnList);
        }

        private void menuStrip_MouseEnter(object sender, EventArgs e)
        {
            Focus();
        }

        private void mapWidth_ValueChanged(object sender, EventArgs e)
        {
            if (mapWidth.Value > mapWidth.Maximum)
                mapWidth.Value = mapWidth.Maximum;

            game.map.mapWidth = (int)mapWidth.Value;
        }

        private void mapHeight_ValueChanged(object sender, EventArgs e)
        {
            if (mapHeight.Value > mapHeight.Maximum)
                mapHeight.Value = mapHeight.Maximum;

            game.map.mapHeight = (int)mapHeight.Value;
        }

        private void textBox_ValueChanged(object sender, EventArgs e)
        {
            if(listBox.SelectedIndex >= 0)
                returnHudOnList()[listBox.SelectedIndex].text = changeHudTextBox.Text;
        }

        private void paused_CheckedChanged(object sender, EventArgs e)
        {
            FocusGameWindow();
        }
        private void collidable_CheckedChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex >= 0)
                returnObjectOnList()[listBox.SelectedIndex].collidable = collidableCheckBox.Checked;
        }
        
        private void enemyStop_CheckedChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex >= 0)
                returnObjectOnList()[listBox.SelectedIndex].blocca = enemyStopCheckBox.Checked;
        }
        private void gravity_CheckedChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex >= 0)
                returnObjectOnList()[listBox.SelectedIndex].applyGravity = gravityCheckBox.Checked;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //quando selezioniamo new 
            game.objects.Clear();
            game.map.boxesCollider.Clear();

            game.objects.Add(new Player(Vector2.Zero));

            mapWidth.Value = game.map.mapWidth = 30;
            mapHeight.Value = game.map.mapHeight = 17;
            savePath = "";

            for (int i = 0; i < game.objects.Count; i++)
            {
                game.objects[i].Load(game.Content);
                game.objects[i].Initialize();
            }

            ResetEditorList();
        }

        private void Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            game.Exit();
        }

        private void noneRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (noneRadioButton.Checked == true)
            {
                mode = CreateMode.None;
                List<int> nothing = new List<int>();
                SetListBox(nothing, false);
            }
        }

        private void FocusGameWindow()
        {
            SetForegroundWindow(gameWinHandle);
        }

        private bool GameWindowFocused()
        {
            return GetForegroundWindow() == gameWinHandle;
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenLevel();
        }

        public List<GameObject> returnObjectOnList()
        {
            List<GameObject> listGameObjects = new List<GameObject>();
            for (int i = 0; i < game.objects.Count; i++)
            {
                if (game.objects[i].typeObject == typeObjectSelected)
                    listGameObjects.Add(game.objects[i]);
            }
            return listGameObjects;
        }
        public List<BaseHUD> returnHudOnList()
        {
            List<BaseHUD> listHUDObjects = new List<BaseHUD>();
            for (int i = 0; i < game.gameHUD.baseHUD.Count; i++)
            {
                if (game.gameHUD.baseHUD[i].typeHUD == typeHUDSelected)
                    listHUDObjects.Add(game.gameHUD.baseHUD[i]);
            }
            return listHUDObjects;
        }

        private void destroyObjectFromList(int index)
        {
            List<GameObject> objectsOnList = returnObjectOnList();
            for (int i = 0; i < game.objects.Count; i++)
            {
                for (int h = 0; h < objectsOnList.Count; h++)
                {
                    if (game.objects[i].visible == true)
                    {
                        if (game.objects[i] == objectsOnList[index])
                        {
                            game.objects.RemoveAt(i);
                            return;
                        }
                    }
                }
            }
        }
        private void destroyHudFromList(int index)
        {
            List<BaseHUD> objectsOnList = returnHudOnList();
            for (int i = 0; i < game.gameHUD.baseHUD.Count; i++)
            {
                for (int h = 0; h < objectsOnList.Count; h++)
                {   
                    if (game.gameHUD.baseHUD[i] == objectsOnList[index])
                    {
                        game.gameHUD.baseHUD.RemoveAt(i);
                        return;
                    }   
                }
            }
        }

        private void SaveAs()
        {
            //qui fa la finestra per selezionare dove salvare
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            savePath = "";

            //(jorge è il tipo di file in cui dobbiamo salvare)
            saveFileDialog.Filter = "JORGE (.jorge)|*.jorge";
            
            try
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    savePath = saveFileDialog.FileName;

                    SaveLevel();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error Saving: " + exception.Message + " " + exception.InnerException);
            }
        }

        private void Save()
        {
            if (savePath == "")
            {
                SaveAs();
                return;
            }

            try
            {
                SaveLevel();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error Saving: " + exception.Message + " " + exception.InnerException);
            }
        }

        private void SaveLevel()
        {
            LevelData levelData = new LevelData()
            {
                objects = game.objects,
                boxesCollider = game.map.boxesCollider,
                ligths = game.lights,
                baseHud = game.gameHUD.baseHUD,
                tagsHud = Global.tagsHud,
                tagsObject = Global.tagsObject,
                tagsBoxCollider = Global.tagsBoxCollider,
                mapWidth = game.map.mapWidth,
                mapHeight = game.map.mapHeight,
            };

            XmlHelper.Save(levelData, savePath);
            Global.levelName = savePath;
            Thread copyThread = new Thread(game.inputCopy);
            copyThread.Start();
        }

        public void OpenLevel()
        {
            //loading livello
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "JORGE (.jorge)|*.jorge";

            //l'utente disattivandco  questo non può prendere pi ogetti
            openFileDialog1.Multiselect = false;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    LevelData levelData = XmlHelper.Load(openFileDialog1.FileName);
                    Global.levelName = openFileDialog1.SafeFileName;
                    //carichiamo il file da loadare
                    game.objects = levelData.objects;
                    game.map.boxesCollider = levelData.boxesCollider;
                    game.lights = levelData.ligths;
                    game.gameHUD.baseHUD = levelData.baseHud;
                    Global.tagsHud = levelData.tagsHud;
                    Global.tagsObject = levelData.tagsObject;
                    Global.tagsBoxCollider = levelData.tagsBoxCollider;
                    mapWidth.Value = game.map.mapWidth = levelData.mapWidth;
                    mapHeight.Value = game.map.mapHeight = levelData.mapHeight;
                    
                    LoadLevelContent();

                    if (game.objects.Count > 0)
                        Camera.LookAt(game.objects[0].position);

                    ResetEditorList();

                    savePath = "";

                    FocusGameWindow();
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Error Loading: " + exception.Message + " " + exception.InnerException);
                }
            }
        }
    }
}
