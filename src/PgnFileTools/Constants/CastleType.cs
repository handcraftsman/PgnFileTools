using MvbaCore;

namespace PgnFileTools
{
    public class CastleType : NamedConstant<CastleType>
    {
        public static readonly CastleType KingSide = new CastleType("O-O");
        public static readonly CastleType QueenSide = new CastleType("O-O-O");

        private CastleType(string symbol)
        {
            Symbol = symbol;
            Add(symbol, this);
        }

        public string Symbol { get; private set; }
    }
}