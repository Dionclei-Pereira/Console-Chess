using Console_Chess.Board;
using Console_Chess.Chess;

namespace Console_Chess {
    internal class Program {
        static void Main(string[] args) {

            try {

                ChessGame game = new ChessGame(Color.Magenta, Color.Cyan);
                while(!game.ended) {

                    Console.Clear();
                    Screen.PrintBoard(game.Board);
                    Console.WriteLine();
                    Console.WriteLine();

                    Console.WriteLine("Turn: " + game.Turn);
                    Console.WriteLine("Current Player: " + game.Playing);

                    Console.WriteLine();
                    Console.Write("Origin: ");
                    Position origin = Screen.ReadPosition();

                    Console.Clear();
                    bool[,] moves = game.Board.GetPiece(origin).GetMoves();
                    Screen.PrintBoard(game.Board, moves);
                    Console.WriteLine();
                    Console.Write("Target: ");
                    Position target = Screen.ReadPosition();

                    game.ExecuteMovement(origin, target);

                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

        }
    }
}