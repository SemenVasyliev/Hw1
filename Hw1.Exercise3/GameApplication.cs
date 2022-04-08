using System;

namespace Hw1.Exercise3
{
    /// <summary>
    /// 'Rock-Paper-Scissors' game application core.
    /// </summary>
    public class GameApplication
    {
        /// <summary>
        /// Runs game application.
        /// Prints game results.
        /// </summary>
        /// <param name="args">
        /// Arguments - player's shape.
        /// args[0] - shape [Rock, Paper, Scissors].
        /// </param>
        /// <returns>Returns : 
        /// <c>0</c> in case of success; 
        /// <c>-1</c> in case of invalid <paramref name="args"/>.
        /// </returns>
        public int Run(string[] args)
        {
            if (args == null)
            {
                return -1;
            }
            string[] words = new string[] { "Rock", "Scissors", "Paper" };
            Random rnd = new Random();
            int value = rnd.Next(0, 2);
            args[0] = args[0].ToLower();
            string player_word = args[0];

            switch (player_word)
            {
                case "rock":
                    if (words[value].ToLower() == "rock")
                    {
                        Console.WriteLine("Player=Rock:Draw");
                        Console.WriteLine("Computer=Rock:Draw");
                    }
                    else if (words[value].ToLower() == "paper")
                    {
                        Console.WriteLine("Player=Rock:Lose");
                        Console.WriteLine("Computer=Paper:Win");
                    }
                    else
                    {
                        Console.WriteLine("Player=Rock:Win");
                        Console.WriteLine("Computer=Scissors:Lose");
                    }
                    return 0;

                case "scissors":
                    if (words[value].ToLower() == "scissors")
                    {
                        Console.WriteLine("Player=Scissors:Draw");
                        Console.WriteLine("Computer=Scissors:Draw");
                    }
                    else if (words[value].ToLower() == "paper")
                    {
                        Console.WriteLine("Player=Scissors:Win");
                        Console.WriteLine("Computer=Paper:Lose");
                    }
                    else
                    {
                        Console.WriteLine("Player=Scissors:Lose");
                        Console.WriteLine("Computer=Rock:Win");
                    }
                    return 0;

                case "paper":
                    if (words[value].ToLower() == "paper")
                    {
                        Console.WriteLine("Player=Paper:Draw");
                        Console.WriteLine("Computer=Paper:Draw");
                    }
                    else if (words[value].ToLower() == "scissors")
                    {
                        Console.WriteLine("Player=Paper:Lose");
                        Console.WriteLine("Computer=Scissors:Win");
                    }
                    else
                    {
                        Console.WriteLine("Player=Paper:Win");
                        Console.WriteLine("Computer=Rock:Lose");
                    }
                    return 0;

                default:
                    return -1;
            }
        }
    }
}
