using falling_sand.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace falling_sand.Ui {
    internal class Buttons {
        static FallingSand form = FallingSand.Form;
        static int[] brushSizes = [1, 3, 5];
        static int brushIndex = 0;
        public static void Init() {
            IncrementBrushSize(0);
            form.BrushSizeButton.MouseDown += (object? handler, MouseEventArgs e) => {
                if (e.Button == MouseButtons.Left) {
                    IncrementBrushSize(1);
                } else if (e.Button == MouseButtons.Right) {
                    IncrementBrushSize(-1);
                }
                form.Canvas.Focus();
            };

            form.ToolButton.MouseClick += (object? handler, MouseEventArgs e) => {
                bool usingEraser = BrushGame.Tool == GameTool.Brush; // *will use eraser
                BrushGame.Tool = usingEraser ? GameTool.Eraser : GameTool.Brush;
                form.ToolButton.Image = usingEraser ? Resources.EraserIcon : Resources.BrushIcon;
                form.ToolButton.Text = "Tool: " + BrushGame.Tool.ToString();
                IncrementBrushSize(0);
                form.Canvas.Focus();
            };

            form.ClearButton.MouseClick += (object? handler, MouseEventArgs e) => {
                Game.FillSpatialMap();
                form.Canvas.Focus();
            };

            form.PlayButton.MouseClick += (object? handler, MouseEventArgs e) => {
                Game.Paused = !Game.Paused;
                form.PlayButton.Image = (!Game.Paused) ? Resources.PauseIcon : Resources.PlayIcon;
                form.PlayButton.Text = (Game.Paused) ? "Play   " : "Pause";
                form.FPSCounter.Visible = !Game.Paused;

                form.Canvas.Focus();
            };
        }

        public static void IncrementBrushSize(int amount = 0) {
            brushIndex += amount;
            if (brushIndex >= brushSizes.Length) {
                brushIndex = 0;
            } else if (brushIndex < 0) {
                brushIndex = brushSizes.Length - 1;
            }
            BrushGame.BrushSize = brushSizes[brushIndex];
            form.BrushSizeButton.Text = BrushGame.Tool.ToString() + " Size: " + BrushGame.BrushSize + "x" + BrushGame.BrushSize;
        }
    }
}
