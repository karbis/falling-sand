﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Windows.Forms.Timer;

namespace falling_sand.Ui {
    internal class Game {
        public static Element SelectedElement = ListOfElements.List[0];
        //public static Element[] State = [];
        public static Size GameSize = new Size(30,30);
        public static bool Paused = false;

        public static void PaintElement(Graphics g, Element element, double scale = 16, int x = 0, int y = 0) {
            SolidBrush brush = new SolidBrush((element.ElementColorFunction != null) ? element.ElementColorFunction(x, y) : element.ElementColor);
            double xPos = x * scale;
            double yPos = y * scale;
            g.FillRectangle(brush, new Rectangle((int)xPos, (int)yPos, (int)((x+1)*scale)-(int)xPos, (int)((y+1)*scale) - (int)yPos));
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
                    PaintElement(e.Graphics, elem, (double)Canvas.Width / GameSize.Width, elem.X, elem.Y);
                }
                BrushGame.PaintOnBrush(e);
            };
            DoubleBufferPanel(Canvas);

            const int intendedFps = 30;
            int fps = 0;

            Timer timer = new Timer();
            timer.Interval = 1;
            timer.Start();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            long lastElapsed = stopwatch.ElapsedMilliseconds;
            timer.Tick += (object? sender, EventArgs e) => {
                if (stopwatch.ElapsedMilliseconds - lastElapsed < 1000d / intendedFps) return;
                lastElapsed = (long)(Math.Floor(stopwatch.ElapsedMilliseconds/(1000d/intendedFps))*(1000d/intendedFps));
                if (Paused) {
                    Canvas.Invalidate();
                    return;
                }
                // snaps the lastElapsed variable to last frame, to not make it behind (too slow)
                fps++;

                Timer fpsTimer = new Timer();
                fpsTimer.Interval = 1000;
                fpsTimer.Start();
                fpsTimer.Tick += (object? sender, EventArgs e) => {
                    if (Paused) return;
                    fps--;
                    FallingSand.Form.FPSCounter.Text = "TPS: " + fps;
                    fpsTimer.Stop();
                    fpsTimer.Dispose();
                };
                FallingSand.Form.FPSCounter.Text = "TPS: " + fps;

                Update();
            };

            /*Canvas.MouseClick += (object? sender, MouseEventArgs e) => {
                double xScale = e.Location.X / (double)Canvas.Width;
                double yScale = e.Location.Y / (double)Canvas.Height;
                Point clickPoint = new Point((int)(xScale * StateSize.Width), (int)(yScale * StateSize.Height));

                Element? elem = ListOfElements.List.Find(x => x.Name == SelectedElement.Name);
                if (elem == null) return;
                elem = (Element?)Activator.CreateInstance(elem.GetType());
                if (elem == null) return;
                elem.X = clickPoint.X;
                elem.Y = clickPoint.Y;
            };*/

        }
        public static void Update() {
            List<Element?> shallowCopy = [];
            foreach (Element? elem in Element.SpatialMap) {
                shallowCopy.Add(elem);
            }
            foreach (Element? elem in shallowCopy) {
                if (elem == null) continue;
                elem.Update();
            }
            Canvas.Invalidate();
        }

        public static void FillSpatialMap() {
            Element.SpatialMap.Clear();
            Element.SpatialMap = Enumerable.Repeat((Element?)null, GameSize.Width * GameSize.Height).ToList();
        }
        public static double GetCoordsPerUnit() {
            return (double)Canvas.Width / GameSize.Width;
        }

        static Dictionary<int, Color[]> colorMapCache = [];
        public static Element.ColorFunction GenerateColorMap(int seed, double frequency, Color mainColor, Color secondaryColor) {
            const int colorMapSize = 24;
            if (colorMapCache.GetValueOrDefault(seed) != null) {
                return (int x, int y) => colorMapCache[seed][x % colorMapSize + y % colorMapSize * colorMapSize];
            }
            Random rand = new Random(seed);
            Color[] colors = new Color[colorMapSize * colorMapSize];
            for (int i = 0; i < colorMapSize * colorMapSize; i++) {
                colors[i] = (rand.NextDouble() > frequency) ? secondaryColor : mainColor;
            }
            colorMapCache.Add(seed, colors);
            return (int x, int y) => colors[x % colorMapSize + y % colorMapSize * colorMapSize];
        }
        public static readonly int[][] SideNeighbors = [[1,0],[0,1],[-1,0],[0,-1]];
    }
}
