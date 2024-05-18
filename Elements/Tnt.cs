using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace falling_sand.Elements {
    public class Tnt : Element {
        public Tnt() {
            SelectionBarOrder = 7;
            ElementImage = "Tnt";
            Description = "Explodes when interacted with fire";
            Name = "TNT";
        }
        int explodeTimer = 0;
        int innerTimer = 0;
        List<int> alreadyVisited = [];
        List<int> blacklistedAreas = [];
        Ui.Game.EmptyFunction? updateHook;
        public override void Update() {
            base.Update();
            if (IsTouchingHotElement()) {
                Destroy();
                updateHook = new Ui.Game.EmptyFunction(updateTick);
                Ui.Game.OnUpdateHooks.Add(updateHook);
            }
        }
        void updateTick() {
            innerTimer++;
            if (innerTimer % 2 == 0) return;
            if (explodeTimer > 4 && updateHook != null) {
                Ui.Game.OnUpdateHooks.Remove(updateHook);
                return;
            }
            for (int x = -explodeTimer; x <= explodeTimer; x++) {
                for (int y = -explodeTimer; y <= explodeTimer; y++) {
                    if (IsWallFrom(x, y)) continue;
                    if (alreadyVisited.Contains(x + y * 15)) continue;
                    alreadyVisited.Add(x + y * 15);

                    bool shouldContinue = false;
                    for (int i = 1; i <= explodeTimer; i++) {
                        int x1 = (int)((double)x / explodeTimer * i);
                        int y1 = (int)((double)y / explodeTimer * i);
                        Element? elem = GetElementFrom(x1, y1);
                        if (!blacklistedAreas.Contains(x1 + y1 * 15) && !IsWallFrom(x1, y1) && (elem == null || elem.Name == "Fire")) continue;
                        if (elem != null && !elem.Invincible && !elem.Liquid) {
                            elem.Destroy();
                            blacklistedAreas.Add(x1 + y1 * 15);
                        }
                        shouldContinue = true; // bootleg raycast
                        break;
                    }
                    if (shouldContinue) continue;
                    Fire fire = new Fire();
                    fire.X = X + x;
                    fire.Y = Y + y;
                }
            }


            explodeTimer++;
        }
    }
}
