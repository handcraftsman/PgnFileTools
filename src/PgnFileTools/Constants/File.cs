using MvbaCore;

namespace PgnFileTools
{
    public class File : NamedConstant<File>
    {
        public static readonly File A = new File('a', 1);
        public static readonly File B = new File('b', 2);
        public static readonly File C = new File('c', 3);
        public static readonly File D = new File('d', 4);
        public static readonly File E = new File('e', 5);
        public static readonly File F = new File('f', 6);
        public static readonly File G = new File('g', 7);
        public static readonly File H = new File('h', 8);

        private File(char token, int index)
        {
            Index = index;
            Symbol = token + "";
            Add(Symbol, this);
            Add(index+"", this);
        }

        public int Index { get; private set; }

        public string Symbol { get; private set; }

        public static File GetFor(char ch)
        {
            var file = GetFor(ch + "");
            return (file != null && file.Symbol[0] == ch) ? file : null;
        }

        public static File GetFor(int index)
        {
            return GetFor(index + "");
        }
    }
}