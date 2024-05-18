using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace falling_sand.Elements {
    public class Termite : Element {
        public Termite() {
            Gravity = true;
            Flammable = true;
            Description = "Eats wood";
            moveDir = (sbyte)((Random.Shared.NextDouble() > .5) ? -1 : 1);
            SelectionBarOrder = 15;
            ElementImage = "Termite";
        }
        byte timer = 0;
        sbyte moveDir = 1;

        public override void Update() {
            base.Update();
            
            Element? aboveElement = GetElementFrom(0, -1);
            if (aboveElement != null && aboveElement.Gravity && aboveElement.Name != Name) {
                // death
                Destroy();
                return;
            }

            if (isFalling()) {
                timer = 0;
                return;
            };
            if (timer == 20) { // 0.666..s
                timer = 0;
                Element? wood = GetElementOfType("Wood", 0, 1) ?? GetElementOfType("Wood", moveDir, 0);
                if (wood != null) {
                    wood.Destroy();
                } else if (IsEmptySpaceFrom(moveDir, 0) && IsEmptySpaceFrom(moveDir, 1) && !IsEmptySpaceFrom(moveDir, 2)) {
                    Move(moveDir, 1);
                } else if (!IsEmptySpaceFrom(moveDir, 0) && IsEmptySpaceFrom(0, -1) && IsEmptySpaceFrom(moveDir, -1)) {
                    Move(moveDir, -1);
                } else if (IsEmptySpaceFrom(moveDir, 1) || !IsEmptySpaceFrom(moveDir,0)) {
                    moveDir *= -1;
                } else {
                    Move(moveDir, 0);
                }
            }

            timer++;
        }
    }
}
