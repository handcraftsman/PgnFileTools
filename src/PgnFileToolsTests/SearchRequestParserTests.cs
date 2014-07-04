using System.Linq;

using FluentAssert;

using NUnit.Framework;

using PgnFileTools;

namespace PgnFileToolsTests
{
    [TestFixture]
    public class SearchRequestParserTests
    {
        private PgnStreamReader _gameReader;
        private SearchRequestParser _requestParser;

        [TestFixtureSetUp]
        public void BeforeFirstTest()
        {
            _requestParser = new SearchRequestParser();
            _gameReader = new PgnStreamReader();
        }

        [Test]
        public void Given_a_request_for_header__Result__exactly_matching__1_DASH_0__should_return_a_function_that_finds_matching_games()
        {
            const string input = "Result=1-0";
            var isMatch = _requestParser.Parse(input.CreateTextReader());
            if (!isMatch.IsValid)
            {
                Assert.Fail(isMatch.ToString());
            }

            const string pgnInput = "[Result \"1-0\"]\n1.a4 1-0\n" + "[Result \"0-1\"]\n1.d4 d5 0-1\n";
            var games = _gameReader.Read(pgnInput.CreateTextReader())
                .Where(isMatch.Item)
                .ToList();

            games.Count.ShouldBeEqualTo(1);
            games.Single().Headers["Result"].ShouldBeEqualTo("1-0");
        }

        [Test]
        public void Given_a_request_for_header__Result__starting_with__1_DASH_0__should_return_a_function_that_finds_matching_games()
        {
            const string input = "Result^1-";
            var isMatch = _requestParser.Parse(input.CreateTextReader());
            if (!isMatch.IsValid)
            {
                Assert.Fail(isMatch.ToString());
            }

            const string pgnInput = "[Result \"1-0\"]\n1.a4 1-0\n" + "[Result \"0-1\"]\n1.d4 d5 0-1\n";
            var games = _gameReader.Read(pgnInput.CreateTextReader())
                .Where(isMatch.Item)
                .ToList();

            games.Count.ShouldBeEqualTo(1);
            games.Single().Headers["Result"].ShouldBeEqualTo("1-0");
        }
    }
}