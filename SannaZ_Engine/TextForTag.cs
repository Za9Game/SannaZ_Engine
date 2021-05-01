using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SannaZ_Engine
{
    public static class TextForTag
    {
        public static string Update(string text, TagObject tagHud)
        {
            switch (tagHud.tag)
            {
                case "Score":
                    text = "Score\n" + " " + Global.score.score.ToString();
                    break;

                case "Timer":
                    text = "Time\n" + "" + Global.score.time.ToString();
                    break;

                case "World":
                    text = "World\n" + " 1-1";
                    break;

                case "Coins":
                    text = "Coins\n" + "  " + Global.score.coins.ToString();
                    break;
            }
            return text;
        }
    }
}
