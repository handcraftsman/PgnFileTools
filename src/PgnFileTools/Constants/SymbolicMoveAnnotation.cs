using MvbaCore;

namespace PgnFileTools
{
    public class SymbolicMoveAnnotation : NamedConstant<SymbolicMoveAnnotation>
    {
        public static readonly SymbolicMoveAnnotation GoodMove = new SymbolicMoveAnnotation("!", 1);
        public static readonly SymbolicMoveAnnotation PoorMove = new SymbolicMoveAnnotation("?", 2);
        public static readonly SymbolicMoveAnnotation VeryPoorMove = new SymbolicMoveAnnotation("??", 4);

        private SymbolicMoveAnnotation(string symbol, int id)
        {
            Id = id;
            Add(symbol, this);
        }

        public int Id { get; private set; }
    }
}