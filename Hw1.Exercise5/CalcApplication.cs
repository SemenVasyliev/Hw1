using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Hw1.Exercise5
{
    /// <summary>
    /// Calc application core.
    /// </summary>
    public class CalcApplication
    {
        /// <summary>
        /// Runs calc application.
        /// Prints calculation result.
        /// </summary>
        /// <param name="args"> Math expression.</param>
        /// <returns>Returns : 
        /// <c>0</c> in case of success; 
        /// <c>-1</c> in case of invalid format of <paramref name="args"/>;
        /// <c>-2</c> in case of invalid math expression <paramref name="args"/>.
        /// </returns>
        public int Run(string[] args)
        {
            if (args == null)
            {
                return -1;
            }
            string pattern = @"^-?\d{1,}(\.\d{1,})?[-+*/\\xX^]-?\d{1,}(\.\d{1,})?$";
            string patternFac = @"^-?\d{1,}(\.\d{1,})?[!]-?$";
            string patternEcho = @"^-?\d{1,}(\.\d{1,})?$";
            string b = "";
            for (int i = 0; i < args.Length; i++)
            {
                b += args[i];
            }
            b = b.Replace(',', '.'); 
            if (Regex.Match(b, pattern).Success)
            {
                double result = 0;
                double a = 0;
                double c = 0;
                string left;
                string right;
                int temp = 0;
                string oper = "";
                for (int i = 1; i < b.Length; i++)
                {
                    oper = b[i].ToString();
                    if (oper != "." && int.TryParse(oper, NumberStyles.Any, CultureInfo.InvariantCulture, out temp) == false)
                    {
                        temp = i;
                        break;

                    }
                }

                right = b.Substring(temp + 1, b.Length - (temp + 1));
                left = b.Substring(0, temp);
                double.TryParse(left, NumberStyles.Any, CultureInfo.InvariantCulture, out a);
                double.TryParse(right, NumberStyles.Any, CultureInfo.InvariantCulture, out c);

                switch (oper)
                {
                    case "+":
                        result = a + c;
                        Console.WriteLine(result);
                        return 0;
                    case "-":
                        result = a - c;
                        Console.WriteLine(result);
                        return 0;
                    case "*":
                        result = a * c;
                        Console.WriteLine(result);
                        return 0;
                    case "/":
                        if (c == 0)
                        {
                            return -2;
                        }
                        result = a / c;
                        Console.WriteLine(result);
                        return 0;
                    case "\\":
                        goto case "/";
                    case "x":
                        goto case "*";
                    case "X":
                        goto case "*";
                    case "^":
                        if (c < 0 || a < 0)
                        {
                            return -2;
                        }
                        Console.WriteLine(Math.Pow(a, c));
                        return 0;
                    default:
                        break;
                }

            }
            else if (Regex.Match(b, patternFac).Success) 
            {
                int temp;
                b = b.Remove(b.Length - 1);

                if (int.TryParse(b, NumberStyles.Any, CultureInfo.InvariantCulture, out temp) == false)
                {
                    return -2;
                }

                long n = long.Parse(b, NumberStyles.Any, CultureInfo.InvariantCulture);

                if (n > 18 || n < 0)
                {
                    return -2;
                }

                long result = Factorial(n);
                Console.WriteLine(result);
                return 0;
            }
            else if (Regex.Match(b, patternEcho).Success) 
            {
                double result = double.Parse(b, NumberStyles.Any, CultureInfo.InvariantCulture);
                Console.WriteLine(result);
                return 0;
            }
            else if (b.EndsWith("0"))
            {
                return -2;
            }
            else
            {
                return -1;
            }
            return 0;
        }
        public static long Factorial(long n)
        {
            if (n == 1 || n == 0)
                return 1;

            return n * Factorial(n - 1);
        }

    }
}
