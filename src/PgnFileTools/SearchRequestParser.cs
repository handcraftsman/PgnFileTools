using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using MvbaCore;

using PgnFileTools.Extensions;

namespace PgnFileTools
{
    public class SearchRequestParser
    {
        private readonly StringBuilder _token;
        private Func<char, State, bool> _handle;

        public SearchRequestParser()
        {
            _token = new StringBuilder();
        }

        private bool HandleHeaderValue(char ch, State state)
        {
            if (ch == '\r' || ch == '\n')
            {
                UpdateFunction(state, _token.ToString());
                state.Header = null;
                _token.Length = 0;
                _handle = HandleToken;
                return true;
            }
            _token.Append(ch);
            return true;
        }

        private bool HandleToken(char ch, State state)
        {
            switch (ch)
            {
                case '\n':
                case '\r':
                    break;
                case '=':
                {
                    state.Header = _token.ToString();
                    state.HeaderMatchType = HeaderMatchType.Equal;
                    _token.Length = 0;
                    _handle = HandleHeaderValue;
                    break;
                }
                case '^':
                {
                    state.Header = _token.ToString();
                    state.HeaderMatchType = HeaderMatchType.StartsWith;
                    _token.Length = 0;
                    _handle = HandleHeaderValue;
                    break;
                }
                default:
                    _token.Append(ch);
                    break;
            }
            return true;
        }

        public Notification<Func<GameInfo, bool>> Parse(TextReader reader)
        {
            _token.Length = 0;
            _handle = HandleToken;
            var state = new State();

            foreach (var ch in reader.GenerateFrom())
            {
                var success = _handle(ch, state);
                if (!success)
                {
                    return new Notification<Func<GameInfo, bool>>(new NotificationMessage(NotificationSeverity.Error, state.ErrorMessage));
                }
            }

            if (_handle == HandleHeaderValue)
            {
                _handle('\n', state);
            }

            return new Notification<Func<GameInfo, bool>>
                {
                    Item = state.Func.Compile()
                };
        }

        private static void UpdateFunction(State state, string value)
        {
            var header = state.Header;
            switch (state.HeaderMatchType)
            {
                case HeaderMatchType.Equal:
                    state.Func = state.Func.AndWith(x => x.Headers.Any(y => y.Key.Equals(header, StringComparison.InvariantCultureIgnoreCase) && y.Value == value));
                    break;
                case HeaderMatchType.StartsWith:
                    state.Func = state.Func.AndWith(x => x.Headers.Any(y => y.Key.Equals(header, StringComparison.InvariantCultureIgnoreCase) && y.Value.StartsWith(value)));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private enum HeaderMatchType
        {
            Equal,
            StartsWith
        }

        private class State
        {
            public string ErrorMessage { get; set; }
            public Expression<Func<GameInfo, bool>> Func { get; set; }
            public string Header { get; set; }
            public HeaderMatchType HeaderMatchType { get; set; }
        }
    }
}