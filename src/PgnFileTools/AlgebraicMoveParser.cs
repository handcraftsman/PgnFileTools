using System;
using System.Linq;

namespace PgnFileTools
{
    public class AlgebraicMoveParser
    {
        private Func<char, Move, bool> _handle;

        private bool Done(char ch, Move move)
        {
            return false;
        }

        public Move Parse(string algebraicMove)
        {
            _handle = ReadDestinationFile;
            var move = new Move
                {
                    PieceType = PieceType.Pawn
                };
            if (algebraicMove.Select(ch => _handle(ch, move)).Any(success => !success))
            {
                move.HasError = true;
            }
            return move;
        }

        private bool ReadDestinationFile(char ch, Move move)
        {
            var file = File.GetFor(ch);
            if (file != null)
            {
                move.DestinationFile = file;
                _handle = ReadDestinationRow;
                return true;
            }
            return false;
        }

        private bool ReadDestinationRow(char ch, Move move)
        {
            var row = Row.GetFor(ch);
            if (row != null)
            {
                move.DestinationRow = row;
                _handle = Done;
                return true;
            }
            return false;
        }
    }
}