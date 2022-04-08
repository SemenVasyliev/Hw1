using System;
using System.Collections.Generic;
using System.Linq;
using Hw1.Exercise3;
using Hw1.Tests.Tools;
using Xunit;

#pragma warning disable CA1707 // Test name can contains underscores
namespace Hw1.Tests.Exercise3
{
    public class GameTests
    {
        private static class Shape
        {
            public const string Rock = "Rock";
            public const string Paper = "Paper";
            public const string Scissors = "Scissors";
        }

        private static class ShapeRC
        {
            public const string Rock = "roCK";
            public const string Paper = "PaPer";
            public const string Scissors = "SciSSorS";
        }

        private static class Outcome
        {
            public const string Win = "Win";
            public const string Lose = "Lose";
            public const string Draw = "Draw";
        }

        private record GameResult(string Player, string Computer);

        [Theory]
        [InlineData(Shape.Rock, Shape.Scissors, Shape.Paper)]
        [InlineData(Shape.Paper, Shape.Rock, Shape.Scissors)]
        [InlineData(Shape.Scissors, Shape.Paper, Shape.Rock)]
        [InlineData(ShapeRC.Rock, Shape.Scissors, Shape.Paper)]
        [InlineData(ShapeRC.Paper, Shape.Rock, Shape.Scissors)]
        [InlineData(ShapeRC.Scissors, Shape.Paper, Shape.Rock)]
        public void Run_WithValidArgs_ReturnsGameResults(string playerShapeArg, string compLoseShape, string compWinsShape)
        {
            using var output = ConsoleOutputInterceptor.InterceptOutput();
            var app = new GameApplication();

            var returnCode = app.Run(new string[] { playerShapeArg });
            Assert.Equal(0, returnCode);

            var outputStr = output.ToString();
            Assert.NotNull(outputStr);

            var result = outputStr.KeyValueToObject<GameResult>();

            Assert.NotNull(result.Player);
            Assert.NotNull(result.Computer);

            var playerGame = result.Player.Split(':').Select(s => s.Trim()).ToArray();
            var compGame = result.Computer.Split(':').Select(s => s.Trim()).ToArray();

            Assert.Equal(2, playerGame.Length);
            Assert.Equal(2, compGame.Length);

            var playerShape = playerGame[0];
            var playerOutcome = playerGame[1];
            var compShape = compGame[0];
            var compOutcome = compGame[1];

            Assert.Equal(playerShapeArg, playerShape, ignoreCase: true);

            var possibleShapes = new HashSet<string>() { Shape.Paper, Shape.Rock, Shape.Scissors };
            var shapes = new HashSet<string>() { playerShape, compShape };
            Assert.Subset(possibleShapes, shapes);

            var sc = StringComparer.OrdinalIgnoreCase;
            if (sc.Equals(playerShape, compShape))
            {
                Assert.Equal(playerOutcome, Outcome.Draw);
                Assert.Equal(compOutcome, Outcome.Draw);
            }

            if (sc.Equals(compLoseShape, compShape))
            {
                Assert.Equal(playerOutcome, Outcome.Win);
                Assert.Equal(compOutcome, Outcome.Lose);
            }

            if (sc.Equals(compWinsShape, compShape))
            {
                Assert.Equal(playerOutcome, Outcome.Lose);
                Assert.Equal(compOutcome, Outcome.Win);
            }
        }

        [Theory]
        [InlineData(null, -1)]
        [InlineData(new string[] { "" }, -1)]
        [InlineData(new string[] { "invalid", "input" }, -1)]
        [InlineData(new string[] { "ro ck" }, -1)]
        [InlineData(new string[] { "Pa per" }, -1)]
        [InlineData(new string[] { "Scisors" }, -1)]
        public void Run_WithInvalidArgs_ReturnsError(string[] args, int errorCode)
        {
            var app = new GameApplication();
            var returnCode = app.Run(args);
            Assert.Equal(errorCode, returnCode);
        }
    }
}
#pragma warning restore CA1707 // Test name can contains underscores
