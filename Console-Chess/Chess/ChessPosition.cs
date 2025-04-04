using Console_Chess.Board;

namespace Console_Chess.Chess {
    internal class ChessPosition {

        public int X { get; set; }
        public char Y { get; set; }

        public ChessPosition(char y, int x) {
            X = x;
            Y = y;
        }

        public Position ToPosition() {
            return new Position(8 - X, Y - 'a');
        }

        public override string ToString() {
            return "" + Y + X;
        }

    }
}
