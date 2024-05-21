using falling_sand.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Windows.Forms.Timer;

namespace falling_sand.Ui {
    internal class Game {
        public static Element SelectedElement = ListOfElements.List[0];
        //public static Element[] State = [];
        public static Size GameSize = new Size(30,30);
        public static bool Paused = false;
        public static int TickRate = 30;
        public delegate void EmptyFunction();
        public static List<EmptyFunction> OnUpdateHooks = [];

        static Dictionary<string, Bitmap> images = new Dictionary<string, Bitmap> {
            { "Ice", Resources.IceElement },
            { "Icicle", Resources.IcicleElement },
            { "Tnt", Resources.TntElement },
            { "Stone", Resources.StoneElement },
            { "Wood", Resources.WoodElement },
            {"Obsidian", Resources.ObsidianElement },
            {"Termite", Resources.TermiteElement },
            {"Sponge", Resources.SpongeElement },
            {"WetSponge", Resources.WetSpongeElement },
            {"BlackHole", Resources.BlackHoleElement },
            {"Snowflake", Resources.SnowflakeElement },
            {"Candle", Resources.CandleElement }
        };
        public static void PaintElement(Graphics g, Element element, float scale = 16, int x = 0, int y = 0) {
            RectangleF pixelRect = new RectangleF(x * scale, y * scale, scale, scale);
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

            if (element.ElementImage != null) {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.DrawImage(images[element.ElementImage], pixelRect);
                return;
            }
            SolidBrush brush = new SolidBrush((element.ElementColorFunction != null) ? element.ElementColorFunction(x, y) : element.ElementColor);
            g.FillRectangle(brush, pixelRect);
            brush.Dispose();
        }
        public static void DoubleBufferPanel(Control panel) {
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty
                | BindingFlags.Instance | BindingFlags.NonPublic, null,
                panel, new object[] { true });
        }

        public static Point GetPosFromGlobal(int global) {
            return new Point(global % GameSize.Width, global / GameSize.Height);
        }
        public static int GetGlobalFromPos(int x, int y) {
            return x + y * GameSize.Width;
        }

        public static Control Canvas = FallingSand.Form.Canvas;
        public static void Init() {
            SelectedElement = ListOfElements.List[0]; // i dont know
            FillSpatialMap();
            Canvas.Paint += (object? sender, PaintEventArgs e) => {
                //e.Graphics.Clear(Color.Black);
                //e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                foreach (Element? elem in Element.SpatialMap) {
                    if (elem == null) continue;
                    PaintElement(e.Graphics, elem, (float)Canvas.Width / GameSize.Width, elem.X, elem.Y);
                }
                BrushGame.PaintOnBrush(e);
            };
            DoubleBufferPanel(Canvas);

            int fps = 0;

            Timer timer = new Timer();
            timer.Interval = 1;
            timer.Start();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            long lastElapsed = stopwatch.ElapsedMilliseconds;
            double leftOverSeconds = 0;
            timer.Tick += (object? sender, EventArgs e) => {
                if (stopwatch.ElapsedMilliseconds - lastElapsed + leftOverSeconds < 1000d / TickRate) return;
                leftOverSeconds += stopwatch.ElapsedMilliseconds - lastElapsed;
                lastElapsed = stopwatch.ElapsedMilliseconds;
                if (Paused) {
                    Canvas.Invalidate();
                    leftOverSeconds = 0;
                    return;
                }

                int updateAmount = (int)(leftOverSeconds / 1000d * TickRate);
                updateAmount = Math.Min(10, updateAmount);
                if (updateAmount == 0) return;

                fps += Math.Min(1, updateAmount);

                Timer fpsTimer = new Timer();
                fpsTimer.Interval = 1000;
                fpsTimer.Start();
                fpsTimer.Tick += (object? sender, EventArgs e) => {
                    if (Paused) return;
                    fps -= Math.Min(1, updateAmount);
                    FallingSand.Form.FPSCounter.Text = "TPS: " + fps;
                    fpsTimer.Dispose();
                };
                FallingSand.Form.FPSCounter.Text = "TPS: " + fps;

                leftOverSeconds -= 1000d / TickRate * updateAmount;
                Update(updateAmount);
                if (leftOverSeconds > 500) {
                    // too much time, reset
                    leftOverSeconds = 0;
                }
            };
        }
        public static void Update(int updateAmount = 1) {
            foreach (Element? elem in (Element?[])Element.SpatialMap.Clone()) {
                if (elem == null) continue;
                for (int i = 0; i < updateAmount; i++) {
                    if (elem == null || elem.isDestroyed) break;
                    elem.Update();
                }
            }
            for (int i = 0; i < updateAmount; i++) {
                if (OnUpdateHooks.Count == 0) break;
                EmptyFunction[] hooks = new EmptyFunction[OnUpdateHooks.Count];
                OnUpdateHooks.CopyTo(hooks);
                foreach (EmptyFunction func in hooks) {
                    func();
                }
            }
            Canvas.Invalidate();
        }

        public static void FillSpatialMap() {
            Array.Clear(Element.SpatialMap);
            Element.SpatialMap = new Element?[GameSize.Width*GameSize.Height];
            OnUpdateHooks.Clear();
            OnUpdateHooks.TrimExcess();
        }
        public static float GetCoordsPerUnit() {
            return (float)Canvas.Width / GameSize.Width;
        }
        public static readonly int[][] SideNeighbors = [[1,0],[0,1],[-1,0],[0,-1]];
    }
}
