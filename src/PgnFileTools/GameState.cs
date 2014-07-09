using System.Collections.Generic;

namespace PgnFileTools
{
    public class GameState
    {
        public GameState()
        {
            White = new List<Square>();
            Black = new List<Square>();
        }

        public List<Square> Black { get; private set; }
        public PieceColor ToMove { get; set; }
        public List<Square> White { get; private set; }

        public void Add(Square square)
        {
            if (square.PieceColor == PieceColor.Black)
            {
                Black.Add(square);
            }
            else if (square.PieceColor == PieceColor.White)
            {
                White.Add(square);
            }
        }
    }
}