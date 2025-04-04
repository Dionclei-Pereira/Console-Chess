
using Console_Chess.Board;

namespace Console_Chess.Chess {
    internal class King : Piece {
        public King(GameBoard board, Color color) : base(board, color) {
        }

        public override string ToString() {
            return "K";
        }
    }
}
