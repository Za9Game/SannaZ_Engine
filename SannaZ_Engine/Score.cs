using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SannaZ_Engine
{
    public class Score
    {
        public int score { get; set; }
        public int time { get; set; }
        public int coins { get; set; }

        public void Initialize()
        {
            score = 0;
            time = 400;
            coins = 0;
        }


    }
}
