# Basic-Calculator-compiler.

you can after run the program write your expression in console and get result and also parse tree.

## Example:

```bash
> ((1+2+3)*5/6)^2
the result : 25
```
```
>show
>((1+2+3)*5/6)^2
└───BinaryExpression
    ├───ParanthesizedExpressionSyntax
    │   ├───OpenPranToken
    │   ├───BinaryExpression
    │   │   ├───BinaryExpression
    │   │   │   ├───ParanthesizedExpressionSyntax
    │   │   │   │   ├───OpenPranToken
    │   │   │   │   ├───BinaryExpression
    │   │   │   │   │   ├───BinaryExpression
    │   │   │   │   │   │   ├───NumberExpression
    │   │   │   │   │   │   │   └───NumberToken 1
    │   │   │   │   │   │   ├───PlusToken
    │   │   │   │   │   │   └───NumberExpression
    │   │   │   │   │   │       └───NumberToken 2
    │   │   │   │   │   ├───PlusToken
    │   │   │   │   │   └───NumberExpression
    │   │   │   │   │       └───NumberToken 3
    │   │   │   │   └───ClosePranToken
    │   │   │   ├───MultiToken
    │   │   │   └───NumberExpression
    │   │   │       └───NumberToken 5
    │   │   ├───DivideToken
    │   │   └───NumberExpression
    │   │       └───NumberToken 6
    │   └───ClosePranToken
    ├───PowerToken
    └───NumberExpression
        └───NumberToken 2
the result : 25
```

## Installation

```
mkdir folderName
cd folderName
git clone  https://github.com/abdoafage/Basic-Calculator-Compiler
```
## other projects
[projects](https://abdoyasser.herokuapp.com/)
