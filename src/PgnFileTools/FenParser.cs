using System;
using System.Linq;

namespace PgnFileTools
{
    public class FenParser
    {
        private Func<char, State, bool> _handle;
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
                state.File = File.GetFor(state.File.Index + 1);
                return true;
            }
            if (Char.IsDigit(ch))
            {
                var filesToSkip = " 12345678".IndexOf(ch);
                state.File = File.GetFor(state.File.Index + filesToSkip);
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
            _handle = HandleRow;
            foreach (var success in fen.Select(ch => _handle(ch, state)).Where(success => !success))
            {
                break;
            }

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