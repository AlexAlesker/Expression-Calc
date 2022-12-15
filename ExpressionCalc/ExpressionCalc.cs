using System.Collections.Generic;

namespace ExpressionCalc {

public static class ExpressionCalc {
    private static readonly ExpressionParser _parser = new ExpressionParser();
    private static Stack<dynamic> _stack;

    public static dynamic Calculate(string expression) {
        _stack = new Stack<dynamic>();

        _parser.Expression = expression;
        var lexemes = _parser.GetLexemes();
        var outQueue = InfixToRPNNotationConverter.GetRPNQueue(lexemes);

        foreach (var lexeme in outQueue) {
            if (lexeme.IsNumber) {
                _stack.Push(lexeme.Value);
                continue;
            }

            if (lexeme.IsOperator) {
                var num2 = _stack.Pop();
                var num1 = _stack.Pop();
                var result = lexeme.Calculate(num1, num2);
                _stack.Push(result);
            }
        }

        return _stack.Pop();
    }
}
}
