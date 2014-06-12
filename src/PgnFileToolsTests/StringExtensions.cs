using System;
using System.Collections.Generic;

namespace PgnFileToolsTests
{
    public static class StringExtensions
    {
        private static readonly Random random = new Random();

        public static IEnumerable<char> Generate(this string source)
        {
            while (true)
            {
                yield return source[random.Next(source.Length)];
            }
        }
    }
}