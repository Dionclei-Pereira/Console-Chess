using Console_Chess.Board;
using Console_Chess.Board.Exceptions;
using Console_Chess.Chess;

namespace Console_Chess {
    internal class Program {
        static void Main(string[] args) {

            try {

                ChessGame game = new ChessGame(Color.Magenta, Color.Cyan);
                while(!game.Ended) {
                    try {
                        Screen.PrintGame(game);
                        Console.Write("Origin: ");
                        Position origin = Screen.ReadPosition(game.Board);
                        game.ValidateOriginPos(origin);

                        Console.Clear();
                        bool[,] moves = game.Board.GetPiece(origin).GetMoves();
                        Screen.PrintBoard(game.Board, moves);
                        Console.WriteLine();
                        Console.Write("Target: ");
                        Position target = Screen.ReadPosition(game.Board);
                        game.ValidateTargetPos(origin, target);

                        game.ExecuteMovement(origin, target);
                    } catch (BoardException e) {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }

                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

        }
    }
}