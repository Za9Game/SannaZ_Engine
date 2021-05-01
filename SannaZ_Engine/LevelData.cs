using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Xml.Serialization;

namespace SannaZ_Engine
{
    public class LevelData
    {
        [XmlElement("Player", Type = typeof(Player))]
        [XmlElement("Enemy", Type = typeof(Enemy))]
        [XmlElement("Tile", Type = typeof(Tile))]

        public List<GameObject> objects { get; set; }
        public List<BoxCollider> boxesCollider { get; set; }
        public List<Light> ligths { get; set; }

        [XmlElement("Text", Type = typeof(Text))]
        [XmlElement("Button", Type = typeof(Button))]
        public List<BaseHUD> baseHud { get; set; }

        public List<TagObject> tagsObject { get; set; }
        public List<TagObject> tagsHud { get; set; }
        public List<TagObject> tagsBoxCollider { get; set; }


        public int mapWidth { get; set; }
        public int mapHeight { get; set; }

    }
}
