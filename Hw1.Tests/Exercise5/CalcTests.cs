using Hw1.Exercise5;
using Hw1.Tests.Tools;
using Xunit;

#pragma warning disable CA1707 // Test name can contains underscores
namespace Hw1.Tests.Exercise5
{
    public class CalcTests
    {
        [Theory]
        // Echo
        [InlineData(new string[] { "1" }, 1d, 0)]
        [InlineData(new string[] { "-1" }, -1d, 0)]
        [InlineData(new string[] { "-", "1" }, -1d, 0)]
        [InlineData(new string[] { "1.23" }, 1.23d, 0)]
        [InlineData(new string[] { "1,23" }, 1.23d, 0)]
        [InlineData(new string[] { "-", "1,23" }, -1.23d, 0)]
        // Factorial
        [InlineData(new string[] { "0!" }, 1d, 0)]
        [InlineData(new string[] { "1!" }, 1d, 0)]
        [InlineData(new string[] { "10!" }, 3_628_800d, 0)]
        [InlineData(new string[] { "18!" }, 6_402_373_705_728_000d, 0)]
        // Sum
        [InlineData(new string[] { "1+2" }, 3d, 0)]
        [InlineData(new string[] { "1+", "2" }, 3d, 0)]
        [InlineData(new string[] { "1", "+2" }, 3d, 0)]
        [InlineData(new string[] { "1", "+", "2" }, 3d, 0)]
        [InlineData(new string[] { "-1", "+", "-", "2" }, -3d, 0)]
        [InlineData(new string[] { "-1.1+-2,2" }, -3.3d, 0)]
        [InlineData(new string[] { "-1.1", "+", "-", "2.2" }, -3.3d, 0)]
        [InlineData(new string[] { "-1.1", "+", "-", "2,2" }, -3.3d, 0)]
        // Sub
        [InlineData(new string[] { "1-2" }, -1d, 0)]
        [InlineData(new string[] { "1-", "2" }, -1d, 0)]
        [InlineData(new string[] { "1", "-2" }, -1d, 0)]
        [InlineData(new string[] { "1", "-", "2" }, -1d, 0)]
        [InlineData(new string[] { "-1--2" }, 1d, 0)]
        [InlineData(new string[] { "-1", "-", "-", "2" }, 1d, 0)]
        [InlineData(new string[] { "2.22-1,2" }, 1.02d, 0)]
        // Mul
        [InlineData(new string[] { "1*2" }, 2d, 0)]
        [InlineData(new string[] { "1x2" }, 2d, 0)]
        [InlineData(new string[] { "1X2" }, 2d, 0)]
        [InlineData(new string[] { "1", "x2" }, 2d, 0)]
        [InlineData(new string[] { "1x", "2" }, 2d, 0)]
        [InlineData(new string[] { "1x", "-2" }, -2d, 0)]
        [InlineData(new string[] { "-", "1x", "-2" }, 2d, 0)]
        [InlineData(new string[] { "-1*-2" }, 2d, 0)]
        [InlineData(new string[] { "1.1*2,0" }, 2.2d, 0)]
        [InlineData(new string[] { "1,1x2.0" }, 2.2d, 0)]
        [InlineData(new string[] { "1.0X2.0" }, 2d, 0)]
        [InlineData(new string[] { "1.1", "x2" }, 2.2d, 0)]
        [InlineData(new string[] { "1x", "2.2" }, 2.2d, 0)]
        [InlineData(new string[] { "1,1x", "-2.0" }, -2.2d, 0)]
        [InlineData(new string[] { "-", "1x", "-2,0" }, 2d, 0)]
        [InlineData(new string[] { "-1.1*-2.2" }, 2.42d, 0)]
        // Div
        [InlineData(new string[] { "4/2" }, 2d, 0)]
        [InlineData(new string[] { "4\\2" }, 2d, 0)]
        [InlineData(new string[] { "4", "/", "2" }, 2d, 0)]
        [InlineData(new string[] { "4", "\\", "2" }, 2d, 0)]
        [InlineData(new string[] { "4.4", "/2" }, 2.2d, 0)]
        [InlineData(new string[] { "4,4/", "2" }, 2.2d, 0)]
        // Pow
        [InlineData(new string[] { "2^1" }, 2d, 0)]
        [InlineData(new string[] { "2^2" }, 4d, 0)]
        [InlineData(new string[] { "4^0.5" }, 2d, 0)]
        [InlineData(new string[] { "2^", "1" }, 2d, 0)]
        [InlineData(new string[] { "2", "^2" }, 4d, 0)]
        [InlineData(new string[] { "4,0", "^", "0.5" }, 2d, 0)]
        public void Run_WithValidArgs_ReturnsCalcResult(string[] args, double result, int successCode)
        {
            using var output = ConsoleOutputInterceptor.InterceptOutput();
            var app = new CalcApplication();

            var returnCode = app.Run(args);
            Assert.Equal(successCode, returnCode);

            var outputStr = output.ToString().NormalizeOutput(trimNewLineEnding: true);
            var outputResult = outputStr.ParseDouble();
            Assert.Equal(result, outputResult, 1);
        }

        [Theory]
        [InlineData(new string[] { "1 / 0" }, -2)]
        [InlineData(new string[] { "1", "/ 0" }, -2)]
        [InlineData(new string[] { "-10 / 0" }, -2)]
        [InlineData(new string[] { "-2^0.5" }, -2)]
        [InlineData(new string[] { "-1!" }, -2)]
        [InlineData(new string[] { "19!" }, -2)]
        [InlineData(new string[] { "2.34!" }, -2)]
        [InlineData(new string[] { "2,34!" }, -2)]
        [InlineData(new string[] { "1000000!" }, -2)]
        public void Run_WithInvalidArgs_ReturnsError(string[] args, int errorCode)
        {
            var app = new CalcApplication();
            var returnCode = app.Run(args);
            Assert.Equal(errorCode, returnCode);
        }

        [Theory]
        [InlineData(null, -1)]
        [InlineData(new string[] { "" }, -1)]
        [InlineData(new string[] { "invalid", "format" }, -1)]
        [InlineData(new string[] { "1+d2" }, -1)]
        [InlineData(new string[] { "1 z 2" }, -1)]
        [InlineData(new string[] { "-1 z -2" }, -1)]
        [InlineData(new string[] { "1 --- 2" }, -1)]
        [InlineData(new string[] { "1xx2" }, -1)]
        [InlineData(new string[] { "1+--2" }, -1)]
        [InlineData(new string[] { "1+2+3" }, -1)]
        [InlineData(new string[] { "1+2-3" }, -1)]
        public void Run_WithInvalidFormatArgs_ReturnsError(string[] args, int errorCode)
        {
            var app = new CalcApplication();
            var returnCode = app.Run(args);
            Assert.Equal(errorCode, returnCode);
        }
    }
}
#pragma warning restore CA1707 // Test name can contains underscores
