using System;

namespace ExpressionCalc {

internal class Program {
    static void Main() {
        Console.WriteLine("Enter the expression OR");
        Console.WriteLine("Press <Enter> to cancel processing");
        while (true) {
            var s = Console.ReadLine();

            if (string.IsNullOrEmpty(s)) {
                break;
            }

            Console.WriteLine(ExpressionCalc.Calculate(s));
        }
    }
}
}
