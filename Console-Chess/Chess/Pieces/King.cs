using Console_Chess.Board;

namespace Console_Chess.Chess.Pieces {
    internal class King : Piece {
        public King(GameBoard board, Color color) : base(board, color) {
        }

        private bool canMove(Position pos) {
            Piece p = Board.GetPiece(pos);
            return p == null || Color != p.Color;
        }

        public override bool[,] GetMoves() {
            bool[,] m = new bool[Board.X, Board.Y];
            Position pos = new Position(0, 0);
            
            pos.SetPos(Position.X - 1, Position.Y);
            m[pos.X, pos.Y] = Board.PositionIsValid(pos) && canMove(pos);

            pos.SetPos(Position.X - 1, Position.Y + 1);
            m[pos.X, pos.Y] = Board.PositionIsValid(pos) && canMove(pos);

            pos.SetPos(Position.X, Position.Y + 1);
            m[pos.X, pos.Y] = Board.PositionIsValid(pos) && canMove(pos);
             
            pos.SetPos(Position.X + 1, Position.Y + 1);
            m[pos.X, pos.Y] = Board.PositionIsValid(pos) && canMove(pos);

            pos.SetPos(Position.X + 1, Position.Y);
            m[pos.X, pos.Y] = Board.PositionIsValid(pos) && canMove(pos);

            pos.SetPos(Position.X + 1, Position.Y - 1);
            m[pos.X, pos.Y] = Board.PositionIsValid(pos) && canMove(pos);

            pos.SetPos(Position.X - 1, Position.Y - 1);
            m[pos.X, pos.Y] = Board.PositionIsValid(pos) && canMove(pos);

            pos.SetPos(Position.X, Position.Y - 1);
            m[pos.X, pos.Y] = Board.PositionIsValid(pos) && canMove(pos);

            return m;
        }
        public override string ToString() {
            return "K";
        }
    }
}
