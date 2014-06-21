using System.Text;

namespace PgnFileTools
{
    public class Move
    {
        public CastleType CastleType { get; set; }
        public File DestinationFile { get; set; }
        public Row DestinationRow { get; set; }
        public string ErrorMessage { get; set; }
        public bool HasError { get; set; }
        public bool IsCapture { get; set; }
        public bool IsCastle { get; set; }
        public bool IsCheck { get; set; }
        public bool IsDoubleCheck { get; set; }
        public bool IsEnPassantCapture { get; set; }
        public bool IsMate { get; set; }
        public bool IsPromotion { get; set; }
        public int Number { get; set; }
        public PieceType PieceType { get; set; }
        public PieceType PromotionPiece { get; set; }
        public File SourceFile { get; set; }
        public Row SourceRow { get; set; }

        public string ToAlgebraicString()
        {
            var result = new StringBuilder();
            result.Append(DestinationFile.Symbol);
            result.Append(DestinationRow.Symbol);
            return result.ToString();
        }
    }
}