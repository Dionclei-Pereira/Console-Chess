using Console_Chess.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess.Chess {
    internal class ThreeCheck : ChessGame {

        public int PlayerOneChecks { get; protected set; } = 0;
        public int PlayerTwoChecks { get; protected set; } = 0;

        public ThreeCheck(Color colorOne, Color colorTwo) : base(colorOne, colorTwo) {
        }

        public override void TestingChecks() {
            base.TestingChecks();
            if (IsGameInCheck) {
                if (Playing == PlayerOne) {
                    PlayerTwoChecks++;
                } else {
                    PlayerOneChecks++;
                }
                if (PlayerOneChecks == 3 || PlayerTwoChecks == 3) {
                    Ended = true;
                }
            }
        }
    }
}
