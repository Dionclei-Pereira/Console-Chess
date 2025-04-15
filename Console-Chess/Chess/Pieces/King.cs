using Console_Chess.Board;

namespace Console_Chess.Chess.Pieces {
    internal class King : Piece {
        public King(GameBoard board, Color color) : base(board, color) {
        }

        public override bool[,] GetMoves() {
            bool[,] m = new bool[Board.X, Board.Y];
            Position pos = new Position(0, 0);
            
            pos.SetPos(Position.X - 1, Position.Y);
            if (Board.PositionIsValid(pos)) {
                m[pos.X, pos.Y] = CanMove(pos);
            }

            pos.SetPos(Position.X - 1, Position.Y + 1);
            if (Board.PositionIsValid(pos)) {
                m[pos.X, pos.Y] = CanMove(pos);
            }

            pos.SetPos(Position.X, Position.Y + 1);
            if (Board.PositionIsValid(pos)) {
                m[pos.X, pos.Y] = CanMove(pos);
            }

            pos.SetPos(Position.X + 1, Position.Y + 1);
            if (Board.PositionIsValid(pos)) {
                m[pos.X, pos.Y] = CanMove(pos);
            }

            pos.SetPos(Position.X + 1, Position.Y);
            if (Board.PositionIsValid(pos)) {
                m[pos.X, pos.Y] = CanMove(pos);
            }

            pos.SetPos(Position.X + 1, Position.Y - 1);
            if (Board.PositionIsValid(pos)) {
                m[pos.X, pos.Y] = CanMove(pos);
            }

            pos.SetPos(Position.X - 1, Position.Y - 1);
            if (Board.PositionIsValid(pos)) {
                m[pos.X, pos.Y] = CanMove(pos);
            }

            pos.SetPos(Position.X, Position.Y - 1);
            if (Board.PositionIsValid(pos)) {
                m[pos.X, pos.Y] = CanMove(pos);
            }

            return m;
        }
        public override string ToString() {
            return "K";
        }
    }
}
