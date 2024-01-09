using System;
using System.Collections.Generic;
using System.Linq;
using myCompiler.CodeAnalysis;

namespace MYCOMPILER
{
    public static class Program
    {
        static bool showTree = false;
        public static void Main(string[] args)
        {
            Console.WriteLine("\t\t\t\tWELCOME TO MY CALCULATOR BY C#");
            Console.WriteLine("\t\t\t\t------------------------------");
            Console.WriteLine("\t\t write < show > to show/hide the syntax tree to our expression");
            while (true)
            {
                Console.Write(">");
                var line = Console.ReadLine();
                if (line == "show")
                {
                    showTree = !showTree;
                    continue;
                }
                Parser parser = new Parser(line);
                var syntaxTree = parser.parse();
                var color = Console.ForegroundColor;

                if (showTree) prettyPrint(syntaxTree.Root);

                if (syntaxTree.Diagonstics.Any())
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    foreach (var diagonastic in syntaxTree.Diagonstics)
                    {
                        Console.WriteLine(diagonastic);
                    }

                    Console.ForegroundColor = color;
                }
                else
                {
                    Evaluator e = new Evaluator(syntaxTree.Root);
                    var res = e.Evaluate();
                    Console.WriteLine($"the result : {res}");
                }
            }
        }
        static void prettyPrint(SyntaxNode node, string indent = "", bool isLast = true)
        {
            var marker = isLast ? "└───" : "├───";
            Console.Write(indent + marker + node.Kind);
            if (node is SyntaxToken t && t.Value != null)
            {
                Console.Write(" ");
                Console.Write(t.Value);
            }
            Console.WriteLine();

            indent += isLast ? "    " : "│   ";

            var lastChild = node.GetChildren().LastOrDefault();
            foreach (var child in node.GetChildren())
            {
                prettyPrint(child, indent, child == lastChild);
            }
        }
    }



}

