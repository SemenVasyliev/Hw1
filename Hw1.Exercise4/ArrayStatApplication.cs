using System;
using System.Collections.Generic;

namespace Hw1.Exercise4
{
    /// <summary>
    /// Array statistics application core.
    /// </summary>
    public class ArrayStatApplication
    {
        /// <summary>
        /// Runs array statistics application.
        /// Prints statistics.
        /// </summary>
        /// <param name="args">
        /// Arguments - integer array.
        /// </param>
        /// <returns>Returns : 
        /// <c>0</c> in case of success; 
        /// <c>-1</c> in case of invalid <paramref name="args"/>.
        /// </returns>
        public int Run(string[] args)
        {
            string sorted = "";
            List<int> nums = new List<int>();
            if (args == null)
            {
                return -1;
            }
            for (int i = 0; i < args.Length; i++)
            {
                int temp;
                if (int.TryParse(args[i], out temp) == false)
                {
                    return -1;
                }

                nums.Add(temp);
            }

            Sort(nums);
            Console.WriteLine("Min=" + nums[0]);
            Console.WriteLine("Max=" + nums[nums.Count - 1]);
            int sum = Sum(nums);
            Console.WriteLine("Sum=" + sum);
            double count = nums.Count;
            double average = sum / count;
            Console.WriteLine("Average=" + average);
            Console.WriteLine("Count=" + nums.Count);
            for (int i = 0; i < nums.Count; i++)
            {
                sorted += nums[i];
                sorted += ";";
            }
            sorted = sorted.Remove(sorted.Length - 1);
            Console.WriteLine("Sorted=" + sorted);
            return 0;
        }

        public static int Sum(List<int> nums)
        {
            int sum = 0;
            for (int i = 0; i < nums.Count; i++)
            {
                sum += nums[i];
            }
            return sum;
        }

        public static void Sort(List<int> nums)
        {
            int temp;
            for (int i = 0; i < nums.Count; i++)
            {
                for (int j = i + 1; j < nums.Count; j++)
                {
                    if (nums[i] > nums[j])
                    {
                        temp = nums[i];
                        nums[i] = nums[j];
                        nums[j] = temp;
                    }
                }
            }
        }
    }
}
