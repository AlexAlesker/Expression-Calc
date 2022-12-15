using NUnit.Framework;

namespace ExpressionCalc {

[TestFixture]
internal class ExpressionCalcTestCase {

    [Test]
    public void TestExpressionCalc1() {
        Assert.That(ExpressionCalc.Calculate("1"), Is.EqualTo(1));
    }

    [Test]
    public void TestExpressionCalc2() {
        Assert.That(ExpressionCalc.Calculate("1 + 2"), Is.EqualTo(3));
    }

    [Test]
    public void TestExpressionCalc3() {
        Assert.That(ExpressionCalc.Calculate("1 + 2 * 3"), Is.EqualTo(7));
    }

    [Test]
    public void TestExpressionCalc4() {
        Assert.That(ExpressionCalc.Calculate("(1 + 2) * 3"), Is.EqualTo(9));
    }

    [Test]
    public void TestExpressionCalc5() {
        Assert.That(ExpressionCalc.Calculate("3 + 4.1 * .2 / (1 - 5)"), Is.EqualTo(2.795));
    }
}
}
