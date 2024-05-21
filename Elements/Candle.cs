using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace falling_sand.Elements {
    public class Candle : Element {
        public Candle() {
            SelectionBarOrder = 21;
            Description = "Turns into a fire source once lit";
            ElementImage = "Candle";
        }
        bool lit = false;
        public override void Update() {
            base.Update();
            Element? elem = GetElementFrom(0, -1);
            if (!lit) {
                lit = elem != null && elem.Hot;
                if (!lit) return;
            }

            if (elem != null && elem.Flammable) {
                elem.Destroy();
                elem = null;
            }

            if (elem == null) {
                Fire fire = new Fire();
                fire.X = X;
                fire.Y = Y - 1;
            }
        }
    }
}
