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

            if (TestCheckMate(Playing)) {
                Ended = true;
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

            if (p is King && target.Y == origin.Y + 2) {
                Position rookOrigin = new Position(origin.X, origin.Y + 3);
                Position rookTarget = new Position(origin.X, origin.Y + 1);
                Piece rook = Board.RemovePiece(rookTarget);
                rook.DecreaseMovements();
                Board.PutPiece(rook, origin);
            } else if (p is King && target.Y == origin.Y - 2) {
                Position rookOrigin = new Position(origin.X, origin.Y - 4);
                Position rookTarget = new Position(origin.X, origin.Y - 1);
                Piece rook = Board.RemovePiece(rookTarget);
                rook.DecreaseMovements();
                Board.PutPiece(rook, origin);
            }
        }

        public Piece Move(Position origin, Position target) {
            Piece movingPiece = Board.RemovePiece(origin);
            Piece targetPiece = Board.RemovePiece(target);

            if (targetPiece != null) {
                Captured.Add(targetPiece);
            }

            movingPiece.IncreaseMovements();
            Board.PutPiece(movingPiece, target);

            if (movingPiece is King && target.Y == origin.Y + 2) {
                Position rookOrigin = new Position(origin.X, origin.Y + 3);
                Position rookTarget = new Position(origin.X, origin.Y + 1);
                Piece rook = Board.RemovePiece(rookOrigin);
                rook.IncreaseMovements();
                Board.PutPiece(rook, rookTarget);
            } else if (movingPiece is King && target.Y == origin.Y - 2) {
                Position rookOrigin = new Position(origin.X, origin.Y - 4);
                Position rookTarget = new Position(origin.X, origin.Y -1);
                Piece rook = Board.RemovePiece(rookOrigin);
                rook.IncreaseMovements();
                Board.PutPiece(rook, rookTarget);
            }

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

        public Color GetEnemyColor(Color color) {
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

        public bool TestCheckMate(Color color) {
            if (!IsInCheck(color)) return false;
            foreach (Piece p in GetPieces(color)) {
                bool[,] m = p.GetMoves();
                Position origin = p.Position;
                for (int i = 0; i < Board.X; i++) {
                    for (int j = 0; j < Board.Y; j++) {
                        if (m[i, j]) {
                            Position target = new Position(i, j);
                            Piece targetPiece = Move(origin, target);
                            bool isInCheck = IsInCheck(color);
                            Unmove(origin, target, targetPiece);
                            if (!isInCheck) return false;
                        }
                    }
                }
            }
            return true;
        }

        public void PutNewPiece(ChessPosition position, Piece piece) {
            Board.PutPiece(piece, position.ToPosition());
            Pieces.Add(piece);
        }

        private void PutPieces() {
            PutNewPiece(new ChessPosition('a', 8), new Rook(Board, PlayerTwo));
            PutNewPiece(new ChessPosition('a', 1), new Rook(Board, PlayerOne));
            PutNewPiece(new ChessPosition('h', 8), new Rook(Board, PlayerTwo));
            PutNewPiece(new ChessPosition('h', 1), new Rook(Board, PlayerOne));

            PutNewPiece(new ChessPosition('e', 1), new King(Board, PlayerOne, this));
            PutNewPiece(new ChessPosition('e', 8), new King(Board, PlayerTwo, this));

            PutNewPiece(new ChessPosition('c', 1), new Bishop(Board, PlayerOne));
            PutNewPiece(new ChessPosition('c', 8), new Bishop(Board, PlayerTwo));
            PutNewPiece(new ChessPosition('f', 1), new Bishop(Board, PlayerOne));
            PutNewPiece(new ChessPosition('f', 8), new Bishop(Board, PlayerTwo));

            PutNewPiece(new ChessPosition('b', 1), new Knight(Board, PlayerOne));
            PutNewPiece(new ChessPosition('b', 8), new Knight(Board, PlayerTwo));
            PutNewPiece(new ChessPosition('g', 1), new Knight(Board, PlayerOne));
            PutNewPiece(new ChessPosition('g', 8), new Knight(Board, PlayerTwo));

            PutNewPiece(new ChessPosition('d', 1), new Queen(Board, PlayerOne));
            PutNewPiece(new ChessPosition('d', 8), new Queen(Board, PlayerTwo));

            PutNewPiece(new ChessPosition('a', 2), new Pawn(Board, PlayerOne, PlayerOne));
            PutNewPiece(new ChessPosition('a', 7), new Pawn(Board, PlayerTwo, PlayerOne));
            PutNewPiece(new ChessPosition('b', 2), new Pawn(Board, PlayerOne, PlayerOne));
            PutNewPiece(new ChessPosition('b', 7), new Pawn(Board, PlayerTwo, PlayerOne));
            PutNewPiece(new ChessPosition('c', 2), new Pawn(Board, PlayerOne, PlayerOne));
            PutNewPiece(new ChessPosition('c', 7), new Pawn(Board, PlayerTwo, PlayerOne));
            PutNewPiece(new ChessPosition('d', 2), new Pawn(Board, PlayerOne, PlayerOne));
            PutNewPiece(new ChessPosition('d', 7), new Pawn(Board, PlayerTwo, PlayerOne));
            PutNewPiece(new ChessPosition('e', 2), new Pawn(Board, PlayerOne, PlayerOne));
            PutNewPiece(new ChessPosition('e', 7), new Pawn(Board, PlayerTwo, PlayerOne));
            PutNewPiece(new ChessPosition('f', 2), new Pawn(Board, PlayerOne, PlayerOne));
            PutNewPiece(new ChessPosition('f', 7), new Pawn(Board, PlayerTwo, PlayerOne));
            PutNewPiece(new ChessPosition('g', 2), new Pawn(Board, PlayerOne, PlayerOne));
            PutNewPiece(new ChessPosition('g', 7), new Pawn(Board, PlayerTwo, PlayerOne));
            PutNewPiece(new ChessPosition('h', 2), new Pawn(Board, PlayerOne, PlayerOne));
            PutNewPiece(new ChessPosition('h', 7), new Pawn(Board, PlayerTwo, PlayerOne));

        }
    }
}
