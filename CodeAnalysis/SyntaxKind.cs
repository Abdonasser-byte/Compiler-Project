namespace myCompiler.CodeAnalysis
{
    enum SyntaxKind
    {
        NumberToken,
        WhiteSpaceToken,
        PlusToken,
        MinusToken,
        DivideToken,
        MultiToken,
        PowerToken,
        ModToken,
        OpenPranToken,
        ClosePranToken,
        BadToken,
        EndFileToken,
        NumberExpression,
        BinaryExpression,
        ParanthesizedExpressionSyntax
    }

}