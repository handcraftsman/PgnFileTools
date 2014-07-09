using System;
using System.Linq;

namespace PgnFileTools
{
    public class FenParser
    {
        private Func<char, State, bool> _handle;

        private bool HandleActiveColor(char ch, State state)
        {
            var color = PieceColor.GetFor(ch);
            if (color != null)
            {
                state.GameState.ToMove = color;
                return true;
            }
            return false;
        }

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
                state.File = File.GetFor(state.File.Index + filesToSkip) ?? File.A;
                return true;
            }
            if (ch == '/')
            {
                state.Row = Row.GetFor(state.Row.Index - 1);
                state.File = File.A;
                return true;
            }
            if (ch == ' ')
            {
                _handle = HandleActiveColor;
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