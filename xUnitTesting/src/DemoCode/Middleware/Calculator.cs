using System;

namespace DemoCode.Middleware
{
    public class Calculator
    {
        public int AddInt(int a, int b)
        {
            return (a + b);
        }

        public double addDouble(double a, double b)
        {
            return (a + b);
        }

        public int Divide(int value, int divideBy)
        {
            if (value > 200)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            return value / divideBy;
        }
    }
}