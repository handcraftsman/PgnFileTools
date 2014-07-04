using System.Collections.Generic;
using System.IO;

namespace PgnFileTools
{
    public class PgnStreamReader
    {
        public IEnumerable<GameInfo> Read(TextReader reader)
        {
            var parser = new GameInfoParser();
            while (reader.Peek() != -1)
            {
                var game = parser.Parse(reader);
                yield return game;
            }
        }
    }
}