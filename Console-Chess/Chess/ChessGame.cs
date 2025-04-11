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
        public Color PlayerOne { get; protected set; }
        public Color PlayerTwo { get; protected set; }
        public Color Playing { get; protected set; }
        public bool Ended { get; protected set; }
        private HashSet<Piece> Pieces { get; set; }
        private HashSet<Piece> Captured { get; set; }
        public bool IsGameInCheck { get; set; }


        public ChessGame(Color colorOne, Color colorTwo) {
            Turn = 1;
            Board = new GameBoard(8, 8);
            Playing = colorOne;
            PlayerOne = colorOne;
            PlayerTwo = colorTwo;
            Pieces = new HashSet<Piece>();
            Captured = new HashSet<Piece>();
            PutPieces();
            Ended = false;
        }

        public void ExecuteMovement(Position origin, Position target) {
            Piece p = Move(origin, target);

            if (IsInCheck(Playing)) {
                Unmove(origin, target, p);
                throw new BoardException("You can not do this movement!");
            }

            Turn++;
            Playing = Playing == PlayerOne ? PlayerTwo : PlayerOne;

            if (IsInCheck(Playing)) {
                IsGameInCheck = true;
            } else {
                IsGameInCheck = false;
            }
        }

        public void Unmove(Position origin, Position target, Piece piece) {
            Piece p = Board.RemovePiece(target);
            p.DecreaseMovements();
            if (piece != null) {
                Board.PutPiece(piece, target);
                Captured.Remove(piece);
            }
            Board.PutPiece(p, origin);

        }

        public Piece Move(Position origin, Position target) {
            Piece movingPiece = Board.RemovePiece(origin);
            Piece targetPiece = Board.RemovePiece(target);

            if (targetPiece != null) {
                Captured.Add(targetPiece);
            }

            movingPiece.IncreaseMovements();
            Board.PutPiece(movingPiece, target);
            return targetPiece;
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

        public HashSet<Piece> GetPieces(Color color) {
            return Pieces.Where(p => p.Color == color && !Captured.Contains(p)).ToHashSet();
        }

        public HashSet<Piece> GetCapturedPieces(Color color) {
            return Captured.Where(p => p.Color == color).ToHashSet();
        }
        
        private Piece GetKing(Color color) {
            foreach (var p in GetPieces(color)) {
                if (p is King) return p;
            }
            return null;
        }

        private Color GetEnemyColor(Color color) {
            return color == PlayerOne ? PlayerTwo : PlayerOne;
        }

        public bool IsInCheck(Color color) {
            Piece king = GetKing(color) ?? throw new BoardException("King not found");
            foreach (Piece p in GetPieces(GetEnemyColor(color))) {
                bool[,] moves = p.GetMoves();
                if (moves[king.Position.X, king.Position.Y]) {
                    return true;
                }
            }
            return false;
        }

        public void PutNewPiece(ChessPosition position, Piece piece) {
            Board.PutPiece(piece, position.ToPosition());
            Pieces.Add(piece);
        }

        private void PutPieces() {
            PutNewPiece(new ChessPosition('a', 8), new Rook(Board, PlayerTwo));
            PutNewPiece(new ChessPosition('a', 1), new Rook(Board, PlayerOne));
            PutNewPiece(new ChessPosition('e', 1), new King(Board, PlayerOne));
            PutNewPiece(new ChessPosition('e', 8), new King(Board, PlayerTwo));
            PutNewPiece(new ChessPosition('h', 8), new Rook(Board, PlayerTwo));
            PutNewPiece(new ChessPosition('h', 1), new Rook(Board, PlayerOne));
        }
    }
}
