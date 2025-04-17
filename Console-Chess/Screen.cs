using Console_Chess.Board;
using Console_Chess.Board.Exceptions;
using Console_Chess.Chess;
using System.Drawing;

namespace Console_Chess {
    internal class Screen {

        public static void GetColors(out Board.Color? c1, out Board.Color? c2) {
            Console.ForegroundColor = ConsoleColor.White;
            HashSet<Board.Color> colors = new HashSet<Board.Color> { Board.Color.Yellow, Board.Color.White, Board.Color.Cyan, Board.Color.Red, Board.Color.Blue, Board.Color.Magenta, Board.Color.Green };
            c1 = null;
            c2 = null;
            Console.WriteLine("PLAYER ONE");
            TryParseColor(ref c1, ref colors);
            Console.Clear();
            Console.WriteLine("PLAYER TWO");
            TryParseColor(ref c2, ref colors);
        }

        public static Board.Color? TryParseColor(ref Board.Color? color, ref HashSet<Board.Color> colors) {
            while(color == null) {
                try {
                    Console.WriteLine("Please enter a color:");
                    Console.Write("List of colors: [ ");
                    foreach (Board.Color c in colors) {
                        Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), c.ToString());
                        Console.Write(c + " ");
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("]");
                    string? typed = Console.ReadLine();
                    bool parsed = Enum.TryParse(typeof(Board.Color), typed, out var result);
                    if (parsed && colors.Contains((Board.Color)result)) {
                        color = (Board.Color?)result;
                        colors.Remove((Board.Color)color);
                    }
                } catch (InvalidOperationException e) {
                    Console.WriteLine("Invalid Color!");
                }
            }
            return color;
        }

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
                if (game is ThreeCheck) {
                    Console.WriteLine("WINNER: " + game.GetEnemyColor(game.Playing));
                } else {
                    Console.WriteLine("CHECK MATE");
                    Console.WriteLine("WINNER: " + game.GetEnemyColor(game.Playing));
                }
            }
            if (game is ThreeCheck) {
                ThreeCheck threeCheck = (ThreeCheck) game;
                Console.WriteLine("Player One Checks: " + threeCheck.PlayerOneChecks);
                Console.WriteLine("Player Two Checks: " + threeCheck.PlayerTwoChecks);
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

        public static void GetGame(Board.Color? c1, Board.Color? c2, out ChessGame game) {
            char mode = '-';
            while(!"NT".Contains(mode)) {
                Console.Clear();
                Console.WriteLine("Enter a game mode: Normal(N) - Three-Check(T)");
                string str = Console.ReadLine();
                if (str.Length > 0) {
                    mode = str[0];
                }
            }
            switch (mode) {
                case 'T':
                    game = new ThreeCheck((Board.Color)c1, (Board.Color)c2);
                    break;
                default:
                    game = new ChessGame((Board.Color)c1, (Board.Color)c2);
                    break;
            }
        }
    }
}
