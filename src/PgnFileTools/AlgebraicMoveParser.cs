using System;
using System.Linq;

namespace PgnFileTools
{
    public class AlgebraicMoveParser
    {
        private const char CaptureToken = 'x';
        private const char CastleDashToken = '-';
        private const char CastleToken = 'O';
        private const char CheckToken = '+';
        private const char EnPassantCaptureTokenE = 'e';
        private const char EnPassantCaptureTokenP = 'p';
        private const char MateToken = '#';
        private const char PromotionToken = '=';

        private Func<char, Move, bool> _handle;

        private bool Done(char ch, Move move)
        {
            if (ch == CheckToken)
            {
                return HandleCheck(move);
            }

            if (ch == MateToken)
            {
                return HandleMate(move);
            }

            if (move.PieceType == PieceType.Pawn)
            {
                if (move.DestinationRow.IsPromotionRow)
                {
                    return ReadPromotion(ch, move);
                }
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

        private static bool HandleCheck(Move move)
        {
            if (move.IsCheck)
            {
                move.IsDoubleCheck = true;
            }
            else
            {
                move.IsCheck = true;
            }
            return true;
        }

        private static bool HandleMate(Move move)
        {
            move.IsMate = true;
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
            if (!new Func<char, Move, bool>[] { Done, ReadQueenCastleDash }.Contains(_handle))
            {
                move.HasError = true;
                move.ErrorMessage = "Unexpected end of move text.";
            }
            return move;
        }

        private bool ReadCastleEnd(char ch, Move move)
        {
            if (ch == CastleToken)
            {
                _handle = ReadQueenCastleDash;
                return true;
            }
            move.ErrorMessage = "Unexpected character '" + ch + "' reading Castle";
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
                return true;
            }
            if (ch == CaptureToken)
            {
                return HandleCapture(move);
            }
            if (ch == CastleToken)
            {
                _handle = ReadKingCastleDash;
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
            var file = File.GetFor(ch);
            if (file != null)
            {
                move.SourceFile = move.DestinationFile;
                move.DestinationFile = file;
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

        private bool ReadKingCastleDash(char ch, Move move)
        {
            if (ch == CastleDashToken)
            {
                move.IsCastle = true;
                move.CastleType = CastleType.KingSide;
                _handle = ReadCastleEnd;
                return true;
            }
            move.ErrorMessage = "Unexpected character '" + ch + "' reading Castle";
            return false;
        }

        private bool ReadPiece(char ch, Move move)
        {
            if (ch == CastleToken)
            {
                _handle = ReadKingCastleDash;
                return true;
            }
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

        private bool ReadPromotion(char ch, Move move)
        {
            if (ch == PromotionToken)
            {
                _handle = ReadPromotionPiece;
                return true;
            }
            return ReadPromotionPiece(ch, move);
        }

        private bool ReadPromotionPiece(char ch, Move move)
        {
            var piece = PieceType.GetFor(ch);
            if (piece != null)
            {
                move.IsPromotion = true;
                move.PromotionPiece = piece;
                _handle = Done;
                return true;
            }
            move.ErrorMessage = "Unexpected token '" + ch + "' reading promotion.";
            return false;
        }

        private bool ReadQueenCastleDash(char ch, Move move)
        {
            if (ch == CastleDashToken)
            {
                move.CastleType = CastleType.QueenSide;
                _handle = ReadCastleEnd;
                return true;
            }
            _handle = Done;
            return Done(ch, move);
        }
    }
}