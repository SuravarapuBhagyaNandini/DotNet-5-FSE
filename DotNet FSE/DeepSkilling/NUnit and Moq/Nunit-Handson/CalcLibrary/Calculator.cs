namespace CalcLibrary
{
    /// <summary>
    /// A simple Calculator class with basic arithmetic operations.
    /// This is the class under test (CUT).
    /// </summary>
    public class Calculator
    {
        public int Add(int a, int b)
        {
            return a + b;
        }

        public int Subtract(int a, int b)
        {
            return a - b;
        }

        public int Multiply(int a, int b)
        {
            return a * b;
        }

        public double Divide(double a, double b)
        {
            if (b == 0)
                throw new System.DivideByZeroException("Cannot divide by zero.");
            return a / b;
        }
    }
}
