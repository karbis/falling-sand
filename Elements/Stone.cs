using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace falling_sand.Elements {
    public class Stone : Element {
        public Stone() {
            ElementColor = Color.Gray;
            SelectionBarOrder = 1;
        }
    }
}
