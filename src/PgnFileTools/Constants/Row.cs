using MvbaCore;

namespace PgnFileTools
{
    public class Row : NamedConstant<Row>
    {
        public static readonly Row Row1 = new Row('1');
        public static readonly Row Row2 = new Row('2');
        public static readonly Row Row3 = new Row('3');
        public static readonly Row Row4 = new Row('4');
        public static readonly Row Row5 = new Row('5');
        public static readonly Row Row6 = new Row('6');
        public static readonly Row Row7 = new Row('7');
        public static readonly Row Row8 = new Row('8');

        private Row(char token)
        {
            Symbol = token + "";
            Add(Symbol, this);
        }

        public string Symbol { get; private set; }

        public static Row GetFor(char ch)
        {
            return GetFor(ch + "");
        }
    }
}