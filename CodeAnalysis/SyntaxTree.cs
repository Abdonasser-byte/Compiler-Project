namespace myCompiler.CodeAnalysis
{
    sealed class SyntaxTree
    {
        public SyntaxTree(IEnumerable<string> diagonstics, ExpressionSyntax root, SyntaxToken endFileToken)
        {
            Diagonstics = diagonstics.ToArray();
            Root = root;
            EndFileToken = endFileToken;
        }
        public IReadOnlyList<string> Diagonstics { get; }
        public ExpressionSyntax Root { get; }
        public SyntaxToken EndFileToken { get; }
    }

}