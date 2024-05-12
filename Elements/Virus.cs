using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace falling_sand.Elements {
    internal class Virus : Element {
        public Virus() {
            SelectionBarOrder = 6;
            ElementColorFunction = GenerateColorMap(51605, .75, Color.FromArgb(150,150,150), Color.Red);
            Flammable = true;
            Description = "Slowly spreads as time goes on";
        }
        int timer = 0;
        public override void Update() {
            base.Update();
            timer++;
            if (timer < 60) return; // 2 seconds
            timer = 0;
            foreach (int[] neighbor in Ui.Game.SideNeighbors) {
                if (Random.Shared.NextDouble() < .25) continue; // variety
                if (IsWallFrom(neighbor[0], neighbor[1])) continue;
                Element? elem = GetElementFrom(neighbor[0], neighbor[1]);
                if (elem != null) {
                    if (elem.Liquid) {
                        elem.Destroy();
                    } else continue;
                };
                Virus virus = new Virus();
                virus.X = X + neighbor[0];
                virus.Y = Y + neighbor[1];
            }
        }
    }
}
