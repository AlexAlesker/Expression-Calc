using System;
using System.Collections.Generic;

namespace ExpressionCalc {

public static class InfixToRPNNotationConverter {
    private static List<Lexeme> _outQueue;
    private static Stack<Lexeme> _operatorStack;
    private static readonly ArgumentException _ex = new ArgumentException("Expression string is in wrong format");

    // Converts lexeme list to Reverse Polish notation queue (Dijkstra algorithm)
    public static List<Lexeme> GetRPNQueue(IEnumerable<Lexeme> lexemes) {
        _outQueue = new List<Lexeme>();
        _operatorStack = new Stack<Lexeme>();

        foreach (var lexeme in lexemes) {
            if (lexeme.IsNumber) {
                _outQueue.Add(lexeme);
                continue;
            }

            if (lexeme.IsLeftBracket) {
                _operatorStack.Push(lexeme);
                continue;
            }

            if (lexeme.IsRightBracket) {
                ProcessRightBracket();
                continue;
            }

            if (lexeme.IsOperator) {
                ProcessOperator(lexeme);
            }
        }

        ProcessNonEmptyStack();

        return _outQueue;
    }

    private static void ProcessRightBracket() {
        if (_operatorStack.Count == 0) {
            throw _ex;
        }

        while (_operatorStack.Count > 0) {
            var op = _operatorStack.Pop();

            if (op.IsLeftBracket) {
                break;
            }

            _outQueue.Add(op);

            if (_operatorStack.Count == 0) {
                throw _ex;
            }
        }
    }

    private static void ProcessOperator(Lexeme lexeme) {
        while (_operatorStack.Count > 0) {
            var op = _operatorStack.Peek();

            if (op.IsOperator && op.Priority >= lexeme.Priority) {
                _outQueue.Add(_operatorStack.Pop());
            }
            else {
                break;
            }
        }

        _operatorStack.Push(lexeme);
    }

    private static void ProcessNonEmptyStack() {
        while (_operatorStack.Count > 0) {
            var op = _operatorStack.Pop();

            if (op.IsLeftBracket) {
                throw _ex;
            }

            _outQueue.Add(op);
        }
    }
}
}
