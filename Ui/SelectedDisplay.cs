using falling_sand.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace falling_sand.Ui {
    public class SelectedDisplay {
        static FallingSand form = FallingSand.Form;
        static PaintEventHandler? paintHandler;
        static List<Control> appendedControls = [];
        public static void Update() {
            foreach (Control control in appendedControls) {
                control.Dispose();
            }
            appendedControls.Clear();
            Panel display = form.SelectedDisplayPanel;
            if (paintHandler != null) {
                display.Paint -= paintHandler;
            }
            Element selectedElement = Game.SelectedElement;
            paintHandler = ElementSelection.SetPaintToElement(selectedElement, display);
            display.Invalidate();
            Game.DoubleBufferPanel(display);

            form.SelectedName.Text = selectedElement.Name;

            if (selectedElement.Description != null) {
                AddText("•");
                AddText(selectedElement.Description);
            }
            bool[] lookForBools = [selectedElement.Gravity && !selectedElement.Liquid, selectedElement.Flammable, selectedElement.Liquid];
            Bitmap[] lookForIcons = [Resources.GravityIcon, Resources.FlammableIcon, Resources.LiquidIcon];
            string[] lookForNames = ["Gravity","Flammable", "Liquid"];
            bool addedDash = false;
            for (int i = 0; i < lookForBools.Length; i++) {
                if (!lookForBools[i]) continue;
                if (!addedDash) {
                    AddText("•");
                    addedDash = true;
                }
                ElementSelection.ToolTip.SetToolTip(AddImage(lookForIcons[i]), lookForNames[i]);
                if (selectedElement.Description == null) {
                    AddText(lookForNames[i]);
                }
            }
        }

        public static Label AddText(string text = "") {
            Label label = new Label();
            Control lastControl = appendedControls.ElementAtOrDefault(appendedControls.Count-1) ?? form.SelectedName;
            label.Location = new Point(lastControl.Location.X + lastControl.Width - 2, form.SelectedName.Location.Y + 3);
            appendedControls.Add(label);
            label.AutoSize = true;
            label.Text = text;
            form.BottomCenterBar.Controls.Add(label);
            return label;
        }

        public static PictureBox AddImage(Bitmap resource) {
            PictureBox img = new PictureBox();

            img.Image = resource;
            Control lastControl = appendedControls.ElementAtOrDefault(appendedControls.Count - 1) ?? form.SelectedName;
            img.Location = new Point(lastControl.Location.X + lastControl.Width - 3, form.SelectedName.Location.Y + 1);
            img.Width = 20;
            appendedControls.Add(img);
            form.BottomCenterBar.Controls.Add(img);
            
            return img;
        }
    }
}
