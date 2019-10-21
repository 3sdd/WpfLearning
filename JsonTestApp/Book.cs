using System;
using System.Collections.Generic;
using System.Text;

namespace JsonTestApp
{
    class Book
    {
        public string Title { get; set; }
        public int Price { get; set; }

        public string[] Authors { get; set; }

        public Dictionary<string, string> TestDic { get; set; }
    }
}
