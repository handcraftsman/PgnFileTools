using MvbaCore;

namespace PgnFileTools
{
    public class PieceType : NamedConstant<PieceType>
    {
        public static readonly PieceType Bishop = new PieceType("B", "B");
        public static readonly PieceType King = new PieceType("K", "K");
        public static readonly PieceType Knight = new PieceType("N", "N");
        public static readonly PieceType Pawn = new PieceType("", "P");
        public static readonly PieceType Queen = new PieceType("Q", "Q");
        public static readonly PieceType Rook = new PieceType("R", "R");

        private PieceType(string token, string symbol)
        {
            Symbol = symbol;
            Add(token, this);
        }

        public string Symbol { get; private set; }

        public static PieceType GetFor(char token)
        {
            return GetFor(token + "");
        }
    }
}