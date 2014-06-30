using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using PgnFileTools.Extensions;

namespace PgnFileTools
{
    public class GameInfoParser
    {
        private readonly AlgebraicMoveParser _algebraicMoveParser;
        private readonly Stack<StringBuilder> _moveVariations;
        private StringBuilder _partial;
        private Func<char, GameInfo, bool> _handle;
        private int _moveNumber;

        public GameInfoParser()
        {
            _partial = new StringBuilder();
            _algebraicMoveParser = new AlgebraicMoveParser();
            _moveVariations = new Stack<StringBuilder>();
        }

        private static bool Done(char ch, GameInfo gameInfo)
        {
            return true;
        }

        private bool HandleGameComment(char ch, GameInfo gameInfo)
        {
            if (ch == '}')
            {
                gameInfo.Comment = _partial.ToString();
                _partial.Length = 0;
                _handle = HandleMoveNumber;
                return true;
            }
            _partial.Append(ch);
            return true;
        }

        private bool HandleHeaderBody(char ch, GameInfo gameInfo)
        {
            if (ch != ']')
            {
                _partial.Append(ch);
                return true;
            }
            var headerParts = _partial.ToString().Split(' ');
            _partial.Length = 0;
            gameInfo.Headers.Add(headerParts[0], headerParts[1].Trim('"'));

            _handle = HandleHeaderStart;
            return true;
        }

        private bool HandleHeaderStart(char ch, GameInfo gameInfo)
        {
            if (ch == '[')
            {
                _handle = HandleHeaderBody;
                return true;
            }
            if (Char.IsWhiteSpace(ch))
            {
                return true;
            }
            if (ch == '{')
            {
                _partial.Length = 0;
                _handle = HandleGameComment;
                return true;
            }
            if (Char.IsDigit(ch))
            {
                _partial.Length = 0;
                _handle = HandleMoveNumber;
                return HandleMoveNumber(ch, gameInfo);
            }
            return false;
        }

        private bool HandleMoveAnnotation(char ch, GameInfo gameInfo)
        {
            if (Char.IsDigit(ch))
            {
                _partial.Append(ch);
                return true;
            }
            if (_partial.Length > 0)
            {
                gameInfo.Moves.Last().Annotation = Int32.Parse(_partial.ToString());
                _partial.Length = 0;
            }
            _handle = HandleMoveText;
            return HandleMoveText(ch, gameInfo);
        }

        private bool HandleMoveComment(char ch, GameInfo gameInfo)
        {
            if (ch == '}')
            {
                gameInfo.Moves.Last().Comment = _partial.ToString();
                _partial.Length = 0;
                _handle = HandleMoveText;
                return true;
            }
            _partial.Append(ch);
            return true;
        }

        private bool HandleMoveNumber(char ch, GameInfo gameInfo)
        {
            if (Char.IsWhiteSpace(ch))
            {
                return true;
            }
            if (Char.IsDigit(ch))
            {
                _partial.Append(ch);
                return true;
            }
            if (ch == '.')
            {
                _moveNumber = Int32.Parse(_partial.ToString());
                _partial.Length = 0;
                _handle = HandleMoveText;
                return true;
            }
            return false;
        }

        private bool HandleMoveText(char ch, GameInfo gameInfo)
        {
            if (Char.IsWhiteSpace(ch))
            {
                if (_partial.Length == 0)
                {
                    return true;
                }
                if (_partial.ToString() == gameInfo.Headers["Result"])
                {
                    _handle = Done;
                    return true;
                }
                var move = _algebraicMoveParser.Parse(_partial.ToString());
                move.Number = _moveNumber;
                gameInfo.Moves.Add(move);
                _partial.Length = 0;
                return true;
            }
            switch (ch)
            {
                case '.':
                    return HandleMoveNumber(ch, gameInfo);

                case '{':
                {
                    _partial.Length = 0;
                    _handle = HandleMoveComment;
                    return true;
                }
                case '(':
                {
                    _partial.Length = 0;
                    _handle = HandleMoveVariation;
                    return true;
                }

                case '$':
                {
                    _partial.Length = 0;
                    _handle = HandleMoveAnnotation;
                    return true;
                }
                case '?':
                case '!':
                {
                    var move = _algebraicMoveParser.Parse(_partial.ToString());
                    move.Number = _moveNumber;
                    gameInfo.Moves.Add(move);
                    _partial.Length = 0;
                    _handle = HandleSymbolicMoveAnnotation;
                    return HandleSymbolicMoveAnnotation(ch, gameInfo);
                }
            }

            _partial.Append(ch);
            return true;
        }

        private bool HandleMoveVariation(char ch, GameInfo gameInfo)
        {
            if (ch == '(')
            {
                _moveVariations.Push(_partial);
                _partial = new StringBuilder();
                _partial.Append(ch);
                return true;
            }
            if (ch == ')')
            {
                if (_moveVariations.Count == 0)
                {
                    gameInfo.Moves.Last().Variation = _partial.ToString();
                    _partial.Length = 0;
                    _handle = HandleMoveText;
                }
                else
                {
                    var temp = _partial;
                    _partial = _moveVariations.Pop();
                    _partial.Append(temp);
                    _partial.Append(ch);
                }
                return true;
            }
            _partial.Append(ch);
            return true;
        }

        private bool HandleSymbolicMoveAnnotation(char ch, GameInfo gameInfo)
        {
            if (ch == '?' || ch == '!')
            {
                _partial.Append(ch);
                return true;
            }

            gameInfo.Moves.Last().Annotation = SymbolicMoveAnnotation.GetFor(_partial.ToString()).Id;
            _partial.Length = 0;
            _handle = HandleMoveText;
            return HandleMoveText(ch, gameInfo);
        }

        public GameInfo Parse(TextReader source)
        {
            _partial.Length = 0;
            _handle = HandleHeaderStart;
            _moveVariations.Clear();

            var gameInfo = new GameInfo();
            if (source.GenerateFrom().Select(ch => _handle(ch, gameInfo)).Any(success => !success))
            {
                gameInfo.HasError = true;
            }
            if (!new Func<char, GameInfo, bool>[] { Done }.Contains(_handle))
            {
                gameInfo.HasError = true;
                gameInfo.ErrorMessage = "Unexpected end of game info text.";
            }
            return gameInfo;
        }
    }
}