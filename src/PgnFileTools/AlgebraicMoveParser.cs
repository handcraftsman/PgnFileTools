using System;
using System.Linq;

namespace PgnFileTools
{
    public class AlgebraicMoveParser
    {
        private const char CaptureToken = 'x';
        private const char EnPassantCaptureTokenE = 'e';
        private const char EnPassantCaptureTokenP = 'p';

        private Func<char, Move, bool> _handle;

        private bool Done(char ch, Move move)
        {
            if (move.PieceType == PieceType.Pawn)
            {
                if (move.IsCapture && ch == EnPassantCaptureTokenE)
                {
                    _handle = ReadEnPassantCapture;
                    return true;
                }
            }

            move.ErrorMessage = "Unexpected token '" + ch + "'";
            return false;
        }

        private bool HandleCapture(Move move)
        {
            move.SourceFile = move.DestinationFile;
            move.DestinationFile = null;
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

        private bool ReadCapture(char ch, Move move)
        {
            if (ch == CaptureToken)
            {
                return HandleCapture(move);
            }
            return false;
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
            var row = Row.GetFor(ch);
            if (row != null)
            {
                move.SourceRow = row;
                _handle = ReadCapture;
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
            if (ch == CaptureToken)
            {
                return HandleCapture(move);
            }
            return false;
        }

        private bool ReadEnPassantCapture(char ch, Move move)
        {
            if (ch == EnPassantCaptureTokenP)
            {
                move.IsEnPassantCapture = true;
                _handle = Done;
                return true;
            }
            move.ErrorMessage = "Unexpected token '" + ch + "', expected '" + EnPassantCaptureTokenP + "'";
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