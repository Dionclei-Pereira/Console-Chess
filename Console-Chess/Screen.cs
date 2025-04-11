using Console_Chess.Board;
using Console_Chess.Board.Exceptions;
using Console_Chess.Chess;
using System.Drawing;

namespace Console_Chess {
    internal class Screen {

        public static void PrintGame(ChessGame game) {
            Console.Clear();
            PrintBoard(game.Board);
            Console.WriteLine();
            PrintCapturedPieces(game);
            Console.WriteLine();
            Console.WriteLine("Turn: " + game.Turn);
            if (!game.Ended) {
                Console.WriteLine("Current Player: " + game.Playing);
                Console.WriteLine();
                if (game.IsGameInCheck) {
                    Console.WriteLine("Check!");
                }
            } else {
                Console.WriteLine("CHECK MATE");
                Console.WriteLine("WINNER: " + game.GetEnemyColor(game.Playing));
            }
            Console.WriteLine();
        }

        private static void PrintCapturedPieces(ChessGame game) {
            Console.WriteLine("Captured Pieces: ");
            Console.Write(game.PlayerOne.ToString());
            PrintPieces(game.GetCapturedPieces(game.PlayerOne));
            Console.WriteLine();
            Console.Write(game.PlayerTwo.ToString());
            PrintPieces(game.GetCapturedPieces(game.PlayerTwo));
            Console.WriteLine();
        }

        private static void PrintPieces(HashSet<Piece> pieces) {
            Console.Write("[");
            pieces.ToList().ForEach(p => Console.Write(p.ToString() + " "));
            Console.Write("]");
        }

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

        public static Position ReadPosition(GameBoard board) {
            string s = Console.ReadLine();
            Position pos = null;
            if (s.Length >= 2) {
                char ch = s[0];
                int.TryParse(s[1] + "", out int i);
                pos = new ChessPosition(ch, i).ToPosition();
            }
            return board.PositionIsValid(pos) ? pos : throw new BoardException("Invalid Position");
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
