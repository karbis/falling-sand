using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace falling_sand.Elements {
    public class Oil : Element {
        public Oil() {
            SelectionBarOrder = 18;
            Gravity = Liquid = true;
            Description = "Floats on water";
            Flammable = true;
            ElementColor = Color.FromArgb(255, 255, 120);
        }
        public override void Update() {
            base.Update();
            Element? elem = GetElementOfType("Water", 0, -1);
            if (elem == null) return;
            if (elem.ignoreLiquidUpdates) return;
            Y -= 1;
            elem.Y += 1;
            updateSpatialMap(X, Y);
            elem.updateSpatialMap(elem.X, elem.Y);
            elem.ignoreLiquidUpdates = true;
            ignoreLiquidUpdates = true;
        }
    }
}
