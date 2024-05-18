using falling_sand.Ui;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace falling_sand.Elements {
    public class BlackHole : Element {
        public BlackHole() {
            Name = "Black Hole";
            SelectionBarOrder = 19;
            Description = "Destroys anything around it";
            ElementImage = "BlackHole";
        }
        int[][] layerOne = [[1, 0], [0, 1], [-1, 0], [0, -1], [1, 1], [1, -1], [-1, 1], [-1, -1]];
        int[][] layerTwo = [[-2, -2], [-1, -2], [0, -2], [1, -2], [2, -2], [-2, 2], [-1, 2], [0, 2], [1, 2], [2, 2], [-2, -1], [-2, 0], [-2, 1], [2, -1], [2, 0], [2, 1]];
        List<Element> toDrag = [];
        public override void Update() {
            base.Update();
            foreach (int[] n in layerOne) {
                Element? elem = GetElementFrom(n[0], n[1]);
                if (elem == null) continue;
                if (elem.Invincible || elem.Name == Name) continue;
                elem.Destroy();
            }
            foreach (Element elem in toDrag) {
                int curX = elem.X;
                int curY = elem.Y;
                elem.Move(Math.Clamp(X - elem.X, -1, 1), Math.Clamp(Y - elem.Y, -1, 1));
                if (curX == elem.X && curY == elem.Y && ListOfElements.List[elem.SelectionBarOrder].Gravity) {
                    elem.Move(0, 1);
                }
            }
            toDrag.Clear();
            foreach (int[] n in layerTwo) {
                Element? elem = GetElementFrom(n[0], n[1]);
                if (elem == null) continue;
                if (elem.Invincible || elem.Name == Name) continue;
                if (toDrag.Contains(elem)) continue;
                toDrag.Add(elem);
                if (!elem.Gravity) return;

                elem.Gravity = false;
                bool canContinue = false;
                Game.EmptyFunction updateHook = () => { };
                updateHook = () => {
                    if (!canContinue) {
                        canContinue = true;
                        return;
                    }
                    elem.Gravity = true;
                    Game.OnUpdateHooks.Remove(updateHook);
                };
                Game.OnUpdateHooks.Add(updateHook);
            }
        }
    }
}
