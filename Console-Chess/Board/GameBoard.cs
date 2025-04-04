using Console_Chess.Board.Exceptions;

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

        public bool PositionIsValid(Position pos) {
            if (pos.X < 0 || pos.X >= X || pos.Y < 0 || pos.Y >= Y) {
                return false;
            }
            return true;
        }

        public bool ExistPieceAt(Position pos) {
            ValidatePosition(pos);
            if (Pieces[pos.X, pos.Y] == null) return false;
            return true;
        }

        public void ValidatePosition(Position pos) {
            if (!PositionIsValid(pos)) {
                throw new BoardException("Invalid position");
            }
        }
    }
}
