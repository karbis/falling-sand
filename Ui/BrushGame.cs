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
        public static List<List<Element>> UndoHistory = [];
        public static List<Element>? LastWaypoint;
        public static int UndoIndex = 0;

        public static void Init() {
            Timer updateTimer = new Timer();
            updateTimer.Interval = 1;
            updateTimer.Start();
            Game.DoubleBufferPanel(input);
            bool mouseDown = false;

            updateTimer.Tick += (object? sender, EventArgs e) => {
                if (!IsMouseInCanvas()) return;
                if (!mouseDown) return;
                Point pos = GetHoveringPoint(currentMousePos);
                for (int x = 0; x < BrushSize; x++) {
                    for (int y = 0; y < BrushSize; y++) {
                        //if (Element.IsWall(pos.X + x, pos.Y + y)) continue;
                        Element? erasedElem = Element.GetElementByCoords(pos.X + x, pos.Y + y);
                        if (Tool == GameTool.Brush && !Element.IsEmptySpace(pos.X + x, pos.Y + y)) {
                            if (!brushReplaces) continue;
                            if (erasedElem == null) continue;
                            erasedElem.Destroy();
                            if (LastWaypoint != null) {
                                LastWaypoint.Remove(erasedElem);
                            }
                        }
                        if (Tool == GameTool.Eraser) {
                            if (erasedElem == null) continue;
                            erasedElem.Destroy();
                            if (LastWaypoint != null) {
                                LastWaypoint.Add(erasedElem);
                            }
                            continue;
                        }
                        Element? elem = (Element?)Activator.CreateInstance(Game.SelectedElement.GetType());
                        if (elem == null) return;
                        elem.X = pos.X + x;
                        elem.Y = pos.Y + y;
                        if (LastWaypoint == null) continue;
                        LastWaypoint.Add(elem);
                    }
                }
            };

            input.MouseDown += (object? sender, MouseEventArgs e) => {
                if (e.Button != MouseButtons.Left) return;
                mouseDown = true;
                LastWaypoint = new List<Element>();
            };
            input.MouseUp += (object? sender, MouseEventArgs e) => {
                if (e.Button != MouseButtons.Left) return;
                mouseDown = false;

                if (LastWaypoint == null) return;
                if (LastWaypoint.Count == 0) return;
                if (UndoIndex != 0) {
                    UndoHistory.RemoveRange(0, UndoIndex);
                }
                UndoIndex = 0;
                if (UndoHistory.Count > 75) {
                    UndoHistory.RemoveAt(75);
                }
                UndoHistory.Insert(0, LastWaypoint);
                LastWaypoint = null;
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
                Game.Canvas.Invalidate();
                lastCoord = hoverPoint;
            };

            FallingSand.Form.KeyPreview = true;
            FallingSand.Form.KeyDown += (object? handler, KeyEventArgs e) => {
                if (!e.Control) return;
                if (e.KeyCode == Keys.Z) {
                    undo(1);
                } else if (e.KeyCode == Keys.Y) {
                    undo(-1);
                }
            };
        }

        public static Point GetHoveringPoint(Point loc) {
            double coordsPerUnit = Game.GetCoordsPerUnit();
            int x = (int)(loc.X / coordsPerUnit);
            int y = (int)(loc.Y / coordsPerUnit);
            return new Point(x - BrushSize / 2, y - BrushSize / 2);
        }
        static bool IsMouseInCanvas() {
            Point realMousePos = new Point(Cursor.Position.X - 8 - FallingSand.Form.DesktopLocation.X, Cursor.Position.Y - 31 - FallingSand.Form.DesktopLocation.Y);
            return new Rectangle(Game.Canvas.Location, Game.Canvas.Size).Contains(realMousePos);
        }
        public static void PaintOnBrush(PaintEventArgs e) {
            if (!IsMouseInCanvas()) return;
            Pen pen = new Pen(Color.FromKnownColor(KnownColor.Control));
            float coordsPerUnit = Game.GetCoordsPerUnit();
            Point hoverPoint = GetHoveringPoint(currentMousePos);
            RectangleF drawRect = new RectangleF(new PointF(hoverPoint.X * coordsPerUnit+.5f, hoverPoint.Y * coordsPerUnit+.5f), Size.Empty);
            drawRect.Width = coordsPerUnit * BrushSize - .5f;
            drawRect.Height = coordsPerUnit * BrushSize - .5f;
            e.Graphics.DrawRectangle(pen, drawRect);
            pen.Dispose();
        }

        static void undo(int amount) {
            if (UndoIndex + amount > UndoHistory.Count || UndoIndex + amount < 0) return;
            List<Element> waypoint = UndoHistory[UndoIndex + Math.Min(0,amount)];
            for (int i = 0; i < waypoint.Count; i++) {
                Element elem = waypoint[i];
                if (!elem.isDestroyed) {
                    elem.Destroy();
                    continue;
                }
                Element? elemClone = (Element?)Activator.CreateInstance(elem.GetType());
                if (elemClone == null) continue;
                elemClone.X = elem.X;
                elemClone.Y = elem.Y;
                waypoint[i] = elemClone;
            }
            UndoIndex += amount;
        }
    }
}
