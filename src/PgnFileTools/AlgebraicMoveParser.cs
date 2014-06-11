using System;
using System.Linq;

namespace PgnFileTools
{
    public class AlgebraicMoveParser
    {
        private const char CaptureToken = 'x';
        private Func<char, Move, bool> _handle;

        private bool Done(char ch, Move move)
        {
            return false;
        }

        private bool HandleCapture(Move move)
        {
            _handle = ReadDestinationFile;
            move.IsCapture = true;
            return true;
        }

        public Move Parse(string algebraicMove)
        {
            _handle = ReadPiece;
            var move = new Move();
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
            if (ch == CaptureToken)
            {
                return HandleCapture(move);
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

        private bool ReadPiece(char ch, Move move)
        {
            var piece = PieceType.GetFor(ch);
            if (piece != null)
            {
                move.PieceType = piece;
                _handle = ReadDestinationFile;
                return true;
            }
            move.PieceType = PieceType.Pawn;
            _handle = ReadDestinationFile;
            return ReadDestinationFile(ch, move);
        }
    }
}