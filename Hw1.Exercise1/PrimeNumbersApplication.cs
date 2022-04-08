using System;

namespace Hw1.Exercise1
{
    /// <summary>
    /// Prime numbers application core.
    /// </summary>
    public class PrimeNumbersApplication
    {
        /// <summary>
        /// Runs prime numbers application.
        /// Prints prime numbers in the given range (from <paramref name="args"/>) to the output.
        /// </summary>
        /// <param name="args">
        /// Arguments - valid integer range [from, to] 
        /// between <see cref="int.MinValue"/> and <see cref="int.MaxValue"/>
        /// to find prime numbers.
        /// </param>
        /// <returns>Return <c>0</c> in case of success and <c>-1</c> in case of failure.</returns>
        public int Run(string[] args)
        {
            int minvalue;
            int maxvalue;
            if (args == null || int.TryParse(args[0], out minvalue) == false ||
            int.TryParse(args[1], out maxvalue) == false)
            {
                return -1;
            }

            if (minvalue < 0)
            {
                minvalue = 0;
            }

            if (maxvalue < 0)
            {
                maxvalue = 0;
            }

            if (minvalue > maxvalue)
            {
                int temp = maxvalue;
                maxvalue = minvalue;
                minvalue = temp;
            }

            for (int i = minvalue; i <= maxvalue; i++)
            {
                if (IsPrime(i))
                {
                    Console.Write(i + ";");
                }
            }
            return 0;
        }
        public bool IsPrime(int a)
        {
            if (a is 1 or 0)
            {
                return false;
            }
            for (int i = 2; i <= Math.Sqrt(a); i++)
            {
                if (a % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
