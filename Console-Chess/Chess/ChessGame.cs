using Console_Chess.Board;
using Console_Chess.Chess.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess.Chess {
    internal class ChessGame {

        public GameBoard Board { get; protected set; }
        private int Turn;
        private Color Player;
        public bool ended { get; protected set; }

        public ChessGame(Color color) {
            Turn = 1;
            Board = new GameBoard(8, 8);
            Player = color;
            PutPieces();
            ended = false;
        }

        public void Move(Position origin, Position target) {
            Piece p = Board.RemovePiece(origin);
            p.IncreaseMovements();
            Piece targetPiece = Board.RemovePiece(target);
            Board.PutPiece(p, target);
        }

        private void PutPieces() {
            King k = new King(Board, Color.Magenta);
            Board.PutPiece(k, new Position(1, 5));
            Rook rook = new Rook(Board, Color.Magenta);
            Board.PutPiece(rook, new Position(1, 7));
        }
    }
}
