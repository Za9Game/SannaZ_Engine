using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;
using System.Xml.Serialization;

namespace SannaZ_Engine
{
    public static class AnimationLoader
    { 
        public static AnimationData Load(string name)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(AnimationData));
            TextReader reader = new StreamReader("Content\\Animations\\" + name);
            AnimationData obj = (AnimationData)serializer.Deserialize(reader);
            reader.Close();
            return obj;
        }

    }
}
