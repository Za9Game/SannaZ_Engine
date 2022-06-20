using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SannaZ_Engine
{
    public static class Global
    {
        public static Map map;
        public static Score score;
        public static Random random = new Random();
        public static string levelName;
        public static List<TagObject> tagsObject = new List<TagObject>();
        public static List<TagObject> tagsHud = new List<TagObject>();
        public static List<TagObject> tagsBoxCollider = new List<TagObject>();

#if DEBUG
        public static Game1 game;
        public static void Initialize(Game1 inputGame, Map inputMap, Score inputScore)
        {
            game = inputGame;
            map = inputMap;
            score = inputScore;
        }
#else
        public static Game1onlyRender game;
        public static void Initialize(Game1onlyRender inputGame, Map inputMap, Score inputScore)
        {
            game = inputGame;
            map = inputMap;
            score = inputScore;
        }
#endif

        public static int TagIndex(List<TagObject> listaTag, string tag)
        {
            if(TagExist(listaTag, tag))
                for (int i = 0; i < listaTag.Count; i++)
                    if (listaTag[i].tag == tag)
                        return i;

            return -1;
        }
        
        public static bool TagExist(List<TagObject> listaTag, string tag)
        {
            for (int i = 0; i < listaTag.Count; i++)
                if (listaTag[i].tag == tag)
                    return true;

            return false;
        }

    }
}
