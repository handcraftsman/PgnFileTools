using System;

namespace PgnFileTools
{
    public class Move
    {
        public File DestinationFile { get; set; }
        public Row DestinationRow { get; set; }
        public bool HasError { get; set; }
        public bool IsCapture { get; set; }
        public bool IsEnPassantCapture { get; set; }
        public PieceType PieceType { get; set; }
        public File SourceFile { get; set; }
        public string ErrorMessage { get; set; }
    }
}