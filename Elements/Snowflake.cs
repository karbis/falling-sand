using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace falling_sand.Elements {
    public class Snowflake : Element {
        public Snowflake() {
            Flammable = true;
            Gravity = true;
            ElementImage = "Snowflake";
            SelectionBarOrder = 20;
            timer = (byte)Random.Shared.NextInt64(0, 3);
        }
        byte timer = 0;
        const byte speed = 4;
        public override void Update() {
            base.Update();
            if (!isFalling()) return;
            timer++;
            if (timer == 2*speed || timer == 3*speed) {
                Move(1, 0);
            } else if (timer == 1*speed || timer == 4*speed) {
                Move(-1, 0);
            }
            if (timer == 4*speed) {
                timer = 0;
            }
        }
    }
}
