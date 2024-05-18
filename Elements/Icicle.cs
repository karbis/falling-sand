using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace falling_sand.Elements {
    public class Icicle : Element {
        public Icicle() {
            SelectionBarOrder = 13;
            Description = "Shatters upon touching the ground";
            ElementImage = "Icicle";
            Gravity = true;
            Flammable = true;
        }
        bool falling = false;
        public override void Update() {
            //base.Update();
            Element? aboveElement = GetElementFrom(0, -1);
            if ((aboveElement == null || aboveElement.Gravity) && !falling) {
                falling = true;
                return;
            }
            if (!falling) return;
            Element? underElem = GetElementFrom(0, 2);
            if (isFalling()) {
                if (underElem != null && underElem.Liquid) {
                    ignoreLiquidUpdates = true;
                }
                Move(0, 1);
                return;
            }
            if (IsTouchingHotElement()) return;
            Destroy();
            if (!IsEmptySpaceFrom(0, -1)) return;
            void createWater(int x, int y) {
                Water water = new Water();
                water.X = X + x;
                water.Y = Y + y;
            }
            if (IsEmptySpaceFrom(1, -1)) {
                createWater(1, -1);
            }
            if (IsEmptySpaceFrom(-1, -1)) {
                createWater(-1, -1);
            }
            createWater(0, -1);
        }
    }
}