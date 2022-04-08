using Hw1.Exercise1;
using Hw1.Tests.Tools;
using Xunit;

#pragma warning disable CA1707 // Test name can contains underscores
namespace Hw1.Tests.Exercise1
{
    public class PrimeNumbersTests
    {
        [Theory]
        [InlineData(new string[] { "5", "7" }, "5;7;", 0)]
        [InlineData(new string[] { "0", "10" }, "2;3;5;7;", 0)]
        [InlineData(new string[] { "-10", "10" }, "2;3;5;7;", 0)]
        [InlineData(new string[] { "10", "-10" }, "2;3;5;7;", 0)]
        [InlineData(new string[] { "104", "112" }, "107;109;", 0)]
        [InlineData(new string[] { "112", "104" }, "107;109;", 0)]
        public void Run_WithValidRange_ReturnsPrimeNumbers(string[] args, string primeNumbers, int successCode)
        {
            using var output = ConsoleOutputInterceptor.InterceptOutput();
            var app = new PrimeNumbersApplication();

            var returnCode = app.Run(args);
            Assert.Equal(successCode, returnCode);

            var outputStr = output.ToString().NormalizeOutput(trimNewLineEnding: true);
            Assert.Equal(primeNumbers, outputStr);
        }

        [Theory]
        [InlineData(null, -1)]
        [InlineData(new string[] { "" }, -1)]
        [InlineData(new string[] { "1,2", "" }, -1)]
        [InlineData(new string[] { "1.2", "3,4" }, -1)]
        [InlineData(new string[] { "1", "two" }, -1)]
        [InlineData(new string[] { "one", "two" }, -1)]
        [InlineData(new string[] { "one", "two", "1", "2" }, -1)]
        public void Run_WithInvalidArgs_ReturnsError(string[] args, int errorCode)
        {
            var app = new PrimeNumbersApplication();
            var returnCode = app.Run(args);
            Assert.Equal(errorCode, returnCode);
        }
    }
}
#pragma warning restore CA1707 // Test name can contains underscores
