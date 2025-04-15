using Console_Chess.Board;

namespace Console_Chess.Chess.Pieces {
    internal class Pawn : Piece {

        private Color PlayerOne;

        public Pawn(GameBoard board, Color color, Color playerOne) : base(board, color) {
            PlayerOne = playerOne;
        }

        private bool ExistsEnemy(Position pos) {
            Piece p = Board.GetPiece(pos);
            return p != null && p.Color != this.Color;
        }

        private bool IsFree(Position pos) {
            return Board.GetPiece(pos) == null;
        }

        public override bool[,] GetMoves() {
            bool[,] m = new bool[Board.X, Board.Y];
            Position pos = new Position(0, 0);
            int direction = (Color == PlayerOne) ? -1 : 1;

            Position forwardOne = new Position(Position.X + direction, Position.Y);
            if (Board.PositionIsValid(forwardOne) && IsFree(forwardOne)) {
                m[forwardOne.X, forwardOne.Y] = true;
            }

            Position forwardTwo = new Position(Position.X + 2 * direction, Position.Y);
            Position intermediate = new Position(Position.X + direction, Position.Y);
            if (Board.PositionIsValid(forwardTwo) && IsFree(forwardTwo) && IsFree(intermediate) && MovementsAmount == 0) {
                m[forwardTwo.X, forwardTwo.Y] = true;
            }

            Position diagLeft = new Position(Position.X + direction, Position.Y - 1);
            if (Board.PositionIsValid(diagLeft) && ExistsEnemy(diagLeft)) {
                m[diagLeft.X, diagLeft.Y] = true;
            }

            Position diagRight = new Position(Position.X + direction, Position.Y + 1);
            if (Board.PositionIsValid(diagRight) && ExistsEnemy(diagRight)) {
                m[diagRight.X, diagRight.Y] = true;
            }
            return m;
        }

        public override string ToString() {
            return "P";
        }
    }
}
