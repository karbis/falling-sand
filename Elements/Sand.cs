using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace falling_sand.Elements {
    public class Sand : Element {

        public Sand() {
            ElementColor = Color.Yellow;
            Gravity = true;
            Flammable = true;
            SelectionBarOrder = 0;
            ElementColorFunction = Ui.Game.GenerateColorMap(5476, .85, Color.Yellow, Color.FromArgb(255, 235, 0));
        }
    }
}
