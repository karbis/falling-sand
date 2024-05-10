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
        private bool isDestroyed = false;
        private bool ignoreLiquidUpdates = false;
        public int SelectionBarOrder = 0;

        public bool Flammable = false;
        public bool Gravity = false;
        public bool Liquid = false;

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
            if (!IsEmptySpaceFrom(0,1)) {
                Element? underElement = GetElementFrom(0, 1);
                if (underElement != null && !underElement.Gravity) return; // dont move if block under doesnt have gravity
                if (underElement != null && underElement.Gravity && IsEmptySpaceFrom(0, 2)) {
                    Move(0, 1); // if you are on top of a falling gravity block, dont move, fall anyways
                    return;
                }
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
            if (elem2 != null && elem2.Name == Name) {
                int steps = 0;
                int moveDir = -1;
                bool moved = false;
                while (true) {
                    if (IsEmptySpaceFrom(0, 1)) break;
                    if (!IsEmptySpaceFrom(moveDir, 0)) {
                        if (moved) break;
                        Move(-steps, 0);
                        moveDir *= -1;
                        steps = 0;
                        moved = true;
                    }
                    Element? elem4 = GetElementFrom(0, 1);
                    if (elem4 != null && elem4.Name != Name) {
                        Move(-steps, 0);
                        break;
                    }
                    Move(moveDir, 0);
                    steps += moveDir;
                }
                if (IsEmptySpaceFrom(0,1)) {
                    Move(0, 1);
                }
                // if under a liquid object, attempt to move down to the liquid object by pushing other liquid objects to the left/right
            } else if (elem2 != null && (!elem2.Gravity || IsEmptySpaceFrom(0,2))) {
                if (IsEmptySpaceFrom(1,0) && IsEmptySpaceFrom(1,1)) {
                    Move(1, 1);
                } else if (IsEmptySpaceFrom(-1,0) && IsEmptySpaceFrom(-1,1)) {
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

        private void updateSpatialMap(int newX, int newY) {
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
            return SpatialMap.ElementAtOrDefault(Game.GetGlobalFromPos(x, y));
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

        public void Destroy() {
            SpatialMap[Game.GetGlobalFromPos(X, Y)] = null;
            isDestroyed = true;
        }
    }
}
