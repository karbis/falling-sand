using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace falling_sand.Elements {
    public class Water : Element {
        public Water() {
            Liquid = Gravity = true;
            ElementColor = Color.FromArgb(30, 160, 240);
            SelectionBarOrder = 4;
            ElementColorFunction = GenerateColorMap(84736, .9, Color.FromArgb(30, 160, 240), Color.FromArgb(20, 150, 220));
        }
        public override void Update() {
            base.Update();
            foreach (int[] n in Ui.Game.SideNeighbors) {
                Element? elem = GetElementFrom(n[0], n[1]);
                if (elem == null || elem.Name != "Fire") continue;
                Destroy();
                elem.Destroy();
                Steam steam = new Steam();
                steam.X = X;
                steam.Y = Y;
                break;
            }
        }
    }
}
