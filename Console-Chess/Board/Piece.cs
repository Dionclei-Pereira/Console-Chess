
namespace Console_Chess.Board {
    internal class Piece {

        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MovementsAmount { get; protected set; }
        public GameBoard Board { get; protected set; }

        public Piece(Position position, GameBoard board, Color color) {
            Position = position;
            Color = color;
            Board = board;
            MovementsAmount = 0;
        }

    }
}
