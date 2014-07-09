using System;

using MvbaCore;

namespace PgnFileTools
{
    public class PieceType : NamedConstant<PieceType>
    {
        public static readonly PieceType Bishop = new PieceType("B", "B", IsLegalBishopMove);
        public static readonly PieceType King = new PieceType("K", "K", IsLegalKingMove);
        public static readonly PieceType Knight = new PieceType("N", "N", IsLegalKnightMove);
        public static readonly PieceType Pawn = new PieceType("", "P", IsLegalPawnMove);
        public static readonly PieceType Queen = new PieceType("Q", "Q", IsLegalQueenMove);
        public static readonly PieceType Rook = new PieceType("R", "R", IsLegalRookMove);

        private PieceType(string token, string symbol, Func<Position, bool, Position, bool> isLegal)
        {
            Symbol = symbol;
            IsLegal = isLegal;
            Add(token, this);
        }

        public Func<Position, bool, Position, bool> IsLegal { get; private set; }

        public string Symbol { get; private set; }

        public static PieceType GetFor(char token)
        {
            var pieceType = GetFor(token + "");
            return (pieceType != null && pieceType.Symbol[0] == token) ? pieceType : null;
        }

        public static PieceType GetForFen(char token)
        {
            var pieceType = GetFor(token + "");
            return pieceType;
        }

        private static bool IsLegalBishopMove(Position source, bool isCapture, Position destination)
        {
            return source.File != destination.File && source.Row != destination.Row;
        }

        private static bool IsLegalKingMove(Position source, bool isCapture, Position destination)
        {
            return (source.File == null || Math.Abs(source.File.Index - destination.File.Index) == 1) &&
                   (source.Row == null || Math.Abs(source.Row.Index - destination.Row.Index) == 1);
        }

        private static bool IsLegalKnightMove(Position source, bool isCapture, Position destination)
        {
            var fileDistance = source.File == null ? -10 : Math.Abs(source.File.Index - destination.File.Index);
            var rowDistance = source.Row == null ? -20 : Math.Abs(source.Row.Index - destination.Row.Index);
            return fileDistance <= 2 && rowDistance <= 2 && fileDistance != rowDistance;
        }

        private static bool IsLegalPawnMove(Position source, bool isCapture, Position destination)
        {
            if (isCapture)
            {
                return source.File != null && Math.Abs(source.File.Index - destination.File.Index) == 1;
            }
            return source.File == null;
        }

        private static bool IsLegalQueenMove(Position source, bool isCapture, Position destination)
        {
            return true; // todo
        }

        private static bool IsLegalRookMove(Position source, bool isCapture, Position destination)
        {
            return (source.File == null || source.Row == null || source.File == destination.File || source.Row == destination.Row);
        }
    }
}