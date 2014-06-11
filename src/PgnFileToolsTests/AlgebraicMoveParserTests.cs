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
        public void Should_be_able_to_parse_a_capture_where_the_piece_and_source_file_are_specified__Rhxb1()
        {
            const string move = "Rhxb1";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, PieceType.Rook, File.H, null, File.B, Row.Row1, true, false, false);
        }

        [Test]
        public void Should_be_able_to_parse_a_capture_where_the_piece_and_source_row_are_specified__R8xa4()
        {
            const string move = "R8xa4";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, PieceType.Rook, null, Row.Row8, File.A, Row.Row4, true, false, false);
        }

        [Test]
        public void Should_be_able_to_parse_a_capture_where_the_piece_is_specified__Nxb2()
        {
            const string move = "Nxb2";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, PieceType.Knight, null, null, File.B, Row.Row2, true, false, false);
        }

        [Test]
        public void Should_be_able_to_parse_a_move_where_the_piece_is_specified__Nc3()
        {
            const string move = "Nc3";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, PieceType.Knight, null, null, File.C, Row.Row3, false, false, false);
        }

        [Test]
        public void Should_be_able_to_parse_a_pawn_move__a4()
        {
            const string move = "a4";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, PieceType.Pawn, null, null, File.A, Row.Row4, false, false, false);
        }

        [Test]
        public void Should_be_able_to_parse_pawn_capture__hxg6()
        {
            const string move = "hxg6";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, PieceType.Pawn, File.H, null, File.G, Row.Row6, true, false, false);
        }

        [Test]
        public void Should_be_able_to_parse_pawn_capture_en_passant__fxg3ep()
        {
            const string move = "fxg3ep";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, PieceType.Pawn, File.F, null, File.G, Row.Row3, true, true, false);
        }

        private static void Verify(Move move, PieceType pieceType, File sourceFile, Row sourceRow, File destinationFile, Row destinationRow, bool isCapture, bool isEnPassantCapture, bool hasError)
        {
            move.PieceType.ShouldBeEqualTo(pieceType, "piece type");
            move.SourceFile.ShouldBeEqualTo(sourceFile, "source file");
            move.SourceRow.ShouldBeEqualTo(sourceRow, "source row");
            move.DestinationFile.ShouldBeEqualTo(destinationFile, "destination file");
            move.DestinationRow.ShouldBeEqualTo(destinationRow, "destination row");
            move.IsCapture.ShouldBeEqualTo(isCapture, "is capture");
            move.IsEnPassantCapture.ShouldBeEqualTo(isEnPassantCapture, "is en passant capture");
            move.HasError.ShouldBeEqualTo(hasError, "has error");
        }
    }
}