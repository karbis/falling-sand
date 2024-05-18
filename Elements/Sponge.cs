using falling_sand.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace falling_sand.Elements {
    public class Sponge : Element {
        public Sponge() {
            SelectionBarOrder = 17;
            Flammable = true;
            Description = "Absorbs 8 water tiles and becomes wet after";
            ElementImage = "Sponge";
        }
        byte absorbedWater = 0;
        const byte capacity = 8; // 3^2-1
        Element? toDestroy;
        public override void Update() {
            if (toDestroy != null) {
                toDestroy.Destroy();
                toDestroy = null;
            }
            base.Update();
            if (absorbedWater == capacity) return;
            Element? elem = GetElementOfType("Water", 0, -1);
            if (elem == null) return;
            absorbedWater++;
            elem.ignoreLiquidUpdates = true;
            toDestroy = elem;
            if (absorbedWater != capacity) return;
            ElementImage = "WetSponge";
        }
    }
}
