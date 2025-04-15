
namespace Console_Chess.Board {
    internal abstract class Piece {

        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MovementsAmount { get; protected set; }
        public GameBoard Board { get; protected set; }

        public Piece(GameBoard board, Color color) {
            Color = color;
            Board = board;
            MovementsAmount = 0;
        }

        public bool canMove(Position pos) {
            Piece p = Board.GetPiece(pos);
            return p == null || Color != p.Color;
        }

        public bool CanMoveTo(Position pos) {
            return GetMoves()[pos.X, pos.Y];
        }
        public bool ExistMovements() {
            bool[,] movements = GetMoves();
            for (int i = 0; i < Board.X; i++) {
                for (int j = 0; j < Board.Y; j++) {
                    if (movements[i, j]) {
                        return true;
                    }
                }
            }
            return false;
        }
        public void IncreaseMovements() {
            MovementsAmount++;
        }

        public void DecreaseMovements() {
            MovementsAmount--;
        }

        public abstract override string ToString();

        public abstract bool[,] GetMoves();
    }
}
