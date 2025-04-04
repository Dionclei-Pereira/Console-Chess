
namespace Console_Chess.Board {
    internal class GameBoard {
        public int X { get; set; }
        public int Y { get; set; }
        private Piece[,] Pieces { get; set; }

        public GameBoard(int x, int y) {
            X = x;
            Y = y;
            Pieces = new Piece[X, Y];
        }

        public Piece GetPiece(int x, int y) {
            return Pieces[x, y];
        }

        public void PutPiece(Piece piece, Position pos) {
            Pieces[pos.X, pos.Y] = piece;
            piece.Position = pos;
        }
    }
}
