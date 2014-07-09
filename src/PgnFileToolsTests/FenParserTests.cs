using System.Linq;

using FluentAssert;

using NUnit.Framework;

using PgnFileTools;

namespace PgnFileToolsTests
{
    [TestFixture]
    public class FenParserTests
    {
        private FenParser _parser;

        [TestFixtureSetUp]
        public void BeforeFirstTest()
        {
            _parser = new FenParser();
        }

        [Test]
        public void Given_a_partial_position_with_black_king_on_A8__should_correctly_decode_the_king()
        {
            const string input = "k7/8/8/8/8/8/8/8 w - - 0 1";
            var position = _parser.Parse(input);
            var kings = position.Black.Where(x => x.PieceType == PieceType.King).ToList();
            kings.Count.ShouldBeEqualTo(1);
            var king = kings.Single();
            king.PieceColor.ShouldBeEqualTo(PieceColor.Black);
            king.Position.File.ShouldBeEqualTo(File.A);
            king.Position.Row.ShouldBeEqualTo(Row.Row8);
        }
    }
}