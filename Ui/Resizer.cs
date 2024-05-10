using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace falling_sand.Ui
{
    public class Resizer
    {
        static Panel bg = FallingSand.Form.BottomBar;
        public static void Init()
        {
            UpdateSizes();
            FallingSand.Form.Resize += (obj, args) =>
            {
                UpdateSizes();
            };
        }

        public static Size GetCorrectedFormSize()
        {
            return FallingSand.Form.Size - new Size(16, 39);
        }

        public static void UpdateSizes()
        {
            UpdateCanvas();
            UpdateToolbar();
        }

        public static void UpdateCanvas()
        {
            Size canvasAvailableRegion = GetCorrectedFormSize() - new Size(0, bg.Height);
            double aspectRatio = 1d;
            double minSize = Math.Min(canvasAvailableRegion.Width, canvasAvailableRegion.Height);
            Size canvasSize = new Size();
            Point canvasPosition = new Point();
            if (minSize * aspectRatio > canvasAvailableRegion.Width)
            {
                minSize = canvasAvailableRegion.Width;
            }
            else if (minSize / aspectRatio > canvasAvailableRegion.Height)
            {
                minSize = canvasAvailableRegion.Height;
            }
            if (minSize == canvasAvailableRegion.Height)
            {
                canvasSize.Height = (int)minSize;
                canvasSize.Width = (int)(minSize * aspectRatio);
                canvasPosition.X = (int)(canvasAvailableRegion.Width / 2d - canvasSize.Width / 2d);
            }
            else
            {
                canvasSize.Width = (int)minSize;
                canvasSize.Height = (int)(minSize / aspectRatio);
                canvasPosition.Y = (int)(canvasAvailableRegion.Height / 2d - canvasSize.Height / 2d);
            }

            FallingSand form = FallingSand.Form;
            form.Canvas.Size = canvasSize;
            form.Canvas.Location = canvasPosition;
            form.CanvasInput.Size = canvasSize;
        }

        public static void UpdateToolbar()
        {
            Panel bar = FallingSand.Form.BottomCenterBar;
            bg.Size = new Size(GetCorrectedFormSize().Width, bg.Height);
            bg.Location = new Point(0, GetCorrectedFormSize().Height - bg.Height);
            bar.Location = new Point(GetCorrectedFormSize().Width / 2 - bar.Width / 2, 0);
        }
    }
}
