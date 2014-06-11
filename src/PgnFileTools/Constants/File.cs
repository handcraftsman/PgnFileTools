using MvbaCore;

namespace PgnFileTools
{
    public class File : NamedConstant<File>
    {
        public static readonly File A = new File('a');
        public static readonly File B = new File('b');
        public static readonly File C = new File('c');
        public static readonly File D = new File('d');
        public static readonly File E = new File('e');
        public static readonly File F = new File('f');
        public static readonly File G = new File('g');
        public static readonly File H = new File('h');

        private File(char token)
        {
            Symbol = token + "";
            Add(Symbol, this);
        }

        public string Symbol { get; private set; }

        public static File GetFor(char ch)
        {
            return GetFor(ch + "");
        }
    }
}