using System;
using System.Linq;

namespace PgnFileTools
{
    public class FenParser
    {
        public bool HandleRow(char ch, State state)
        {
            var pieceType = PieceType.GetForFen(ch);
            if (pieceType != null)
            {
                var pieceColor = PieceColor.GetForFen(ch);
                state.GameState.Add(new Square
                    {
                        PieceColor = pieceColor,
                        PieceType = pieceType,
                        Position = new Position
                            {
                                File = state.File,
                                Row = state.Row
                            }
                    });
                return true;
            }
            return false;
        }

        public GameState Parse(string fen)
        {
            var state = new State
                {
                    GameState = new GameState(),
                    Row = Row.Row8,
                    File = File.A
                };
            HandleRow(fen[0], state);


            return state.GameState;
        }

        public class State
        {
            public File File;
            public GameState GameState;
            public Row Row;
        }
    }
}