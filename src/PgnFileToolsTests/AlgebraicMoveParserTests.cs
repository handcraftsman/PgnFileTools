using System;
using System.Linq;
using System.Text.RegularExpressions;

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
        [Explicit]
        public void FuzzIt()
        {
            var regex = new Regex(@"^(([NBRQK]?[a-h]?[1-8]?x?[a-h][1-8])|([a-h]?[1-8]?x?[a-h][1-8](?:\=[NBRQK]))|(O(-?O){1,2}))(\+{1,2}|#)?$");
            const string possibleCharacters = "NBQKRabcdefgh12345678xO-+#";
            var random = new Random();

            Func<string> generateMove = () => new String(possibleCharacters.Generate().Take(random.Next(2, 10)).ToArray());

            var anyFailed = false;
            for (var i = 0; i < 10000; i++)
            {
                var move = generateMove();
                try
                {
                    var result = _parser.Parse(move);
                    if (!result.HasError && !regex.IsMatch(move))
                    {
                        Console.WriteLine(move);
                        anyFailed = true;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(move);
                    anyFailed = true;
                    Console.WriteLine(e);
                }
            }
            anyFailed.ShouldBeFalse();
        }

        [Test]
        public void Given_a_destination_followed_by_a_partial_castle__c3_DASH_O__Should_set_HasError_to__true()
        {
            const string move = "c3-O";
            var algebraic = _parser.Parse(move);
            algebraic.HasError.ShouldBeTrue();
            algebraic.ErrorMessage.ShouldNotBeNullOrEmpty();
        }

        [Test]
        public void Given_and_incomplete_move__Should_set_HasError_to__true()
        {
            const string move = "c";
            var algebraic = _parser.Parse(move);
            algebraic.HasError.ShouldBeTrue();
            algebraic.ErrorMessage.ShouldNotBeNullOrEmpty();
        }

        [Test]
        public void Should_be_able_to_parse_King_side_castle__O_DASH_O()
        {
            const string move = "O-O";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, new Move
                {
                    PieceType = null,
                    SourceFile = null,
                    SourceRow = null,
                    DestinationFile = null,
                    DestinationRow = null,
                    IsCapture = false,
                    IsEnPassantCapture = false,
                    IsPromotion = false,
                    PromotionPiece = null,
                    IsCastle = true,
                    CastleType = CastleType.KingSide,
                    IsCheck = false,
                    IsDoubleCheck = false,
                    IsMate = false,
                    HasError = false
                });
        }

        [Test]
        public void Should_be_able_to_parse_Queen_side_castle__O_DASH_O_DASH_O()
        {
            const string move = "O-O-O";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, new Move
                {
                    PieceType = null,
                    SourceFile = null,
                    SourceRow = null,
                    DestinationFile = null,
                    DestinationRow = null,
                    IsCapture = false,
                    IsEnPassantCapture = false,
                    IsPromotion = false,
                    PromotionPiece = null,
                    IsCastle = true,
                    CastleType = CastleType.QueenSide,
                    IsCheck = false,
                    IsDoubleCheck = false,
                    IsMate = false,
                    HasError = false
                });
        }

        [Test]
        public void Should_be_able_to_parse_a_capture_where_the_piece_and_source_file_are_specified__Rhxb1()
        {
            const string move = "Rhxb1";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, new Move
                {
                    PieceType = PieceType.Rook,
                    SourceFile = File.H,
                    SourceRow = null,
                    DestinationFile = File.B,
                    DestinationRow = Row.Row1,
                    IsCapture = true,
                    IsEnPassantCapture = false,
                    IsPromotion = false,
                    PromotionPiece = null,
                    IsCastle = false,
                    CastleType = null,
                    IsCheck = false,
                    IsDoubleCheck = false,
                    IsMate = false,
                    HasError = false
                });
        }

        [Test]
        public void Should_be_able_to_parse_a_capture_where_the_piece_and_source_row_are_specified__R8xa4()
        {
            const string move = "R8xa4";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, new Move
                {
                    PieceType = PieceType.Rook,
                    SourceFile = null,
                    SourceRow = Row.Row8,
                    DestinationFile = File.A,
                    DestinationRow = Row.Row4,
                    IsCapture = true,
                    IsEnPassantCapture = false,
                    IsPromotion = false,
                    PromotionPiece = null,
                    IsCastle = false,
                    CastleType = null,
                    IsCheck = false,
                    IsDoubleCheck = false,
                    IsMate = false,
                    HasError = false
                });
        }

        [Test]
        public void Should_be_able_to_parse_a_capture_where_the_piece_is_specified__Nxb2()
        {
            const string move = "Nxb2";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, new Move
                {
                    PieceType = PieceType.Knight,
                    SourceFile = null,
                    SourceRow = null,
                    DestinationFile = File.B,
                    DestinationRow = Row.Row2,
                    IsCapture = true,
                    IsEnPassantCapture = false,
                    IsPromotion = false,
                    PromotionPiece = null,
                    IsCastle = false,
                    CastleType = null,
                    IsCheck = false,
                    IsDoubleCheck = false,
                    IsMate = false,
                    HasError = false
                });
        }

        [Test]
        public void Should_be_able_to_parse_a_move_that_checks_the_King__f5_PLUS()
        {
            const string move = "f5+";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, new Move
                {
                    PieceType = PieceType.Pawn,
                    SourceFile = null,
                    SourceRow = null,
                    DestinationFile = File.F,
                    DestinationRow = Row.Row5,
                    IsCapture = false,
                    IsEnPassantCapture = false,
                    IsPromotion = false,
                    PromotionPiece = null,
                    IsCastle = false,
                    CastleType = null,
                    IsCheck = true,
                    IsDoubleCheck = false,
                    IsMate = false,
                    HasError = false
                });
        }

        [Test]
        public void Should_be_able_to_parse_a_move_that_double_checks_the_King__g1Q_PLUS_PLUS()
        {
            const string move = "f1N++";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, new Move
                {
                    PieceType = PieceType.Pawn,
                    SourceFile = null,
                    SourceRow = null,
                    DestinationFile = File.F,
                    DestinationRow = Row.Row1,
                    IsCapture = false,
                    IsEnPassantCapture = false,
                    IsPromotion = true,
                    PromotionPiece = PieceType.Knight,
                    IsCastle = false,
                    CastleType = null,
                    IsCheck = true,
                    IsDoubleCheck = true,
                    IsMate = false,
                    HasError = false
                });
        }

        [Test]
        public void Should_be_able_to_parse_a_move_that_mates_the_King__a2_SHARP()
        {
            const string move = "a2#";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, new Move
                {
                    PieceType = PieceType.Pawn,
                    SourceFile = null,
                    SourceRow = null,
                    DestinationFile = File.A,
                    DestinationRow = Row.Row2,
                    IsCapture = false,
                    IsEnPassantCapture = false,
                    IsPromotion = false,
                    PromotionPiece = null,
                    IsCastle = false,
                    CastleType = null,
                    IsCheck = false,
                    IsDoubleCheck = false,
                    IsMate = true,
                    HasError = false
                });
        }

        [Test]
        public void Should_be_able_to_parse_a_move_where_the_piece_and_file_are_specified__Rab3()
        {
            const string move = "Rab3";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, new Move
                {
                    PieceType = PieceType.Rook,
                    SourceFile = File.A,
                    SourceRow = null,
                    DestinationFile = File.B,
                    DestinationRow = Row.Row3,
                    IsCapture = false,
                    IsEnPassantCapture = false,
                    IsPromotion = false,
                    PromotionPiece = null,
                    IsCastle = false,
                    CastleType = null,
                    IsCheck = false,
                    IsDoubleCheck = false,
                    IsMate = false,
                    HasError = false
                });
        }

        [Test]
        public void Should_be_able_to_parse_a_move_where_the_piece_and_row_are_specified__R6d4()
        {
            const string move = "R6d4";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, new Move
                {
                    PieceType = PieceType.Rook,
                    SourceFile = null,
                    SourceRow = Row.Row6,
                    DestinationFile = File.D,
                    DestinationRow = Row.Row4,
                    IsCapture = false,
                    IsEnPassantCapture = false,
                    IsPromotion = false,
                    PromotionPiece = null,
                    IsCastle = false,
                    CastleType = null,
                    IsCheck = false,
                    IsDoubleCheck = false,
                    IsMate = false,
                    HasError = false
                });
        }

        [Test]
        public void Should_be_able_to_parse_a_move_where_the_piece_is_specified__Nc3()
        {
            const string move = "Nc3";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, new Move
                {
                    PieceType = PieceType.Knight,
                    SourceFile = null,
                    SourceRow = null,
                    DestinationFile = File.C,
                    DestinationRow = Row.Row3,
                    IsCapture = false,
                    IsEnPassantCapture = false,
                    IsPromotion = false,
                    PromotionPiece = null,
                    IsCastle = false,
                    CastleType = null,
                    IsCheck = false,
                    IsDoubleCheck = false,
                    IsMate = false,
                    HasError = false
                });
        }

        [Test]
        public void Should_be_able_to_parse_a_pawn_move__a4()
        {
            const string move = "a4";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, new Move
                {
                    PieceType = PieceType.Pawn,
                    SourceFile = null,
                    SourceRow = null,
                    DestinationFile = File.A,
                    DestinationRow = Row.Row4,
                    IsCapture = false,
                    IsEnPassantCapture = false,
                    IsPromotion = false,
                    PromotionPiece = null,
                    IsCastle = false,
                    CastleType = null,
                    IsCheck = false,
                    IsDoubleCheck = false,
                    IsMate = false,
                    HasError = false
                });
        }

        [Test]
        public void Should_be_able_to_parse_pawn_capture__hxg6()
        {
            const string move = "hxg6";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, new Move
                {
                    PieceType = PieceType.Pawn,
                    SourceFile = File.H,
                    SourceRow = null,
                    DestinationFile = File.G,
                    DestinationRow = Row.Row6,
                    IsCapture = true,
                    IsEnPassantCapture = false,
                    IsPromotion = false,
                    PromotionPiece = null,
                    IsCastle = false,
                    CastleType = null,
                    IsCheck = false,
                    IsDoubleCheck = false,
                    IsMate = false,
                    HasError = false
                });
        }

        [Test]
        public void Should_be_able_to_parse_pawn_capture_and_promotion__exd8_EQUAL_Q()
        {
            const string move = "exd8=Q";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, new Move
                {
                    PieceType = PieceType.Pawn,
                    SourceFile = File.E,
                    SourceRow = null,
                    DestinationFile = File.D,
                    DestinationRow = Row.Row8,
                    IsCapture = true,
                    IsEnPassantCapture = false,
                    IsPromotion = true,
                    PromotionPiece = PieceType.Queen,
                    IsCastle = false,
                    CastleType = null,
                    IsCheck = false,
                    IsDoubleCheck = false,
                    IsMate = false,
                    HasError = false
                });
        }

        [Test]
        public void Should_be_able_to_parse_pawn_capture_en_passant__fxg3ep()
        {
            const string move = "fxg3ep";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, new Move
                {
                    PieceType = PieceType.Pawn,
                    SourceFile = File.F,
                    SourceRow = null,
                    DestinationFile = File.G,
                    DestinationRow = Row.Row3,
                    IsCapture = true,
                    IsEnPassantCapture = true,
                    IsPromotion = false,
                    PromotionPiece = null,
                    IsCastle = false,
                    CastleType = null,
                    IsCheck = false,
                    IsDoubleCheck = false,
                    IsMate = false,
                    HasError = false
                });
        }

        [Test]
        public void Should_be_able_to_parse_pawn_promotion__b1_EQUAL_Q_PLUS()
        {
            const string move = "b1=Q+";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, new Move
                {
                    PieceType = PieceType.Pawn,
                    SourceFile = null,
                    SourceRow = null,
                    DestinationFile = File.B,
                    DestinationRow = Row.Row1,
                    IsCapture = false,
                    IsEnPassantCapture = false,
                    IsPromotion = true,
                    PromotionPiece = PieceType.Queen,
                    IsCastle = false,
                    CastleType = null,
                    IsCheck = true,
                    IsDoubleCheck = false,
                    IsMate = false,
                    HasError = false
                });
        }

        [Test]
        public void Should_be_able_to_parse_pawn_promotion__c1Q()
        {
            const string move = "c1Q";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, new Move
                {
                    PieceType = PieceType.Pawn,
                    SourceFile = null,
                    SourceRow = null,
                    DestinationFile = File.C,
                    DestinationRow = Row.Row1,
                    IsCapture = false,
                    IsEnPassantCapture = false,
                    IsPromotion = true,
                    PromotionPiece = PieceType.Queen,
                    IsCastle = false,
                    CastleType = null,
                    IsCheck = false,
                    IsDoubleCheck = false,
                    IsMate = false,
                    HasError = false
                });
        }

        [Test]
        public void Should_be_able_to_parse_pawn_promotion__c1_EQUAL_Q()
        {
            const string move = "c1=Q";
            var algebraic = _parser.Parse(move);
            Verify(algebraic, new Move
                {
                    PieceType = PieceType.Pawn,
                    SourceFile = null,
                    SourceRow = null,
                    DestinationFile = File.C,
                    DestinationRow = Row.Row1,
                    IsCapture = false,
                    IsEnPassantCapture = false,
                    IsPromotion = true,
                    PromotionPiece = PieceType.Queen,
                    IsCastle = false,
                    CastleType = null,
                    IsCheck = false,
                    IsDoubleCheck = false,
                    IsMate = false,
                    HasError = false
                });
        }

        private static void Verify(Move move, Move expected)
        {
            move.HasError.ShouldBeEqualTo(expected.HasError, "has error");
            move.PieceType.ShouldBeEqualTo(expected.PieceType, "piece type");
            move.SourceFile.ShouldBeEqualTo(expected.SourceFile, "source file");
            move.SourceRow.ShouldBeEqualTo(expected.SourceRow, "source row");
            move.DestinationFile.ShouldBeEqualTo(expected.DestinationFile, "destination file");
            move.DestinationRow.ShouldBeEqualTo(expected.DestinationRow, "destination row");
            move.IsCapture.ShouldBeEqualTo(expected.IsCapture, "is capture");
            move.IsEnPassantCapture.ShouldBeEqualTo(expected.IsEnPassantCapture, "is en passant capture");
            move.IsPromotion.ShouldBeEqualTo(expected.IsPromotion, "is promotion");
            move.PromotionPiece.ShouldBeEqualTo(expected.PromotionPiece, "promotion piece");
            move.IsCastle.ShouldBeEqualTo(expected.IsCastle, "is castle");
            move.CastleType.ShouldBeEqualTo(expected.CastleType, "castle type");
            move.IsCheck.ShouldBeEqualTo(expected.IsCheck, "is check");
            move.IsDoubleCheck.ShouldBeEqualTo(expected.IsDoubleCheck, "is double check");
            move.IsMate.ShouldBeEqualTo(expected.IsMate, "is mate");
        }
    }
}