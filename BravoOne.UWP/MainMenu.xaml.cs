using BravoOne.lib;
using BravoOne.UWP.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BravoOne.UWP
{
    public sealed partial class MainMenu : Page
    {
        private MainMenuViewModel ViewModel => (MainMenuViewModel)DataContext;

        public MainMenu()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataContext = new MainMenuViewModel((GameWrapper)e.Parameter);

            base.OnNavigatedTo(e);
        }

        private void btnNewGame_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _ = Frame.Navigate(typeof(NewGame), ViewModel.gWrapper);
        }

        private void btnLoadGame_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _ = Frame.Navigate(typeof(LoadGame), ViewModel.gWrapper);
        }

        private void btnOptions_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _ = Frame.Navigate(typeof(Options), ViewModel.gWrapper);
        }

        private void btnCredits_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _ = Frame.Navigate(typeof(Credits), ViewModel.gWrapper);
        }
    }
}