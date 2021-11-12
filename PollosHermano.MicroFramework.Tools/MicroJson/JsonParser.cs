using PollosHermano.MicroFramework.Tools.MicroJson.Exceptions;
using PollosHermano.MicroFramework.Tools.MicroJson.Loggers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PollosHermano.MicroFramework.Tools.MicroJson
{
    /// <summary>
    /// Parses JSON into POCOs.
    /// </summary>
    public class JsonParser
    {
        string Input { get; set; }
        int InputLength { get; set; }
        int Position { get; set; }
        int Line { get; set; }
        int Column { get; set; }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to collect line info during parsing.
        /// </summary>
        /// <value>
        /// <c>true</c> if line info should be collected during parsing; otherwise, <c>false</c>.
        /// </value>
        public bool CollectLineInfo { get; set; }

        /// <summary>
        /// Parse the specified JSON text.
        /// </summary>
        /// <param name='text'>
        /// The JSON text to parse.
        /// </param>
        public object Parse(string text)
        {
            if (text == null)
                throw BuildParserException("input is null");

            Input = text;
            InputLength = text.Length;
            Position = 0;
            Line = 1;
            Column = 1;

            var o = Value();

            SkipWhitespace();

            if (Position != InputLength)
                throw BuildParserException("extra characters at end");

            return o;
        }

        void WriteLineLog(string msg, params object[] args)
        {
            if (Logger != null)
            {
                Logger.WriteLine(msg, args);
            }
        }

        ParserException BuildParserException(string msg)
        {
            if (CollectLineInfo)
            {
                return new ParserException(string.Format(CultureInfo.InvariantCulture, "Parse error: {0} at line {1}, column {2}.", msg, Line, Column), Line, Column);
            }
            else
            {
                return new ParserException("Parse error: " + msg + ".", 0, 0);
            }
        }

        void AdvanceInput(int n)
        {
            if (CollectLineInfo)
            {
                for (int i = Position; i < Position + n; i++)
                {
                    var c = Input[i];

                    if (c == '\n')
                    {
                        Line++;
                        Column = 1;
                    }
                    else
                    {
                        Column++;
                    }
                }
            }

            Position += n;
        }

        string Accept(string s)
        {
            var len = s.Length;

            if (Position + len > InputLength)
            {
                return null;
            }

            if (Input.IndexOf(s, Position, len, StringComparison.Ordinal) != -1)
            {
                var match = Input.Substring(Position, len);
                AdvanceInput(len);
                return match;
            }

            return null;
        }

        char Expect(char c)
        {
            if (Position >= InputLength || Input[Position] != c)
            {
                throw BuildParserException("expected '" + c + "'");
            }

            AdvanceInput(1);

            return c;
        }

        object Value()
        {
            SkipWhitespace();

            if (Position >= InputLength)
            {
                throw BuildParserException("input contains no value");
            }

            var nextChar = Input[Position];

            if (nextChar == '"')
            {
                AdvanceInput(1);
                return String();
            }
            else if (nextChar == '[')
            {
                AdvanceInput(1);
                return List();
            }
            else if (nextChar == '{')
            {
                AdvanceInput(1);
                return Dictionary();
            }
            else if (char.IsDigit(nextChar) || nextChar == '-')
            {
                return Number();
            }
            else
            {
                return Literal();
            }
        }

        object Number()
        {
            int currentPos = Position;
            bool dotSeen = false;

            Accept(c => c == '-', ref currentPos);
            ExpectDigits(ref currentPos);

            if (Accept(c => c == '.', ref currentPos))
            {
                dotSeen = true;
                ExpectDigits(ref currentPos);
            }

            if (Accept(c => (c == 'e' || c == 'E'), ref currentPos))
            {
                Accept(c => (c == '-' || c == '+'), ref currentPos);
                ExpectDigits(ref currentPos);
            }

            var len = currentPos - Position;
            var num = Input.Substring(Position, len);

            if (dotSeen)
            {
                decimal d;
                if (decimal.TryParse(num, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign | NumberStyles.AllowExponent, CultureInfo.InvariantCulture, out d))
                {
                    WriteLineLog("decimal: {0}", d);
                    AdvanceInput(len);
                    return d;
                }
                else
                {
                    double dbl;
                    if (double.TryParse(num, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign | NumberStyles.AllowExponent, CultureInfo.InvariantCulture, out dbl))
                    {
                        WriteLineLog("double: {0}", dbl);
                        AdvanceInput(len);
                        return dbl;
                    }

                    throw BuildParserException("cannot parse decimal number");
                }
            }
            else
            {
                int i;
                if (int.TryParse(num, NumberStyles.AllowLeadingSign | NumberStyles.AllowExponent, CultureInfo.InvariantCulture, out i))
                {
                    WriteLineLog("int: {0}", i);
                    AdvanceInput(len);
                    return i;
                }
                else
                {
                    long l;
                    if (long.TryParse(num, NumberStyles.AllowLeadingSign | NumberStyles.AllowExponent, CultureInfo.InvariantCulture, out l))
                    {
                        WriteLineLog("long: {0}", l);
                        AdvanceInput(len);
                        return l;
                    }

                    throw BuildParserException("cannot parse integer number");
                }
            }
        }

        bool Accept(Predicate<char> predicate, ref int pos)
        {
            if (pos < InputLength && predicate(Input[pos]))
            {
                pos++;
                return true;
            }

            return false;
        }

        void ExpectDigits(ref int pos)
        {
            int start = pos;
            while (pos < InputLength && char.IsDigit(Input[pos])) pos++;
            if (start == pos) throw BuildParserException("not a number");
        }

        string String()
        {
            int currentPos = Position;
            StringBuilder sb = new StringBuilder();

            while (true)
            {
                if (currentPos >= InputLength)
                {
                    throw BuildParserException("unterminated string");
                }

                var c = Input[currentPos];

                if (c == '"')
                {
                    var len = currentPos - Position;
                    AdvanceInput(len + 1);
                    WriteLineLog("string: {0}", sb);
                    return sb.ToString();
                }
                else if (c == '\\')
                {
                    currentPos++;

                    if (currentPos >= InputLength)
                    {
                        throw BuildParserException("unterminated escape sequence string");
                    }

                    c = Input[currentPos];

                    switch (c)
                    {
                        case '"':
                        case '/':
                        case '\\':
                            sb.Append(c);
                            break;
                        case 'b':
                            sb.Append('\b');
                            break;
                        case 'f':
                            sb.Append('\f');
                            break;
                        case 'n':
                            sb.Append('\n');
                            break;
                        case 'r':
                            sb.Append('\r');
                            break;
                        case 't':
                            sb.Append('\t');
                            break;
                        case 'u':
                            currentPos += 4;
                            if (currentPos >= InputLength)
                                throw BuildParserException("unterminated unicode escape in string");
                            else
                            {
                                int u;
                                if (!int.TryParse(Input.Substring(currentPos - 3, 4), NumberStyles.AllowHexSpecifier, NumberFormatInfo.InvariantInfo, out u))
                                    throw BuildParserException("not a well-formed unicode escape sequence in string");
                                sb.Append((char)u);
                            }
                            break;
                        default:
                            throw BuildParserException("unknown escape sequence in string");
                    }
                }
                else if ((int)c < 0x20)
                {
                    throw BuildParserException("control character in string");
                }
                else
                {
                    sb.Append(c);
                }

                currentPos++;
            }
        }

        object Literal()
        {
            if (Accept("true") != null)
            {
                WriteLineLog("bool: true");
                return true;
            }

            if (Accept("false") != null)
            {
                WriteLineLog("bool: false");
                return false;
            }

            if (Accept("null") != null)
            {
                WriteLineLog("null");
                return null;
            }

            throw BuildParserException("unknown token");
        }

        IList<object> List()
        {
            WriteLineLog("list: [");

            List<object> list = new List<object>();

            SkipWhitespace();
            if (IsNext(']'))
            {
                AdvanceInput(1); return list;
            }

            object obj = null;
            do
            {
                SkipWhitespace();
                obj = Value();
                if (obj != null)
                {
                    list.Add(obj);
                    SkipWhitespace();
                    if (IsNext(']')) break;
                    Expect(',');
                }
            }
            while (obj != null);

            Expect(']');

            WriteLineLog("]");

            return list;
        }

        IDictionary<string, object> Dictionary()
        {
            WriteLineLog("Dictionary: {");

            Dictionary<string, object> dict = new Dictionary<string, object>();

            SkipWhitespace();
            if (IsNext('}'))
            {
                AdvanceInput(1); return dict;
            }

            KeyValuePair<string, object>? kvp = null;
            do
            {
                SkipWhitespace();

                kvp = KeyValuePair();

                if (kvp.HasValue)
                {
                    dict[kvp.Value.Key] = kvp.Value.Value;
                }

                SkipWhitespace();
                if (IsNext('}')) break;
                Expect(',');
            }
            while (kvp != null);

            Expect('}');

            WriteLineLog("}");

            return dict;
        }

        KeyValuePair<string, object>? KeyValuePair()
        {
            Expect('"');

            var key = String();

            SkipWhitespace();

            Expect(':');

            var obj = Value();

            return new KeyValuePair<string, object>(key, obj);
        }

        void SkipWhitespace()
        {
            int n = Position;
            while (IsWhiteSpace(n)) n++;
            if (n != Position)
            {
                AdvanceInput(n - Position);
            }
        }

        bool IsWhiteSpace(int n)
        {
            if (n >= InputLength) return false;
            char c = Input[n];
            return c == ' ' || c == '\t' || c == '\r' || c == '\n';
        }

        bool IsNext(char c)
        {
            return Position < InputLength && Input[Position] == c;
        }
    }

}
