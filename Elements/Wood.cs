using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace falling_sand.Elements {
    public class Wood : Element {
        public Wood() {
            Flammable = true;
            ElementImage = "Wood";
            SelectionBarOrder = 9;
        }
    }
}
