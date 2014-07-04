using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PgnFileToolsTests
{
    public static class StringExtensions
    {
        private static readonly Random random = new Random();

        public static StreamReader CreateStream(this string input)
        {
            return new StreamReader(new MemoryStream(Encoding.ASCII.GetBytes(input)));
        }

        public static IEnumerable<char> Generate(this string source)
        {
            while (true)
            {
                yield return source[random.Next(source.Length)];
            }
        }
    }
}