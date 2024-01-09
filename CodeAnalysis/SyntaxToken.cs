namespace myCompiler.CodeAnalysis
{
    class SyntaxToken : SyntaxNode
    {
        public SyntaxToken(SyntaxKind kind, int position, string text, double? value)
        {
            Kind = kind;
            Position = position;
            Text = text;
            Value = value;
        }
        public override SyntaxKind Kind { get; }
        public int Position { get; }
        public string Text { get; }
        public double? Value { get; }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            return Enumerable.Empty<SyntaxNode>();
        }
    }

}