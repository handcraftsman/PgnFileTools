using System;

using MvbaCore;

namespace PgnFileTools
{
    public class PieceColor : NamedConstant<PieceColor>
    {
        public static readonly PieceColor White = new PieceColor("White");
        public static readonly PieceColor Black = new PieceColor("Black");

        private PieceColor(string description)
        {
            Add(description,this);
        }

        public static PieceColor GetForFen(char ch)
        {
            return Char.IsUpper(ch) ? White : Black;
        }
    }
}