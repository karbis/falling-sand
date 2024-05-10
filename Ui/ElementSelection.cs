using falling_sand.Ui;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace falling_sand {
    public partial class ElementSelection : UserControl {
        public Element Element;
        public static List<ElementSelection> Elements = new List<ElementSelection>();
        public static ToolTip ToolTip = new ToolTip();
        public ElementSelection(Element element) {
            Element = element;
            InitializeComponent();
            Elements.Add(this);
            UpdateVisual();

            //Ui.Game.PaintElement()
            SetPaintToElement(element, Visual);

            foreach (Control control in (Control[])[this, Visual, Border]) {
                control.MouseClick += (object? sender, MouseEventArgs e) => {
                    if (e.Button != MouseButtons.Left) return;
                    Game.SelectedElement = Element;
                    UpdateAll();
                };
                Game.DoubleBufferPanel(control);
            }

            ToolTip.SetToolTip(Visual, element.Name);
        }

        public static PaintEventHandler SetPaintToElement(Element element, Control control) {
            PaintEventHandler handler = (object? sender, PaintEventArgs e) => {
                for (int x = 0; x < 8; x++) {
                    for (int y = 0; y < 8; y++) {
                        Ui.Game.PaintElement(e.Graphics, element, control.Width / 8d, x, y);
                    }
                }
            };
            control.Paint += handler;
            return handler;
        }

        public void UpdateVisual() {
            //Background
            Border.Size = (Element == Game.SelectedElement) ? new Size(38,38) : new Size(36,36);
            Border.Location = (Element == Game.SelectedElement) ? new Point(1, 1) : new Point(2, 2);
        }
        public static void UpdateAll() {
            foreach (ElementSelection v in Elements) {
                v.UpdateVisual();
            }
            SelectedDisplay.Update();
        }
    }
}
