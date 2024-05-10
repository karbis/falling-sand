using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace falling_sand.Elements {
    public class Plant : Element {
        public Plant() {
            ElementColor = Color.FromArgb(65, 190, 50);
            Flammable = true;
            SelectionBarOrder = 5;
            Description = "Grows when interacted with water";
        }
        bool isGrown = false;

        public override void Update() {
            base.Update();
            if (isGrown) return;
            foreach (int[] neighborPair in Ui.Game.SideNeighbors) {
                Element? elem = GetElementFrom(neighborPair[0], neighborPair[1]);
                if (elem == null) continue;
                if (elem.Name != "Water") continue;
                isGrown = true;
                ElementColor = Color.FromArgb(50, 165, 40);
                elem.Destroy();
                if (!IsEmptySpaceFrom(0, -1)) break;
                Plant plant = new Plant();
                plant.X = X;
                plant.Y = Y - 1;
                break;
            }
        }
    }
}
