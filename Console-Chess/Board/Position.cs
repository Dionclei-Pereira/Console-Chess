﻿
namespace Console_Chess.Board {
    internal class Position {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y) {
            X = x;
            Y = y;
        }

        public void SetPos(int x, int y) {
            X = x;
            Y = y;
        }

        public override string ToString() {
            return "X: " + X + ", Y: " + Y;
        }
    }
}
