using System;
using System.Collections.Generic;
using System.Linq;

namespace PgnFileTools
{
    public class GameState
    {
        public GameState()
        {
            Black = new List<Square>();
        }

        public List<Square> Black{ get; private set; }

        public void Add(Square square)
        {
            Black.Add(square);
        }
    }
}