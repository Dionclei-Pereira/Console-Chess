using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess.Board {
    internal class Board {
        public int X { get; set; }
        public int Y { get; set; }
        private Piece[,] Pieces { get; set; }

        public Board(int x, int y) {
            X = x;
            Y = y;
            Pieces = new Piece[X, Y];
        }
    }
}
