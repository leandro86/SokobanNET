using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SokobanNET
{
    public class Element
    {
        public ElementType Type { get; set; }
        public int Column { get; set;}
        public int Row { get; set; }
    }
}
