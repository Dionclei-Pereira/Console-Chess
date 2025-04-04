using Console_Chess.Board;

namespace Console_Chess {
    internal class Screen {

        public static void PrintBoard(GameBoard board) {
            for (int i = 0; i < board.X; i++) {
                for (int j = 0; j < board.Y; j++) {
                    if (board.GetPiece(i, j) != null) {
                        Console.Write(board.GetPiece(i, j) + " ");
                    } else {
                        Console.Write("- ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
