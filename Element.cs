using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using falling_sand.Ui;

namespace falling_sand {
    public class Element {
        public string Name;
        public string? Description;
        public Color ElementColor;
        public delegate Color ColorFunction(int x, int y);
        public ColorFunction? ElementColorFunction;
        public Bitmap? ElementImage;
        private int _x = 0;
        private int _y = 0;
        public int X {
            set {
                updateSpatialMap(value, _y);
                _x = value;
            }
            get { return _x; }
        }
        public int Y {
            set {
                updateSpatialMap(_x, value);
                _y = value;
            }
            get { return _y; }
        }

        public static List<Element?> SpatialMap = [];
        public bool isDestroyed = false;
        internal bool ignoreLiquidUpdates = false;
        public int SelectionBarOrder = 0;

        public bool Flammable = false;
        public bool Gravity = false;
        public bool Liquid = false;
        public bool Invincible = false;
        public bool Hot = false;

        public Element() {
            Name = GetType().Name;
        }

        public virtual void Update() {
            ignoreLiquidUpdates = false;
            if (Liquid) {
                liquidUpdate();
            }
            if (Gravity) {
                gravityUpdate();
            }
        }
        private void gravityUpdate() {
            if (!isFalling()) {
                Element? underElement = GetElementFrom(0, 1);
                if (underElement != null && !underElement.Gravity) return; // dont move if block under doesnt have gravity
                // block under, move left / right
                if (IsEmptySpaceFrom(1,1) && IsEmptySpaceFrom(1,0)) {
                    Move(1, 1);
                 } else if (IsEmptySpaceFrom(-1,1) && IsEmptySpaceFrom(-1,0)) {
                    Move(-1, 1);
                }
            } else {
                // gravity
                Move(0, 1);
            }
        }
        private void liquidUpdate() {
            Element? elem2 = GetElementFrom(0, 1);
            if (elem2 != null && elem2.Name == Name && !isFalling()) {
                int steps = 0;
                int moveDir = -1;
                bool moved = false;
                while (true) {
                    steps += moveDir;
                    Element? elem4 = GetElementFrom(steps, 1);
                    if (IsWallFrom(steps, 1) || (elem4 != null && elem4.Name != Name)) {
                        if (moved) break;
                        moved = true;
                        moveDir *= -1;
                        steps = 0;
                        continue;
                    }
                    Element? elem5 = GetElementFrom(steps, 0);
                    if (IsEmptySpaceFrom(steps, 1) && (elem5 != null && elem5.Name == Name)) {
                        Move(steps, 0);
                        Move(0, 1);
                        break; // makes deleting water look cooler
                    }
                    if (IsEmptySpaceFrom(steps, 1)) {
                        Move(steps, 1);
                        break;
                    }
                }
                // liquid physics
            } else if (elem2 != null && !isFalling()) {
                if (IsEmptySpaceFrom(1, 0) && IsEmptySpaceFrom(1, 1)) {
                    Move(1, 1);
                } else if (IsEmptySpaceFrom(-1, 0) && IsEmptySpaceFrom(-1, 1)) {
                    Move(-1, 1);
                }
                // if under a ledge, fall off of it
            }
            if (IsEmptySpaceFrom(0,1)) return;
            if (IsWallFrom(0, -1)) return;
            Element? elem = GetElementFrom(0, -1);
            if (elem == null) return;
            if (!elem.Gravity || elem.Liquid || elem.ignoreLiquidUpdates) return;
            Y -= 1;
            elem.Y += 1;
            updateSpatialMap(X, Y);
            elem.updateSpatialMap(elem.X, elem.Y);
            elem.ignoreLiquidUpdates = true;
            // if gravity object above liquid, move water up
        }

        protected internal void updateSpatialMap(int newX, int newY) {
            if (isDestroyed) return;
            int curPos = Game.GetGlobalFromPos(X, Y);
            int newPos = Game.GetGlobalFromPos(newX, newY);
            if (SpatialMap[curPos] == this) {
                SpatialMap[curPos] = null;
            }
            if (SpatialMap[newPos] == null) {
                SpatialMap[newPos] = this;
            }
        }

        public void Move(int x, int y) {
            // currently, you are only expected to move 1 unit
            // can easily? be fixed tho
            if (SpatialMap.ElementAtOrDefault(Game.GetGlobalFromPos(X + x, Y + y)) != null) return;

            X = Math.Clamp(X+x, 0, Game.GameSize.Width-1);
            Y = Math.Clamp(Y + y, 0, Game.GameSize.Height - 1);
        }
        public Element? GetElementFrom(int x, int y) {
            // from offset*
            return GetElementByCoords(X + x, Y + y);
        }
        public static Element? GetElementByCoords(int x, int y) {
            if (IsWall(x, y)) return null;
            return SpatialMap.ElementAtOrDefault(Game.GetGlobalFromPos(x, y));
        }
        public Element? GetElementOfType(string name, int x, int y) {
            Element? elem = GetElementFrom(x, y);
            if (elem == null || elem.Name != name) return null;
            return elem;
        }
        public static bool IsWall(int x, int y) {
            return x < 0 || y < 0 || x > Game.GameSize.Width - 1 || y > Game.GameSize.Height - 1;
        }
        public bool IsWallFrom(int x, int y) {
            return IsWall(X + x, Y + y);
        }
        public bool IsEmptySpaceFrom(int x, int y) {
            return GetElementFrom(x, y) == null && !IsWallFrom(x, y);
        }
        public static bool IsEmptySpace(int x, int y) {
            return GetElementByCoords(x, y) == null && !IsWall(x, y);
        }
        public bool IsTouchingHotElement(int[][]? neighbors = null) {
            neighbors ??= Game.SideNeighbors;
            foreach (int[] n in neighbors) {
                Element? elem = GetElementFrom(n[0], n[1]);
                if (elem != null && elem.Hot) return true;
            }
            return false;
        }

        public void Destroy() {
            SpatialMap[Game.GetGlobalFromPos(X, Y)] = null;
            isDestroyed = true;
        }
        internal bool isFalling() {
            int i = 1;
            while (true) {
                if (IsWallFrom(0, i)) return false;
                if (IsEmptySpaceFrom(0, i)) return true;
                Element? elem = GetElementFrom(0, i);
                if (elem == null) return true;
                Element? elem2 = GetElementFrom(0, i + 1);
                //if (elem.Gravity && elem2 != null && !elem2.Gravity) return true;
                if (!elem.Gravity) return false;
                i++;
            }
        }

        static Dictionary<int, Color[]> colorMapCache = [];
        public static ColorFunction GenerateColorMap(int seed, double frequency, Color mainColor, Color secondaryColor) {
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
    }
}
