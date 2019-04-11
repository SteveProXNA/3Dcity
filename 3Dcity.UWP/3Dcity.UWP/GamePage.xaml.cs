using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WindowsGame.Common;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace _3Dcity.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        readonly AnGame _game;

        public GamePage()
        {
            this.InitializeComponent();

            // Create the game.
            var launchArguments = string.Empty;
            _game = MonoGame.Framework.XamlGame<AnGame>.Create(launchArguments, Window.Current.CoreWindow, swapChainPanel);
        }
    }
}
