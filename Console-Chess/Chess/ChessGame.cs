using Console_Chess.Board;
using Console_Chess.Board.Exceptions;
using Console_Chess.Chess.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess.Chess {
    internal class ChessGame {

        public GameBoard Board { get; protected set; }
        public int Turn { get; protected set; }
        private Color PlayerOne;
        private Color PlayerTwo;
        public Color Playing { get; protected set; }
        public bool ended { get; protected set; }

        public ChessGame(Color colorOne, Color colorTwo) {
            Turn = 1;
            Board = new GameBoard(8, 8);
            Playing = colorOne;
            PlayerOne = colorOne;
            PlayerTwo = colorTwo;
            PutPieces();
            ended = false;
        }

        public void ExecuteMovement(Position origin, Position target) {
            Turn++;
            Playing = Playing == PlayerOne ? PlayerTwo : PlayerOne;
            Move(origin, target);
        }

        public void ValidateOriginPos(Position pos) {
            if (!Board.PositionIsValid(pos)) {
                throw new BoardException("Invalid position!");
            }
            if (Board.GetPiece(pos) == null) {
                throw new BoardException("This square is empty.");
            }

            if (Playing != Board.GetPiece(pos).Color) {
                throw new BoardException("This piece is not yours");
            }

            if (!Board.GetPiece(pos).ExistMovements()) {
                throw new BoardException("This piece is blocked");
            }
        }
        public void ValidateTargetPos(Position origin, Position target) {
            if (!Board.PositionIsValid(target)) {
                throw new BoardException("Invalid position!");
            }

            if (!Board.GetPiece(origin).CanMoveTo(target)) {
                throw new BoardException("Target position is invalid");
            }
        }

        public void Move(Position origin, Position target) {
            Piece p = Board.RemovePiece(origin);
            p.IncreaseMovements();
            Piece targetPiece = Board.RemovePiece(target);
            Board.PutPiece(p, target);
        }

        private void PutPieces() {
            King k = new King(Board, Color.Magenta);
            Board.PutPiece(k, new Position(0, 5));
            Board.PutPiece(new Rook(Board, Color.Magenta), new Position(0, 6));
            Board.PutPiece(new Rook(Board, Color.Magenta), new Position(0, 4));
            Board.PutPiece(new Rook(Board, Color.Magenta), new Position(1, 6));
            Board.PutPiece(new Rook(Board, Color.Magenta), new Position(1, 5));
            Board.PutPiece(new Rook(Board, Color.Magenta), new Position(1, 4));
        }
    }
}
