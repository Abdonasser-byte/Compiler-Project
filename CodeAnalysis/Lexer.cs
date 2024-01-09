namespace myCompiler.CodeAnalysis
{
    class Lexer
    {
        private readonly string _text;
        private int _position;

        private List<string> _diagonastics = new List<string>();

        public IEnumerable<string> Diagonastics => _diagonastics;
        public Lexer(string text)
        {
            _text = text;
        }
        private char Current
        {
            get
            {
                if (_position >= _text.Length) return '\0';
                return _text[_position];
            }
        }
        private void Next()
        {
            _position++;
        }

        public SyntaxToken NextToken()
        {
            if (_position >= _text.Length) return new SyntaxToken(SyntaxKind.EndFileToken, _position, null, null);
            if (char.IsDigit(Current))
            {
                var start = _position;
                while (char.IsDigit(Current) || Current=='.')
                {
                    Next();
                }
                int len = _position - start;
                string str = _text.Substring(start, len);

                if (!double.TryParse(str, out var value))
                {
                    _diagonastics.Add($"ERROR: the number {str} isn't valid Int32.");
                }
                return new SyntaxToken(SyntaxKind.NumberToken, _position, str, value);
            }

            if (char.IsWhiteSpace(Current))
            {
                var start = _position;
                while (char.IsWhiteSpace(Current))
                {
                    Next();
                }
                int len = _position - start;
                string str = _text.Substring(start, len);

                //int.TryParse(str,out var Value);
                return new SyntaxToken(SyntaxKind.WhiteSpaceToken, _position, str, null);
            }

            if (Current == '+')
            {
                return new SyntaxToken(SyntaxKind.PlusToken, _position++, "+", null);
            }
            if (Current == '-')
            {
                return new SyntaxToken(SyntaxKind.MinusToken, _position++, "-", null);
            }
            if (Current == '/')
            {
                return new SyntaxToken(SyntaxKind.DivideToken, _position++, "/", null);
            }
            if (Current == '*')
            {
                return new SyntaxToken(SyntaxKind.MultiToken, _position++, "*", null);
            }
            if (Current == '^')
            {
                return new SyntaxToken(SyntaxKind.PowerToken, _position++, "^", null);
            }
            if (Current == '%')
            {
                return new SyntaxToken(SyntaxKind.ModToken, _position++, "%", null);
            }
            if (Current == '(')
            {
                return new SyntaxToken(SyntaxKind.OpenPranToken, _position++, "(", null);
            }
            if (Current == ')')
            {
                return new SyntaxToken(SyntaxKind.ClosePranToken, _position++, ")", null);
            }
            _diagonastics.Add($"ERROR: bad character input: {Current}");
            return new SyntaxToken(SyntaxKind.BadToken, _position++, _text.Substring(_position - 1, 1), null);
        }
    }

}