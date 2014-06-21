using MvbaCore;

namespace PgnFileTools
{
    public class SymbolicMoveAnnotation : NamedConstant<SymbolicMoveAnnotation>
    {
        public static readonly SymbolicMoveAnnotation PoorMove = new SymbolicMoveAnnotation("?", 2);

        private SymbolicMoveAnnotation(string symbol, int id)
        {
            Id = id;
            Add(symbol, this);
        }

        public int Id { get; private set; }
    }
}