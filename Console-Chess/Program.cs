using Console_Chess.Board;
using Console_Chess.Chess;

namespace Console_Chess {
    internal class Program {
        static void Main(string[] args) {
            Position p = new Position(1, 5);
            GameBoard board = new GameBoard(8, 8);
            Screen.PrintBoard(board);
        }
    }
}