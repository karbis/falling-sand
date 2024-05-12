using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace falling_sand.Elements {
    public class Fire : Element {
        public Fire() {
            ElementColor = Color.OrangeRed;
            SelectionBarOrder = 2;
            ElementColorFunction = GenerateColorMap(7163, .6, Color.OrangeRed, Color.Red);
            Hot = true;
        }
        int Timer = 0;
        public override void Update() {
            base.Update();
            Timer++;
            if (Timer == 3) {
                Destroy();
                return;
            }
            if (Timer != 1) return;
            foreach (int[] neighborPair in Ui.Game.SideNeighbors) {
                if (IsWallFrom(neighborPair[0], neighborPair[1])) continue;
                Element? elem = GetElementFrom(neighborPair[0], neighborPair[1]);
                if (elem == null) continue;
                if (!elem.Flammable) continue;
                elem.Destroy();
                Fire fire = new Fire();
                fire.X = X + neighborPair[0];
                fire.Y = Y + neighborPair[1];
            }
        }
    }
}
