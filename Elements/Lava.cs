using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace falling_sand.Elements {
    public class Lava : Element {
        public Lava() {
            SelectionBarOrder = 11;
            ElementColorFunction = GenerateColorMap(1474, .8, Color.FromArgb(255, 80, 0), Color.FromArgb(255, 110, 50));
            Description = "Generates stone once interacted with water";
            Liquid = Gravity = true;
            Hot = true;
        }
        public override void Update() {
            foreach (int[] n in Ui.Game.SideNeighbors) {
                Element? elem = GetElementFrom(n[0], n[1]);
                if (elem == null) continue;
                if (elem.Name == "Water") {
                    elem.Destroy();
                    Stone stone = new Stone();
                    stone.X = elem.X;
                    stone.Y = elem.Y;
                    continue;
                }
                if ((!elem.Flammable && !elem.Gravity) || elem.Name == Name) continue;
                elem.Destroy();
                Fire fire = new Fire();
                fire.X = elem.X;
                fire.Y = elem.Y;
            }
            base.Update();
        }
    }
}
