namespace myCompiler.CodeAnalysis
{
    class Parser
    {
        private readonly SyntaxToken[] _tokens;
        private int _position;

        private List<string> _diagonastics = new List<string>();
        public Parser(string text)
        {
            var tokens = new List<SyntaxToken>();
            var lexer = new Lexer(text);
            SyntaxToken token;
            do
            {
                token = lexer.NextToken();
                if (token.Kind != SyntaxKind.BadToken && token.Kind != SyntaxKind.WhiteSpaceToken)
                {
                    tokens.Add(token);
                }

            } while (token.Kind != SyntaxKind.EndFileToken);
            _tokens = tokens.ToArray();
            _diagonastics.AddRange(lexer.Diagonastics);
        }
        public IEnumerable<string> Diagonastics => _diagonastics;
        private SyntaxToken Peek(int offset)
        {
            int index = _position + offset;
            if (index >= _tokens.Length)
            {
                return _tokens[_tokens.Length - 1];
            }
            return _tokens[index];
        }
        private SyntaxToken Current => Peek(0);

        private SyntaxToken NextToken()
        {
            SyntaxToken current = Current;
            _position++;
            return current;
        }
        private SyntaxToken Match(SyntaxKind Kind)
        {
            if (Current.Kind == Kind)
            {
                return NextToken();
            }
            _diagonastics.Add($"ERROR: Unexpected token <{Current.Kind}> expected <{Kind}>");
            return new SyntaxToken(Kind, Current.Position, null, null);
        }
        public ExpressionSyntax ParseExpression()
        {
            return parseTerm();
        }
        public SyntaxTree parse()
        {
            var expression = parseTerm();
            var endfiletoken = Match(SyntaxKind.EndFileToken);
            return new SyntaxTree(_diagonastics, expression, endfiletoken);
        }
        public ExpressionSyntax parseTerm()
        {
            var left = parseFactor();
            while (Current.Kind == SyntaxKind.PlusToken || Current.Kind == SyntaxKind.MinusToken)
            {
                var OperatorToken = NextToken();
                var right = parseFactor();
                left = new BinaryExpressionSyntax(left, OperatorToken, right);
            }
            return left;
        }
        public ExpressionSyntax parseFactor()
        {
            ExpressionSyntax left = parsePower();
            while (Current.Kind == SyntaxKind.MultiToken || Current.Kind == SyntaxKind.DivideToken ||
            Current.Kind == SyntaxKind.ModToken)
            {
                SyntaxToken OperatorToken = NextToken();
                ExpressionSyntax right = parsePower();
                left = new BinaryExpressionSyntax(left, OperatorToken, right);
            }
            return left;
        }
        public ExpressionSyntax parsePower()
        {
            ExpressionSyntax left = ParsePrimaryExpression();
            while (Current.Kind == SyntaxKind.PowerToken)
            {
                SyntaxToken OperatorToken = NextToken();
                ExpressionSyntax right = ParsePrimaryExpression();
                left = new BinaryExpressionSyntax(left, OperatorToken, right);
            }
            return left;
        }
        private ExpressionSyntax ParsePrimaryExpression()
        {
            if (Current.Kind == SyntaxKind.OpenPranToken)
            {
                var left = NextToken();
                var expression = parseTerm();
                var right = Match(SyntaxKind.ClosePranToken);
                return new ParanthesizedExpressionSyntax(left, expression, right);
            }
            SyntaxToken numberToken = Match(SyntaxKind.NumberToken);
            return new NumberExpressionSyntax(numberToken);
        }
    }

}