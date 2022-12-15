using System;

namespace ExpressionCalc {

public enum LexemeType {
    None,
    Number,
    Addition,
    Subtraction,
    Multiplication,
    Division,
    Pow,
    LeftBracket,
    RightBracket
}

public class Lexeme {
    public readonly dynamic Value;
    public readonly LexemeType Type;

    public Lexeme(LexemeType type, dynamic value = null) {
        Type = type;
        Value = value;
    }

    public bool IsNumber => Type == LexemeType.Number;

    public bool IsLeftBracket => Type == LexemeType.LeftBracket;
    public bool IsRightBracket => Type == LexemeType.RightBracket;

    public bool IsOperator {
        get {
            switch (Type) {
            case LexemeType.None:
            case LexemeType.Number:
            case LexemeType.LeftBracket:
            case LexemeType.RightBracket:
                return false;
            default:
                return true;
            }
        }
    }

    public int Priority {
        get {
            switch (Type) {
            case LexemeType.Addition:
            case LexemeType.Subtraction:
                return 1;
            case LexemeType.Multiplication:
            case LexemeType.Division:
                return 2;
            case LexemeType.Pow:
                return 3;
            default:
                return 0;
            }
        }
    }

    public dynamic Calculate(dynamic left, dynamic right) {
        if (!IsOperator) {
            throw new ArithmeticException("Not a valid operator");
        }

        switch (Type) {
        case LexemeType.Addition:
            return left + right;
        case LexemeType.Subtraction:
            return left - right;
        case LexemeType.Multiplication:
            return left * right;
        case LexemeType.Division:
            return left / right;
        default:
            throw new ArithmeticException("Operator is not supported");
        }
    }
}
}
