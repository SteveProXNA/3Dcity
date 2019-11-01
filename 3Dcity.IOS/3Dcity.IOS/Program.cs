using Foundation;
using UIKit;

namespace WindowsGame
{
    [Register("AppDelegate")]
    class Program : UIApplicationDelegate
    {
        private static WindowsGame.Common.AnGame game;

        internal static void RunGame()
        {
            game = new WindowsGame.Common.AnGame();
            game.Run();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            UIApplication.Main(args, null, "AppDelegate");
        }

        public override void FinishedLaunching(UIApplication app)
        {
            RunGame();
        }

    }
}
