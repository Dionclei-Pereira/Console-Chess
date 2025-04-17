using Console_Chess.Board;

namespace Console_Chess.Chess.Pieces {
    internal class Knight : Piece {

        public Knight(GameBoard board, Color color) : base(board, color) { }

        public override bool[,] GetMoves() {

            bool[,] m = new bool[Board.X, Board.Y];

            Position pos = new Position(0, 0);

            pos.SetPos(Position.X - 1, Position.Y - 2);
            if (Board.PositionIsValid(pos) && CanMove(pos)) {
                m[pos.X, pos.Y] = true;
            }

            pos.SetPos(Position.X - 2, Position.Y - 1);
            if (Board.PositionIsValid(pos) && CanMove(pos)) {
                m[pos.X, pos.Y] = true;
            }

            pos.SetPos(Position.X - 2, Position.Y + 1);
            if (Board.PositionIsValid(pos) && CanMove(pos)) {
                m[pos.X, pos.Y] = true;
            }

            pos.SetPos(Position.X - 1, Position.Y + 2);
            if (Board.PositionIsValid(pos) && CanMove(pos)) {
                m[pos.X, pos.Y] = true;
            }

            pos.SetPos(Position.X + 1, Position.Y + 2);
            if (Board.PositionIsValid(pos) && CanMove(pos)) {
                m[pos.X, pos.Y] = true;
            }

            pos.SetPos(Position.X + 2, Position.Y + 1);
            if (Board.PositionIsValid(pos) && CanMove(pos)) {
                m[pos.X, pos.Y] = true;
            }

            pos.SetPos(Position.X + 2, Position.Y - 1);
            if (Board.PositionIsValid(pos) && CanMove(pos)) {
                m[pos.X, pos.Y] = true;
            }

            pos.SetPos(Position.X + 1, Position.Y - 2);
            if (Board.PositionIsValid(pos) && CanMove(pos)) {
                m[pos.X, pos.Y] = true;
            }

            return m;
        }

        public override string ToString() {
            return "N";
        }
    }
}
