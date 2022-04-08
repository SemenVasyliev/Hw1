using Hw1.Exercise2;
using Hw1.Tests.Tools;
using Xunit;

#pragma warning disable CA1707 // Test name can contains underscores
namespace Hw1.Tests.Exercise2
{
    public class AreaCalcTests
    {
        private static class Shape
        {
            public const string Circle = "Circle";
            public const string CircleRC = "cIRCle";

            public const string Square = "Square";
            public const string SquareRC = "squARE";

            public const string Rect = "Rect";
            public const string RectRC = "reCt";

            public const string Triangle = "Triangle";
            public const string TriangleRC = "trianGle";
        }

        [Theory]
        // Circle
        [InlineData(new string[] { Shape.Circle, "30" }, 2827.43d, 0)]
        [InlineData(new string[] { Shape.Circle, "1.23" }, 4.75d, 0)]
        [InlineData(new string[] { Shape.Circle, "1,23" }, 4.75d, 0)]
        // Square
        [InlineData(new string[] { Shape.Square, "30" }, 900d, 0)]
        [InlineData(new string[] { Shape.Square, "1.23" }, 1.51d, 0)]
        [InlineData(new string[] { Shape.Square, "1,23" }, 1.51d, 0)]
        // Rect
        [InlineData(new string[] { Shape.Rect, "30", "30" }, 900d, 0)]
        [InlineData(new string[] { Shape.Rect, "1.23", "4,56" }, 5.60d, 0)]
        // Triangle-2
        [InlineData(new string[] { Shape.Triangle, "30", "30" }, 450d, 0)]
        [InlineData(new string[] { Shape.Triangle, "30.1", "30,2" }, 454.51d, 0)]
        // Triangle-3
        [InlineData(new string[] { Shape.Triangle, "30", "45", "50" }, 666.585d, 0)]
        [InlineData(new string[] { Shape.Triangle, "30.1", "45,2", "50,3" }, 672.148d, 0)]

        public void Run_WithValidShape_ReturnsShapeArea(string[] args, double area, int successCode)
        {
            using var output = ConsoleOutputInterceptor.InterceptOutput();
            var app = new AreaCalcApplication();

            var returnCode = app.Run(args);
            Assert.Equal(successCode, returnCode);

            var outputStr = output.ToString().NormalizeOutput(trimNewLineEnding: true);
            var outputArea = outputStr.ParseDouble();
            Assert.Equal(area, outputArea, 1);
        }

        [Theory]
        // Circle
        [InlineData(new string[] { Shape.CircleRC, "30" }, 2827.43d, 0)]
        [InlineData(new string[] { Shape.CircleRC, "1.23" }, 4.75d, 0)]
        [InlineData(new string[] { Shape.CircleRC, "1,23" }, 4.75d, 0)]
        // Square
        [InlineData(new string[] { Shape.SquareRC, "30" }, 900d, 0)]
        [InlineData(new string[] { Shape.SquareRC, "1.23" }, 1.51d, 0)]
        [InlineData(new string[] { Shape.SquareRC, "1,23" }, 1.51d, 0)]
        // Rect
        [InlineData(new string[] { Shape.RectRC, "30", "30" }, 900d, 0)]
        [InlineData(new string[] { Shape.RectRC, "1.23", "4,56" }, 5.60d, 0)]
        // Triangle-2
        [InlineData(new string[] { Shape.TriangleRC, "30", "30" }, 450d, 0)]
        [InlineData(new string[] { Shape.TriangleRC, "30.1", "30,2" }, 454.51d, 0)]
        // Triangle-3
        [InlineData(new string[] { Shape.TriangleRC, "30", "45", "50" }, 666.585d, 0)]
        [InlineData(new string[] { Shape.TriangleRC, "30.1", "45,2", "50,3" }, 672.148d, 0)]

        public void Run_WithValidShapeRandomCase_ReturnsShapeArea(string[] args, double area, int successCode)
        {
            using var output = ConsoleOutputInterceptor.InterceptOutput();
            var app = new AreaCalcApplication();

            var returnCode = app.Run(args);
            Assert.Equal(successCode, returnCode);

            var outputStr = output.ToString().NormalizeOutput(trimNewLineEnding: true);
            var outputArea = outputStr.ParseDouble();
            Assert.Equal(area, outputArea, 1);
        }

        [Theory]
        [InlineData(new string[] { Shape.Circle, "-2" }, -2)]
        [InlineData(new string[] { Shape.Circle, "-2.1" }, -2)]
        [InlineData(new string[] { Shape.Circle, "-2,1" }, -2)]
        [InlineData(new string[] { Shape.Rect, "-3" }, -2)]
        [InlineData(new string[] { Shape.Square, "2", "-3" }, -2)]
        [InlineData(new string[] { Shape.Triangle, "3", "-1" }, -2)]
        [InlineData(new string[] { Shape.Triangle, "3", "1", "1" }, -2)]
        [InlineData(new string[] { Shape.Triangle, "3.1", "1.2", "1,1" }, -2)]
        public void Run_WithInvalidDimensions_ReturnsError(string[] args, int errorCode)
        {
            var app = new AreaCalcApplication();
            var returnCode = app.Run(args);
            Assert.Equal(errorCode, returnCode);
        }

        [Theory]
        [InlineData(null, -1)]
        [InlineData(new string[] { "" }, -1)]
        [InlineData(new string[] { "invalid", "input" }, -1)]
        [InlineData(new string[] { Shape.Circle }, -1)]
        [InlineData(new string[] { Shape.Rect }, -1)]
        [InlineData(new string[] { Shape.Square }, -1)]
        [InlineData(new string[] { Shape.Triangle }, -1)]
        [InlineData(new string[] { Shape.Circle, "abc" }, -1)]
        [InlineData(new string[] { Shape.Square, "12c4" }, -1)]
        [InlineData(new string[] { Shape.Rect, "14" }, -1)]
        [InlineData(new string[] { Shape.Rect, "14", "d" }, -1)]
        [InlineData(new string[] { Shape.Triangle, "1", "2", "three" }, -1)]
        [InlineData(new string[] { "trangle", "1", "2" }, -1)]
        [InlineData(new string[] { "sqre", "1", "2" }, -1)]
        public void Run_WithInvalidArgs_ReturnsError(string[] args, int errorCode)
        {
            var app = new AreaCalcApplication();
            var returnCode = app.Run(args);
            Assert.Equal(errorCode, returnCode);
        }
    }
}
#pragma warning restore CA1707 // Test name can contains underscores
