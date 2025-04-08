using Console_Chess.Board;
using Console_Chess.Chess;
using System.Drawing;

namespace Console_Chess {
    internal class Screen {

        public static void PrintBoard(GameBoard board) {
            Console.BackgroundColor = ConsoleColor.Black;
            for (int i = 0; i < board.X; i++) {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Y; j++) {
                    PrintPiece(board.GetPiece(i, j));
                }
                Console.WriteLine();
            }
            Console.Write("  A B C D E F G H");
        }

        public static void PrintBoard(GameBoard board, bool[,] moves) {
            for (int i = 0; i < board.X; i++) {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Y; j++) {
                    Console.BackgroundColor = moves[i, j] == true ? 
                        Console.BackgroundColor = ConsoleColor.DarkGray : ConsoleColor.Black;
                    PrintPiece(board.GetPiece(i, j));
                }
                Console.WriteLine();
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("  A B C D E F G H");
        }

        public static Position ReadPosition() {
            string s = Console.ReadLine();
            char ch = s[0];
            int i = int.Parse(s[1] + "");
            return new ChessPosition(ch, i).ToPosition();
        }

        private static void PrintPiece(Piece p) {
            if(p == null) {
                Console.Write("- ");
                return;
            }
            ConsoleColor color = (ConsoleColor) Enum.Parse(typeof(ConsoleColor), p.Color.ToString());
            Console.ForegroundColor = color;
            Console.Write(p + " ");
            Console.ForegroundColor = ConsoleColor.White;
        }

    }
}
