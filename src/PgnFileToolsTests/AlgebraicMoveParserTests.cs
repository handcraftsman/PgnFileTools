using FluentAssert;

using NUnit.Framework;

using PgnFileTools;

namespace PgnFileToolsTests
{
    [TestFixture]
    public class AlgebraicMoveParserTests
    {
        private AlgebraicMoveParser _parser;

        [TestFixtureSetUp]
        public void BeforeFirstTest()
        {
            _parser = new AlgebraicMoveParser();
        }

        [Test]
        public void Should_be_able_to_parse_a_capture_where_the_piece_is_specified__Nxb2()
        {
            const string move = "Nxb2";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, PieceType.Knight, null, File.B, Row.Row2, true, false);
        }

        [Test]
        public void Should_be_able_to_parse_a_move_where_the_piece_is_specified__Nc3()
        {
            const string move = "Nc3";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, PieceType.Knight, null, File.C, Row.Row3, false, false);
        }

        [Test]
        public void Should_be_able_to_parse_a_pawn_move__a4()
        {
            const string move = "a4";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, PieceType.Pawn, null, File.A, Row.Row4, false, false);
        }

        [Test]
        public void Should_be_able_to_parse_pawn_capture__hxg6()
        {
            const string move = "hxg6";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, PieceType.Pawn, File.H, File.G, Row.Row6, true, false);
        }

        private static void Verify(Move move, PieceType pieceType, File sourceFile, File destinationFile, Row destinationRow, bool isCapture, bool hasError)
        {
            move.PieceType.ShouldBeEqualTo(pieceType, "piece type");
            move.SourceFile.ShouldBeEqualTo(sourceFile, "source file");
            move.DestinationFile.ShouldBeEqualTo(destinationFile, "destination file");
            move.DestinationRow.ShouldBeEqualTo(destinationRow, "destination row");
            move.IsCapture.ShouldBeEqualTo(isCapture, "is capture");
            move.HasError.ShouldBeEqualTo(hasError, "has error");
        }
    }
}