using System.Collections.Generic;
using NUnit.Framework;

namespace ExpressionCalc {

[TestFixture]
internal class InfixToRPNNotationConverterTestCase : ExpressionBaseTestCase {

    private List<Lexeme> GetRPNQueue(string expression) {
        var lexemes = GetLexemes(expression);
        return InfixToRPNNotationConverter.GetRPNQueue(lexemes);
    }

    [Test]
    public void TestRPN1() {
        var lexemes = GetRPNQueue("1");
        Assert.That(lexemes.Count, Is.EqualTo(1));
        Assert.That(lexemes[0].Value, Is.EqualTo(1));
    }

    [Test]
    public void TestRPN2() {
        var lexemes = GetRPNQueue("1 + 2");
        Assert.That(lexemes.Count, Is.EqualTo(3));
        Assert.That(lexemes[0].Value, Is.EqualTo(1));
        Assert.That(lexemes[1].Value, Is.EqualTo(2));
        Assert.That(lexemes[2].Type, Is.EqualTo(LexemeType.Addition));
    }

    [Test]
    public void TestRPN3() {
        var lexemes = GetRPNQueue("1 + 2 * 3");
        Assert.That(lexemes.Count, Is.EqualTo(5));
        Assert.That(lexemes[0].Value, Is.EqualTo(1));
        Assert.That(lexemes[1].Value, Is.EqualTo(2));
        Assert.That(lexemes[2].Value, Is.EqualTo(3));
        Assert.That(lexemes[3].Type, Is.EqualTo(LexemeType.Multiplication));
        Assert.That(lexemes[4].Type, Is.EqualTo(LexemeType.Addition));
    }

    [Test]
    public void TestRPN4() {
        var lexemes = GetRPNQueue("(1 + 2) * 3");
        Assert.That(lexemes.Count, Is.EqualTo(5));
        Assert.That(lexemes[0].Value, Is.EqualTo(1));
        Assert.That(lexemes[1].Value, Is.EqualTo(2));
        Assert.That(lexemes[2].Type, Is.EqualTo(LexemeType.Addition));
        Assert.That(lexemes[3].Value, Is.EqualTo(3));
        Assert.That(lexemes[4].Type, Is.EqualTo(LexemeType.Multiplication));
    }
}
}
