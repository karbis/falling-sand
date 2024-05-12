using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace falling_sand.Elements {
    public class Steam : Element {
        public Steam() {
            SelectionBarOrder = 12;
            Description = "Elevates up";
            ElementColor = Color.FromArgb(235, 235, 235);
        }

        public override void Update() {
            base.Update();
            if (IsWallFrom(0,-1)) {
                Destroy();
                return;
            }
            Element? elemAbove = GetElementFrom(0, -1);
            if (elemAbove != null && elemAbove.Gravity && !elemAbove.ignoreLiquidUpdates) {
                Y -= 1;
                elemAbove.Y += 1;
                elemAbove.ignoreLiquidUpdates = true;
                updateSpatialMap(X, Y);
                elemAbove.updateSpatialMap(elemAbove.X, elemAbove.Y);
                return;
            }

            if (IsEmptySpaceFrom(0,-1)) {
                Move(0, -1);
            }
        }
    }
}
