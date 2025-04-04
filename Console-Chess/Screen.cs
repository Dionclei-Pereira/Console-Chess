using Console_Chess.Board;
using System.Drawing;

namespace Console_Chess {
    internal class Screen {

        public static void PrintBoard(GameBoard board) {
            for (int i = 0; i < board.X; i++) {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Y; j++) {
                    if (board.GetPiece(i, j) != null) {
                        PrintPiece(board.GetPiece(i, j));
                    } else {
                        Console.Write("- ");
                    }
                }
                Console.WriteLine();
            }
            Console.Write("  A B C D E F G H");
        }

        private static void PrintPiece(Piece p) {
            ConsoleColor color = (ConsoleColor) Enum.Parse(typeof(ConsoleColor), p.Color.ToString());
            Console.ForegroundColor = color;
            Console.Write(p + " ");
            Console.ForegroundColor = ConsoleColor.White;
        }

    }
}
