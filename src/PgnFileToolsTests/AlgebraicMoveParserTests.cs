using FluentAssert;

using NUnit.Framework;

using PgnFileTools;

namespace PgnFileToolsTests
{
    [TestFixture]
    public class AlgebraicMoveParserTests
    {
        [Test]
        public void Should_be_able_to_parse_pawn_move__a4()
        {
            const string move = "a4";
            var algebraic = new AlgebraicMoveParser().Parse(move);
            Verify(algebraic, PieceType.Pawn, File.A, Row.Row4, false);
        }

        private static void Verify(Move move, PieceType pieceType, File destinationfile, Row destinationRow, bool hasError)
        {
            move.PieceType.ShouldBeEqualTo(pieceType, "piece type");
            move.DestinationFile.ShouldBeEqualTo(destinationfile, "destination file");
            move.DestinationRow.ShouldBeEqualTo(destinationRow, "destination row");
            move.HasError.ShouldBeEqualTo(hasError, "has error");
        }
    }
}