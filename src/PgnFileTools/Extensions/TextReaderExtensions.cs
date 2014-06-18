using System.Collections.Generic;
using System.IO;

namespace PgnFileTools.Extensions
{
    public static class TextReaderExtensions
    {
        public static IEnumerable<char> GenerateFrom(this TextReader source)
        {
            while (source.Peek() != -1)
            {
                yield return (char)source.Read();
            }
        }
    }
}