using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace falling_sand.Elements {
    public class Obsidian : Element {
        public Obsidian() {
            Invincible = true;
            SelectionBarOrder = 14;
            ElementImage = "Obsidian";
        }
    }
}
