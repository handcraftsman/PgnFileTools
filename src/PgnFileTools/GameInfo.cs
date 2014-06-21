using System.Collections.Generic;

namespace PgnFileTools
{
    public class GameInfo
    {
        public GameInfo()
        {
            Headers = new Dictionary<string, string>();
            Moves = new List<Move>();
        }

        public string ErrorMessage { get; set; }
        public bool HasError { get; set; }
        public IDictionary<string, string> Headers { get; private set; }
        public IList<Move> Moves { get; private set; }
    }
}