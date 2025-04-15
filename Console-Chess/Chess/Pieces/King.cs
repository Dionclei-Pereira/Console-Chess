using Console_Chess.Board;

namespace Console_Chess.Chess.Pieces {
    internal class King : Piece {

        private ChessGame Game;

        public King(GameBoard board, Color color, ChessGame game) : base(board, color) {
            Game = game;
        }

        private bool TestRook(Position pos) {
            Piece p = Board.GetPiece(pos);
            return p != null && p is Rook && p.Color == this.Color && p.MovementsAmount == 0;
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

            if (MovementsAmount == 0 && !Game.IsGameInCheck) {
                Position rook1Position = new Position(Position.X, Position.Y + 3);
                if (TestRook(rook1Position)) {
                    Position p1 = new Position(Position.X, Position.Y + 1);
                    Position p2 = new Position(Position.X, Position.Y + 2);
                    if (Board.GetPiece(p1) == null && Board.GetPiece(p2) == null) {
                        m[Position.X, Position.Y + 2] = true;
                    }
                }

                Position rook2Position = new Position(Position.X, Position.Y - 4);
                if (TestRook(rook2Position)) {
                    Position p1 = new Position(Position.X, Position.Y - 1);
                    Position p2 = new Position(Position.X, Position.Y - 2);
                    Position p3 = new Position(Position.X, Position.Y - 3);
                    if (Board.GetPiece(p1) == null && Board.GetPiece(p2) == null && Board.GetPiece(p3) == null) {
                        m[Position.X, Position.Y - 2] = true;
                    }
                }

            }
            return m;
        }
        public override string ToString() {
            return "K";
        }
    }
}
