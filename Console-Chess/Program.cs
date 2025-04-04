using Console_Chess.Board;
using System;

namespace ConsoleChess {
    internal class Program {
        static void Main(string[] args) {
            Position p = new Position(1, 5);

            Board board = new Board(8, 8);

            Console.WriteLine(p);
        }
    }
}