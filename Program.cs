using System;

namespace CalculatorApp
{
    public class Calculator
    {
        // Private fields (Encapsulation)
        private double num1;
        private double num2;

        // Public methods
        public double Add(double a, double b)
        {
            num1 = a;
            num2 = b;
            return num1 + num2;
        }

        public double Subtract(double a, double b)
        {
            num1 = a;
            num2 = b;
            return num1 - num2;
        }

        public double Multiply(double a, double b)
        {
            num1 = a;
            num2 = b;
            return num1 * num2;
        }

        public double Divide(double a, double b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero.");
            }

            num1 = a;
            num2 = b;
            return num1 / num2;
        }
    }
}
