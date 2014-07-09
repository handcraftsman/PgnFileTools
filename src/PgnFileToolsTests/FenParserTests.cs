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
        public void Given__1n6__on_row_8__should_correctly_decode_the_piece()
        {
            const string input = "1n6/8/8/8/8/8/8/8 w - - 0 1";
            var position = _parser.Parse(input);
            position.Black.Count.ShouldBeEqualTo(1);
            var knight = position.Black.Single();
            knight.PieceColor.ShouldBeEqualTo(PieceColor.Black);
            knight.Position.File.ShouldBeEqualTo(File.B);
            knight.Position.Row.ShouldBeEqualTo(Row.Row8);
        }

        [Test]
        public void Given__2bq4__on_row_8__should_correctly_decode_the_pieces()
        {
            const string input = "2bq4/8/8/8/8/8/8/8 w - - 0 1";
            var position = _parser.Parse(input);
            position.Black.Count.ShouldBeEqualTo(2);

            var bishops = position.Black.Where(x => x.PieceType == PieceType.Bishop).ToList();
            bishops.Count.ShouldBeEqualTo(1);
            var bishop = bishops.Single();
            bishop.PieceColor.ShouldBeEqualTo(PieceColor.Black);
            bishop.Position.File.ShouldBeEqualTo(File.C);
            bishop.Position.Row.ShouldBeEqualTo(Row.Row8);

            var queens = position.Black.Where(x => x.PieceType == PieceType.Queen).ToList();
            queens.Count.ShouldBeEqualTo(1);
            var queen = queens.Single();
            queen.PieceColor.ShouldBeEqualTo(PieceColor.Black);
            queen.Position.File.ShouldBeEqualTo(File.D);
            queen.Position.Row.ShouldBeEqualTo(Row.Row8);
        }

        [Test]
        public void Given__k7__on_row_8__should_correctly_decode_the_piece()
        {
            const string input = "k7/8/8/8/8/8/8/8 w - - 0 1";
            var position = _parser.Parse(input);
            position.Black.Count.ShouldBeEqualTo(1);
            var king = position.Black.Single();
            king.PieceColor.ShouldBeEqualTo(PieceColor.Black);
            king.Position.File.ShouldBeEqualTo(File.A);
            king.Position.Row.ShouldBeEqualTo(Row.Row8);
        }
    }
}