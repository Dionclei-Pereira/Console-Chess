using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess.Board {
    internal class Piece {

        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MovementsAmount { get; protected set; }
        public Board Board { get; protected set; }

        public Piece(Position position, Board board, Color color) {
            Position = position;
            Color = color;
            Board = board;
            MovementsAmount = 0;
        }

    }
}
