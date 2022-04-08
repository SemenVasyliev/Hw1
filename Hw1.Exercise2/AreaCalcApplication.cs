using System;
using System.Globalization;

namespace Hw1.Exercise2
{
    /// <summary>
    /// Area calc application core.
    /// </summary>
    public class AreaCalcApplication
    {
        /// <summary>
        /// Runs area calc application.
        /// Prints area of selected shape (from <paramref name="args"/>) to the output.
        /// </summary>
        /// <param name="args">
        /// Arguments - shape with dimensions.
        /// args[0] - shape [Circle, Square, Rect, Triangle].
        /// args[0..2] - shape dimensions.
        /// </param>
        /// <returns>Returns : 
        /// <c>0</c> in case of success; 
        /// <c>-1</c> in case of invalid <paramref name="args"/>;
        /// <c>-2</c> in case of invalid dimensions.
        /// </returns>
        public int Run(string[] args)
        {
            double result;
            if (args == null)
            {
                return -1;
            }
            for (int i = 1; i < args.Length; i++)
            {
                double temp = 0;
                args[i] = args[i].Replace(',', '.');
                if (double.TryParse(args[i], NumberStyles.Any, CultureInfo.InvariantCulture, out temp) == false)
                {
                    return -1;
                }
                else if (temp <= 0)
                {
                    return -2;
                }
            }

            switch (args[0].ToLower())
            {

                case "circle":
                    if (args.Length == 2)
                    {
                        result = Circle(args);
                        if (result == -2)
                        {
                            return -2;
                        }
                        Console.Write(result);
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }

                case "square":
                    if (args.Length == 2)
                    {
                        result = Square(args);
                        if (result == -2)
                        {
                            return -2;
                        }
                        Console.Write(result);
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }

                case "rect":
                    if (args.Length == 3)
                    {
                        result = Rect(args);
                        if (result == -2)
                        {
                            return -2;
                        }
                        Console.Write(result);
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }

                case "triangle":
                    if (args.Length == 3 || args.Length == 4)
                    {
                        result = Triangle(args);
                        if (result == -2)
                        {
                            return -2;
                        }
                        Console.Write(result);
                        return 0;

                    }
                    else
                    {
                        return -1;
                    }

                default:
                    return -1;

            }
        }

        public static double Circle(string[] args)
        {
            double result;
            double radius;
            args[1].Replace(',', '.');
            double.TryParse(args[1], NumberStyles.Any, CultureInfo.InvariantCulture, out radius);

            if (radius <= 0)
            {
                return -2;
            }

            result = Math.Pow(radius, 2) * Math.PI;
            result = Math.Round(result, 2);

            return result;
        }

        public static double Square(string[] args)
        {
            double result;
            double side;
            args[1].Replace(',', '.');
            double.TryParse(args[1], NumberStyles.Any, CultureInfo.InvariantCulture, out side);

            if (side <= 0)
            {
                return -2;
            }

            result = Math.Pow(side, 2);
            result = Math.Round(result, 2);

            return result;
        }

        public static double Rect(string[] args)
        {
            double result;
            double side2;
            double side;
            args[1].Replace(',', '.');
            args[2].Replace(',', '.');
            double.TryParse(args[1], NumberStyles.Any, CultureInfo.InvariantCulture, out side);
            double.TryParse(args[2], NumberStyles.Any, CultureInfo.InvariantCulture, out side2);

            if (side <= 0 || side2 <= 0)
            {
                return -2;
            }

            result = side * side2;
            result = Math.Round(result, 2);

            return result;
        }

        public static double Triangle(string[] args)
        {
            double result = 0;
            args[1].Replace(',', '.');
            args[2].Replace(',', '.');

            switch (args.Length)
            {
                case 3:
                    double basse;
                    double height;
                    double.TryParse(args[1], NumberStyles.Any, CultureInfo.InvariantCulture, out basse);
                    double.TryParse(args[2], NumberStyles.Any, CultureInfo.InvariantCulture, out height);
                    result = (basse * height) / 2;
                    return result;

                case 4:
                    double side1;
                    double side2;
                    double side3;
                    double.TryParse(args[1], NumberStyles.Any, CultureInfo.InvariantCulture, out side1);
                    double.TryParse(args[2], NumberStyles.Any, CultureInfo.InvariantCulture, out side2);
                    double.TryParse(args[3], NumberStyles.Any, CultureInfo.InvariantCulture, out side3);
                    if (TriangleExistence(side1, side2, side3))
                    {
                        double p = (side1 + side2 + side3) / 2;
                        result = Math.Sqrt(p * (p - side1) * (p - side2) * (p - side3));
                        return result;
                    }
                    else
                    {
                        return -2;
                    }


                default:
                    break;
            }

            return result;
        }
        public static bool TriangleExistence(double side1, double side2, double side3)
        {
            if (side1 + side2 > side3)
            {
                if (side1 + side3 > side2)
                {
                    if (side3 + side2 > side1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
