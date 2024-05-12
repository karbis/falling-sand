using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace falling_sand.Elements {
    public class Gunpowder : Element {
        public Gunpowder() {
            Flammable = true;
            ElementColorFunction = GenerateColorMap(608607436, .8, Color.FromArgb(100, 100, 100), Color.FromArgb(80, 80, 80));
            SelectionBarOrder = 8;
        }
        int[][] cornerNeighbors = [[1, 1], [-1, -1], [-1, 1], [1, -1]];
        public override void Update() {
            base.Update();
            foreach (int[] n in cornerNeighbors) {
                Element? elem = GetElementFrom(n[0], n[1]);
                if (elem == null) continue;
                if (!elem.Hot) continue;
                Destroy();
                Fire fire = new Fire();
                fire.X = X;
                fire.Y = Y;
                break;
            }
        }
    }
}
