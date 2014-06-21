using System;
using System.IO;
using System.Linq;
using System.Text;

using PgnFileTools.Extensions;

namespace PgnFileTools
{
    public class GameInfoParser
    {
        private Func<char, GameInfo, bool> _handle;
        private StringBuilder _partialHeader;

        private static bool Done(char ch, GameInfo gameInfo)
        {
            return true;
        }

        private bool HandleHeaderBody(char ch, GameInfo gameInfo)
        {
            if (ch != ']')
            {
                _partialHeader.Append(ch);
                return true;
            }
            var headerParts = _partialHeader.ToString().Split(' ');
            _partialHeader.Length = 0;
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
            if (ch == '\r' || ch == '\n')
            {
                return true;
            }
            return false;
        }

        public GameInfo Parse(TextReader source)
        {
            _partialHeader = new StringBuilder();
            _handle = HandleHeaderStart;

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