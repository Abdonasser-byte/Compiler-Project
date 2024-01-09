namespace myCompiler.CodeAnalysis
{
    class Evaluator
    {
        private readonly ExpressionSyntax _root;
        public Evaluator(ExpressionSyntax root)
        {
            this._root = root;
        }
        public double Evaluate()
        {
            return EvaluateExpression(_root);
        }
        private double EvaluateExpression(ExpressionSyntax node)
        {

            if (node is NumberExpressionSyntax n)
            {
                return (double)n.NumberToken.Value;
            }
            if (node is BinaryExpressionSyntax b)
            {
                var left = EvaluateExpression(b.Left);
                var right = EvaluateExpression(b.Right);

                if (b.OperatorToken.Kind == SyntaxKind.PlusToken)
                {
                    return left + right;
                }
                else if (b.OperatorToken.Kind == SyntaxKind.MinusToken)
                {
                    return left - right;
                }
                else if (b.OperatorToken.Kind == SyntaxKind.MultiToken)
                {
                    return left * right;
                }
                else if (b.OperatorToken.Kind == SyntaxKind.DivideToken)
                {
                    return left / right;
                }
                else if (b.OperatorToken.Kind == SyntaxKind.ModToken)
                {
                    return left % right;
                }
                else if (b.OperatorToken.Kind == SyntaxKind.PowerToken)
                {
                    return (double)Math.Pow(left, right);
                }
                else
                {
                    throw new Exception($"Unexpected binary operator {b.OperatorToken.Kind}");
                }
            }
            if (node is ParanthesizedExpressionSyntax p)
            {
                return EvaluateExpression(p.Expression);
            }
            throw new Exception($"Unexpected node {node.Kind}");
        }
    }

}