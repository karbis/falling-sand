using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace falling_sand.Ui {
    public partial class SettingsForm : Form {
        public SettingsForm() {
            InitializeComponent();
            int originalSpeed = Game.TickRate;
            Size originalSize = Game.GameSize;

            Cancel.MouseClick += (object? handler, MouseEventArgs e) => {
                Game.TickRate = originalSpeed;
                Close();
            };
            Confirm.MouseClick += (object? handler, MouseEventArgs e) => {
                Game.GameSize = new Size((int)GameSizeWidth.Value, (int)GameSizeHeight.Value);
                if (originalSize != Game.GameSize) {
                    Game.FillSpatialMap();
                    Resizer.UpdateSizes(); // for some reason this doesnt do anything if u havent resized the window first, i have no idea
                }
                Close();
            };

            SpeedInput.ValueChanged += (object? handler, EventArgs e) => {
                Game.TickRate = (int)(30*SpeedInput.Value);
                updateSpeedBar();
            };
            SpeedBar.ValueChanged += (object? handler, EventArgs e) => {
                if ((Game.TickRate > 4 * 30 || Game.TickRate < 7) && (SpeedBar.Value == SpeedBar.Maximum || SpeedBar.Value == SpeedBar.Minimum)) return;
                SpeedInput.Value = (decimal)(SpeedBar.Value / 100d);
                Game.TickRate = (int)(SpeedBar.Value / 100d * 30);
            };

            GameSizeWidth.Value = Game.GameSize.Width;
            GameSizeHeight.Value = Game.GameSize.Height;

            updateSpeedBar();
            SpeedInput.Value = (decimal)(Game.TickRate / 30d);
        }

        void updateSpeedBar() {
            SpeedBar.Value = Math.Clamp((int)(Game.TickRate / 30d * 100), SpeedBar.Minimum, SpeedBar.Maximum);
        }
    }
}
