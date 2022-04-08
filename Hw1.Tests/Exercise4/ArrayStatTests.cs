using System;
using System.Linq;
using Hw1.Exercise4;
using Hw1.Tests.Tools;
using Xunit;

#pragma warning disable CA1707 // Test name can contains underscores
namespace Hw1.Tests.Exercise4
{
    public class ArrayStatTests
    {
        private record ArrayStatResult(int Min, int Max, int Sum, string Average, int Count, string Sorted);

        [Theory]
        [InlineData(new[] { "1" }, 1, 1, 1, 1, 1, new[] { "1" })]
        [InlineData(new[] { "1", "2" }, 1, 2, 3, 1.5, 2, new[] { "1", "2" })]
        [InlineData(new[] { "2", "-1", }, -1, 2, 1, 0.5, 2, new[] { "-1", "2" })]
        [InlineData(new[] { "-1", "-2", "-10", "10", "144", "-12", "100", "0" }, -12, 144, 229, 28.625, 8, new[] { "-12", "-10", "-2", "-1", "0", "10", "100", "144" })]
        public void Run_WithValidArgs_ReturnsArrayStat(string[] args, int min, int max, int sum, double average, int count, string[] sorted)
        {
            using var output = ConsoleOutputInterceptor.InterceptOutput();
            var app = new ArrayStatApplication();

            var returnCode = app.Run(args);
            Assert.Equal(0, returnCode);

            var outputStr = output.ToString();
            Assert.NotNull(outputStr);

            var result = outputStr.KeyValueToObject<ArrayStatResult>();

            Assert.NotNull(result.Average);
            Assert.NotNull(result.Sorted);

            Assert.Equal(min, result.Min);
            Assert.Equal(max, result.Max);
            Assert.Equal(sum, result.Sum);
            Assert.Equal(average, result.Average.ParseDouble(), 1);
            Assert.Equal(count, result.Count);

            var sortedResult = result.Sorted.Split(new[] { ' ', ';' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            Assert.Equal(sorted, sortedResult);
        }

        [Theory]
        [InlineData(null, -1)]
        [InlineData(new string[] { "" }, -1)]
        [InlineData(new string[] { "invalid", "input" }, -1)]
        [InlineData(new string[] { "1", "input" }, -1)]
        [InlineData(new string[] { "invalid", "2" }, -1)]
        [InlineData(new string[] { "1", "2", "abc", "4" }, -1)]
        public void Run_WithInvalidArgs_ReturnsError(string[] args, int errorCode)
        {
            var app = new ArrayStatApplication();
            var returnCode = app.Run(args);
            Assert.Equal(errorCode, returnCode);
        }
    }
}
#pragma warning restore CA1707 // Test name can contains underscores
