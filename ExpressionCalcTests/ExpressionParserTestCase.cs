using System;
using NUnit.Framework;

namespace ExpressionCalc {

[TestFixture]
internal class ExpressionParserTestCase : ExpressionBaseTestCase {

    [Test]
    public void TestLexemeParserEmptyExpression() {
        Assert.Throws<ArgumentException>(() =>  _parser.GetLexemes());
    }

    [Test]
    public void TestLexemeParserInt() {
        var lexemes = GetLexemes("123");
        Assert.That(lexemes.Count, Is.EqualTo(1));
        Assert.That(lexemes[0].Value, Is.EqualTo(123));
    }

    [Test]
    public void TestLexemeParserDouble1() {
        var lexemes = GetLexemes("1.1");
        Assert.That(lexemes[0].Value, Is.EqualTo(1.1));
    }

    [Test]
    public void TestLexemeParserDouble2() {
        var lexemes = GetLexemes("1.");
        Assert.That(lexemes[0].Value, Is.EqualTo(1.0));
    }

    [Test]
    public void TestLexemeParserDouble3() {
        var lexemes = GetLexemes(".1");
        Assert.That(lexemes[0].Value, Is.EqualTo(0.1));
    }

    [Test]
    public void TestLexemeParserDouble4() {
        Assert.Throws<ArgumentException>(() => GetLexemes("."));
    }

    [Test]
    public void TestLexemeParserUnaryMinus1() {
        var lexemes = GetLexemes("-1");
        Assert.That(lexemes[0].Value, Is.EqualTo(-1));
    }

    [Test]
    public void TestLexemeParserUnaryMinus2() {
        var lexemes = GetLexemes("1 + -1");
        Assert.That(lexemes[1].Type, Is.EqualTo(LexemeType.Addition));
        Assert.That(lexemes[2].Value, Is.EqualTo(-1));
    }

    [Test]
    public void TestLexemeParserUnaryMinus3() {
        var lexemes = GetLexemes("1 -1");
        Assert.That(lexemes[1].Type, Is.EqualTo(LexemeType.Subtraction));
        Assert.That(lexemes[2].Value, Is.EqualTo(1));
    }

    [Test]
    public void TestLexemeParserUnaryMinus4() {
        var lexemes = GetLexemes("(1 + 2) -1");
        Assert.That(lexemes[6].Value, Is.EqualTo(1));
    }
}
}