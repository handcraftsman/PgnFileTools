using System.IO;
using System.Text;

using FluentAssert;

using NUnit.Framework;

using PgnFileTools;

namespace PgnFileToolsTests
{
    [TestFixture]
    public class GameInfoParserTests
    {
        private GameInfoParser _parser;

        [TestFixtureSetUp]
        public void BeforeFirstTest()
        {
            _parser = new GameInfoParser();
        }
        [Test]
        public void Given_only_a_header_line__should_parse_the_header_line()
        {
            const string input = "[Result \"0-1\"]";
            var result = _parser.Parse(CreateStream(input));
            result.Headers["Result"].ShouldBeEqualTo("0-1");
            result.HasError.ShouldBeFalse();
        }

        private static StreamReader CreateStream(string input)
        {
            return new StreamReader(new MemoryStream(Encoding.ASCII.GetBytes(input)));
        }
    }
}