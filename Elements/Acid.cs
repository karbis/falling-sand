using falling_sand.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace falling_sand.Elements {
    public class Acid : Element {
        public Acid() {
            SelectionBarOrder = 16;
            Gravity = Liquid = true;
            ElementColorFunction = GenerateColorMap(4616, .9, Color.FromArgb(180, 255, 0), Color.FromArgb(120, 255, 0));
        }
        List<Element> toDestroy = [];
        public override void Update() {
            foreach (Element elem in toDestroy) {
                elem.Destroy();
            }
            toDestroy.Clear();
            foreach (int[] n in Game.SideNeighbors) {
                Element? elem = GetElementFrom(n[0], n[1]);
                if (elem == null) continue;
                if (elem.Invincible || elem.Name == Name || toDestroy.Contains(elem)) continue;
                toDestroy.Add(elem);
                elem.ignoreLiquidUpdates = true;
            }
            base.Update();
        }
    }
}
