using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace falling_sand.Ui {
    public class ElementSelectorBar {
        public static void FillElements(Panel bar) {
            for (int i = 0; i < ListOfElements.List.Count; i++) {
                Element elem = ListOfElements.List[i];
                int x = i % 7;
                int y = i / 7;

                ElementSelection control = new ElementSelection(elem);
                control.Location = new Point(x*40, y*40);
                bar.Controls.Add(control);
            }
            Game.DoubleBufferPanel(bar);
        }
    }
}
