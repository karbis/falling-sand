using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace falling_sand.Elements {
    public class Water : Element {
        public Water() {
            Liquid = Gravity = true;
            ElementColor = Color.FromArgb(30, 160, 240);
            SelectionBarOrder = 4;
        }
    }
}
