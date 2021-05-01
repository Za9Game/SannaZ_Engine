using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SannaZ_Engine
{
    public class TagObject
    {
        public int key { get; set; }
        public string tag { get; set; }

        public TagObject() {  }

        public TagObject(int key, string tag)
        {
            this.key = key;
            this.tag = tag;
        }

        
    }
}
