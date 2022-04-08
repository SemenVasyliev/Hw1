namespace Hw1.Exercise2
{
    /// <summary>
    /// Area calculator application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Application entry point.
        /// </summary>
        private static int Main(string[] args)
        {
            var app = new AreaCalcApplication();
            return app.Run(args);
        }
    }
}
