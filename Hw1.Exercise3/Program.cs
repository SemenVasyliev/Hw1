namespace Hw1.Exercise3
{
    /// <summary>
    /// 'Rock-Paper-Scissors' game application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Application entry point.
        /// </summary>
        private static int Main(string[] args)
        {
            var app = new GameApplication();
            return app.Run(args);
        }
    }
}
