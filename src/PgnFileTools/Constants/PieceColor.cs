using System;

using MvbaCore;

namespace PgnFileTools
{
    public class PieceColor : NamedConstant<PieceColor>
    {
        public static readonly PieceColor Black = new PieceColor("b", "Black");
        public static readonly PieceColor White = new PieceColor("w", "White");

        private PieceColor(string symbol, string description)
        {
            Symbol = symbol;
            Description = description;
            Add(symbol, this);
        }

        public string Description { get; private set; }
        public string Symbol { get; private set; }

        public static PieceColor GetFor(char key)
        {
            return GetFor(key + "");
        }

        public static PieceColor GetForFen(char ch)
        {
            return Char.IsUpper(ch) ? White : Black;
        }
    }
}