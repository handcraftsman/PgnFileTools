using System.Linq;

using FluentAssert;

using NUnit.Framework;

using PgnFileTools;

namespace PgnFileToolsTests
{
    public class PgnStreamReaderTests
    {
        [TestFixture]
        public class When_asked_to_read
        {
            private PgnStreamReader _reader;

            [TestFixtureSetUp]
            public void BeforeFirstTest()
            {
                _reader = new PgnStreamReader();
            }

            [Test]
            public void Given_a_stream_that_contains_1_game__should_read_the_game()
            {
                const string input = "[Result \"1-0\"]\n1.a4 1-0\n";
                var games = _reader.Read(input.CreateTextReader()).ToList();
                games.Count.ShouldBeEqualTo(1);
            }

            [Test]
            public void Given_a_stream_that_contains_2_games__should_read_the_games()
            {
                const string input = "[Result \"1-0\"]\n1.a4 1-0\n" + "[Result \"1-0\"]\n1.d4 d5 1-0\n";
                var games = _reader.Read(input.CreateTextReader()).ToList();
                games.Count.ShouldBeEqualTo(2);
            }
        }
    }
}