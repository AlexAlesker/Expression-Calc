using System.Collections.Generic;
using NUnit.Framework;

namespace ExpressionCalc {

internal class ExpressionBaseTestCase {
    protected ExpressionParser _parser;

    [SetUp]
    public void SetUp() {
        _parser = new ExpressionParser();
    }

    protected IList<Lexeme> GetLexemes(string expression) {
        _parser.Expression = expression;
        return _parser.GetLexemes();
    }
}
}
