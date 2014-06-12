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
        public void Should_be_able_to_parse_King_side_castle__O_DASH_O()
        {
            const string move = "O-O";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, null, null, null, null, null, false, false, false, null, true, CastleType.KingSide, false, false, false, false);
        }

        [Test]
        public void Should_be_able_to_parse_Queen_side_castle__O_DASH_O_DASH_O()
        {
            const string move = "O-O-O";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, null, null, null, null, null, false, false, false, null, true, CastleType.QueenSide, false, false, false, false);
        }

        [Test]
        public void Should_be_able_to_parse_a_capture_where_the_piece_and_source_file_are_specified__Rhxb1()
        {
            const string move = "Rhxb1";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, PieceType.Rook, File.H, null, File.B, Row.Row1, true, false, false, null, false, null, false, false, false, false);
        }

        [Test]
        public void Should_be_able_to_parse_a_capture_where_the_piece_and_source_row_are_specified__R8xa4()
        {
            const string move = "R8xa4";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, PieceType.Rook, null, Row.Row8, File.A, Row.Row4, true, false, false, null, false, null, false, false, false, false);
        }

        [Test]
        public void Should_be_able_to_parse_a_capture_where_the_piece_is_specified__Nxb2()
        {
            const string move = "Nxb2";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, PieceType.Knight, null, null, File.B, Row.Row2, true, false, false, null, false, null, false, false, false, false);
        }

        [Test]
        public void Should_be_able_to_parse_a_move_that_checks_the_King__f5_PLUS()
        {
            const string move = "f5+";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, PieceType.Pawn, null, null, File.F, Row.Row5, false, false, false, null, false, null, true, false, false, false);
        }

        [Test]
        public void Should_be_able_to_parse_a_move_that_double_checks_the_King__g1Q_PLUS_PLUS()
        {
            const string move = "f1N++";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, PieceType.Pawn, null, null, File.F, Row.Row1, false, false, true, PieceType.Knight, false, null, true, true, false, false);
        }

        [Test]
        public void Should_be_able_to_parse_a_move_that_mates_the_King__a2_SHARP()
        {
            const string move = "a2#";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, PieceType.Pawn, null, null, File.A, Row.Row2, false, false, false, null, false, null, false, false, true, false);
        }

        [Test]
        public void Should_be_able_to_parse_a_move_where_the_piece_and_file_are_specified__Rab3()
        {
            const string move = "Rab3";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, PieceType.Rook, File.A, null, File.B, Row.Row3, false, false, false, null, false, null, false, false, false, false);
        }

        [Test]
        public void Should_be_able_to_parse_a_move_where_the_piece_and_row_are_specified__R6d4()
        {
            const string move = "R6d4";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, PieceType.Rook, null, Row.Row6, File.D, Row.Row4, false, false, false, null, false, null, false, false, false, false);
        }

        [Test]
        public void Should_be_able_to_parse_a_move_where_the_piece_is_specified__Nc3()
        {
            const string move = "Nc3";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, PieceType.Knight, null, null, File.C, Row.Row3, false, false, false, null, false, null, false, false, false, false);
        }

        [Test]
        public void Should_be_able_to_parse_a_pawn_move__a4()
        {
            const string move = "a4";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, PieceType.Pawn, null, null, File.A, Row.Row4, false, false, false, null, false, null, false, false, false, false);
        }

        [Test]
        public void Should_be_able_to_parse_pawn_capture__hxg6()
        {
            const string move = "hxg6";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, PieceType.Pawn, File.H, null, File.G, Row.Row6, true, false, false, null, false, null, false, false, false, false);
        }

        [Test]
        public void Should_be_able_to_parse_pawn_capture_and_promotion__exd8_EQUAL_Q()
        {
            const string move = "exd8=Q";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, PieceType.Pawn, File.E, null, File.D, Row.Row8, true, false, true, PieceType.Queen, false, null, false, false, false, false);
        }

        [Test]
        public void Should_be_able_to_parse_pawn_capture_en_passant__fxg3ep()
        {
            const string move = "fxg3ep";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, PieceType.Pawn, File.F, null, File.G, Row.Row3, true, true, false, null, false, null, false, false, false, false);
        }

        [Test]
        public void Should_be_able_to_parse_pawn_promotion__b1_EQUAL_Q_PLUS()
        {
            const string move = "b1=Q+";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, PieceType.Pawn, null, null, File.B, Row.Row1, false, false, true, PieceType.Queen, false, null, true, false, false, false);
        }

        [Test]
        public void Should_be_able_to_parse_pawn_promotion__c1Q()
        {
            const string move = "c1Q";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, PieceType.Pawn, null, null, File.C, Row.Row1, false, false, true, PieceType.Queen, false, null, false, false, false, false);
        }

        [Test]
        public void Should_be_able_to_parse_pawn_promotion__c1_EQUAL_Q()
        {
            const string move = "c1=Q";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, PieceType.Pawn, null, null, File.C, Row.Row1, false, false, true, PieceType.Queen, false, null, false, false, false, false);
        }

        [Test]
        public void Should_set_HasError_to__true__for_an_incomplete_move()
        {
            const string move = "c";
            var algebraic = _parser.Parse(move);
            algebraic.HasError.ShouldBeTrue();
            algebraic.ErrorMessage.ShouldNotBeNullOrEmpty();
        }

        private static void Verify(Move move, PieceType pieceType, File sourceFile, Row sourceRow, File destinationFile, Row destinationRow, bool isCapture, bool isEnPassantCapture, bool isPromotion, PieceType promotionPiece, bool isCastle, CastleType castleType, bool isCheck, bool isDoubleCheck, bool isMate, bool hasError)
        {
            move.HasError.ShouldBeEqualTo(hasError, "has error");
            move.PieceType.ShouldBeEqualTo(pieceType, "piece type");
            move.SourceFile.ShouldBeEqualTo(sourceFile, "source file");
            move.SourceRow.ShouldBeEqualTo(sourceRow, "source row");
            move.DestinationFile.ShouldBeEqualTo(destinationFile, "destination file");
            move.DestinationRow.ShouldBeEqualTo(destinationRow, "destination row");
            move.IsCapture.ShouldBeEqualTo(isCapture, "is capture");
            move.IsEnPassantCapture.ShouldBeEqualTo(isEnPassantCapture, "is en passant capture");
            move.IsPromotion.ShouldBeEqualTo(isPromotion, "is promotion");
            move.PromotionPiece.ShouldBeEqualTo(promotionPiece, "promotion piece");
            move.IsCastle.ShouldBeEqualTo(isCastle, "is castle");
            move.CastleType.ShouldBeEqualTo(castleType, "castle type");
            move.IsCheck.ShouldBeEqualTo(isCheck, "is check");
            move.IsDoubleCheck.ShouldBeEqualTo(isDoubleCheck, "is double check");
            move.IsMate.ShouldBeEqualTo(isMate, "is mate");
        }
    }
}