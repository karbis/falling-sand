using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace falling_sand.Elements {
    public class Ice : Element {
        public Ice() {
            SelectionBarOrder = 10;
            ElementImage = "Ice";
            Description = "Melts when interacted with fire";
        }
        public override void Update() {
            base.Update();
            if (IsTouchingHotElement()) {
                Destroy();
                Water water = new Water();
                water.X = X;
                water.Y = Y;
            }
        }
    }
}
