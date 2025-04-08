using Console_Chess.Board;

namespace Console_Chess.Chess.Pieces {
    internal class Rook : Piece {
        public Rook(GameBoard board, Color color) : base(board, color) {
        }

        private bool canMove(Position pos) {
            Piece p = Board.GetPiece(pos);
            return p == null || Color != p.Color;
        }

        public override bool[,] GetMoves() {
            bool[,] m = new bool[Board.X, Board.Y];
            Position pos = new Position(0, 0);

            pos.SetPos(Position.X - 1, Position.Y);
            while (Board.PositionIsValid(pos) && canMove(pos)) {
                m[pos.X, pos.Y] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != this.Color) {
                    break;
                }
                pos.X = pos.X - 1;
            }

            pos.SetPos(Position.X + 1, Position.Y);
            while (Board.PositionIsValid(pos) && canMove(pos)) {
                m[pos.X, pos.Y] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != this.Color) {
                    break;
                }
                pos.X = pos.X + 1;
            }
                
            pos.SetPos(Position.X, Position.Y - 1);
            while (Board.PositionIsValid(pos) && canMove(pos)) {
                m[pos.X, pos.Y] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != this.Color) {
                    break;
                }
                pos.Y = pos.Y - 1;
            }
            pos.SetPos(Position.X, Position.Y + 1);
            while (Board.PositionIsValid(pos) && canMove(pos)) {
                m[pos.X, pos.Y] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != this.Color) {
                    break;
                }
                pos.Y = pos.Y + 1;
            }
            return m;
        }

        public override string ToString() {
            return "R";
        }
    }
}
