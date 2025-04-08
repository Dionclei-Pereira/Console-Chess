
namespace Console_Chess.Board {
    internal abstract class Piece {

        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MovementsAmount { get; protected set; }
        public GameBoard Board { get; protected set; }

        public Piece(GameBoard board, Color color) {
            Color = color;
            Board = board;
            MovementsAmount = 0;
        }

        public void IncreaseMovements() {
            MovementsAmount++;
        }
    }
}
