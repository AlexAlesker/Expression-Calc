using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ExpressionCalc {

public class ExpressionParser {
    private string _expression;
    private List<Lexeme> _lexemes;
    private StringBuilder _numBuffer;
    private static readonly CultureInfo Culture = new CultureInfo("en-US");

    public string Expression {
        get => _expression;
        set {
            CheckExpressionIsNotEmpty(value);
            _expression = value;
        }
    }

    private static void CheckExpressionIsNotEmpty(string expression) {
        if (string.IsNullOrWhiteSpace(expression)) {
            throw new ArgumentException("Expression string is empty or null");
        }
    }

    public IList<Lexeme> GetLexemes() {
        CheckExpressionIsNotEmpty(Expression);

        _lexemes = new List<Lexeme>();
        _numBuffer = new StringBuilder();

        foreach (var lexeme in ParseLexemes()) {
            _lexemes.Add(lexeme);
        }

        return _lexemes;
    }

    private IEnumerable<Lexeme> ParseLexemes() {
        Lexeme lexeme;

        for (var i = 0; i < Expression.Length; i++) {
            LexemeType lexemeType;

            var c = Expression[i];

            switch (c) {
            case '(':
                lexemeType = LexemeType.LeftBracket;
                break;
            case ')':
                lexemeType = LexemeType.RightBracket;
                break;
            case '+':
                lexemeType = LexemeType.Addition;
                break;
            case '-':
                if (IsUnaryMinusAtPosition(i)) {
                    _numBuffer.Append(c);
                    continue;
                }

                lexemeType = LexemeType.Subtraction;
                break;
            case '*':
                lexemeType = LexemeType.Multiplication;
                break;
            case '/':
                lexemeType = LexemeType.Division;
                break;
            case ' ':
                lexemeType = LexemeType.None;
                break;
            default:
                if (IsPartOfNumber(c)) {
                    _numBuffer.Append(c);
                    continue;
                }

                throw new ArgumentException($"Wrong expression literal: '{c}'");
            }

            lexeme = GetNumberLexeme();

            if (lexeme != null) {
                yield return lexeme;
            }

            if (lexemeType != LexemeType.None) {
                yield return new Lexeme(lexemeType);    
            }
        }

        // possible trailing lexeme
        lexeme = GetNumberLexeme();

        if (lexeme != null) {
            yield return lexeme;
        }
    }

    private bool IsUnaryMinusAtPosition(int i) {
        return i < Expression.Length - 1 && IsPartOfNumber(Expression[i + 1]) && _numBuffer.Length == 0
               && (_lexemes.Count == 0 || _lexemes.Count > 0 && _lexemes.Last().IsOperator);
    }

    private static bool IsPartOfNumber(char c) {
        return char.IsDigit(c) || c == '.';
    }

    private Lexeme GetNumberLexeme() {
        if (_numBuffer.Length == 0) {
            return null;
        }

        Lexeme lexeme;
        var numBuffer = _numBuffer.ToString();

        if (int.TryParse(numBuffer, out var n)) {
            lexeme = new Lexeme(LexemeType.Number, n);
        }
        else if (double.TryParse(numBuffer, NumberStyles.Number, Culture, out var d)) {
            lexeme = new Lexeme(LexemeType.Number, d);
        }
        else {
            throw new ArgumentException($"Wrong expression lexeme: '{numBuffer}'");
        }

        _numBuffer.Clear();

        return lexeme;
    }
}
}