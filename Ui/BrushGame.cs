using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Windows.Forms.Timer;

namespace falling_sand.Ui {
    public enum GameTool {
        Brush,
        Eraser
    }

    public class BrushGame {
        static Panel input = FallingSand.Form.CanvasInput;
        static public int BrushSize = 1;
        static Point currentMousePos = new Point();
        static public GameTool Tool = GameTool.Brush;
        const bool brushReplaces = true;

        public static void Init() {
            Timer updateTimer = new Timer();
            updateTimer.Interval = 1000/20;
            updateTimer.Start();
            Game.DoubleBufferPanel(input);
            bool mouseDown = false;

            updateTimer.Tick += (object? sender, EventArgs e) => {
                if (!IsMouseInCanvas()) return;
                if (!mouseDown) return;
                Point pos = GetHoveringPoint(currentMousePos);
                for (int x = 0; x < BrushSize; x++) {
                    for (int y = 0; y < BrushSize; y++) {
                        Element? erasedElem = Element.GetElementByCoords(pos.X + x, pos.Y + y);
                        if (Tool == GameTool.Brush && !Element.IsEmptySpace(pos.X + x, pos.Y + y)) {
                            if (!brushReplaces) continue;
                            if (erasedElem == null) continue; // that means its a wall
                            erasedElem.Destroy();
                        }
                        if (Tool == GameTool.Eraser) {
                            if (erasedElem == null) continue;
                            erasedElem.Destroy();
                            continue;
                        }
                        Element? elem = (Element?)Activator.CreateInstance(Game.SelectedElement.GetType());
                        if (elem == null) return;
                        elem.X = pos.X+x;
                        elem.Y = pos.Y+y;
                    }
                }
            };

            input.MouseDown += (object? sender, MouseEventArgs e) => {
                if (e.Button != MouseButtons.Left) return;
                mouseDown = true;
            };
            input.MouseUp += (object? sender, MouseEventArgs e) => {
                if (e.Button != MouseButtons.Left) return;
                mouseDown = false;
            };
            Point lastCoord = new Point(-1, -1);
            long lastUpdate = 0;
            input.MouseMove += (object? sender, MouseEventArgs e) => {
                currentMousePos = e.Location;
                long now = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                if (now == 0 || now - lastUpdate < 32) return; // prevent it from being updated more than needed
                lastUpdate = now;
                Point hoverPoint = GetHoveringPoint(currentMousePos);
                if (lastCoord == hoverPoint) return;
                input.Invalidate();
                lastCoord = hoverPoint;
            };
            input.Paint += (object? sender, PaintEventArgs e) => {
                //e.Graphics.Clear(Color.Transparent);
                if (!IsMouseInCanvas()) return;
                Pen pen = new Pen(Color.FromKnownColor(KnownColor.Control));
                double coordsPerUnit = Game.GetCoordsPerUnit();
                Point hoverPoint = GetHoveringPoint(currentMousePos);
                Rectangle drawRect = new Rectangle(new Point((int)(hoverPoint.X*coordsPerUnit), (int)(hoverPoint.Y * coordsPerUnit)), Size.Empty);
                drawRect.Width = (int)(coordsPerUnit * BrushSize);
                drawRect.Height = (int)(coordsPerUnit * BrushSize);
                e.Graphics.DrawRectangle(pen, drawRect);
                pen.Dispose();
            };
        }

        public static Point GetHoveringPoint(Point loc) {
            double coordsPerUnit = Game.GetCoordsPerUnit();
            int x = (int)(loc.X / coordsPerUnit);
            int y = (int)(loc.Y / coordsPerUnit);
            return new Point(x - BrushSize/2, y-BrushSize/2);
        }
        static bool IsMouseInCanvas() {
            Point realMousePos = new Point(Cursor.Position.X - 8 - FallingSand.Form.DesktopLocation.X, Cursor.Position.Y - 31 - FallingSand.Form.DesktopLocation.Y);
            return new Rectangle(Game.Canvas.Location, Game.Canvas.Size).Contains(realMousePos);
        }
    }
}
