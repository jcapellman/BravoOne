using BravoOne.UWP.ViewModels;

using Windows.UI.Xaml.Controls;

namespace BravoOne.UWP
{
    public sealed partial class LoadGame : Page
    {
        public LoadGameViewModel ViewModel => (LoadGameViewModel)DataContext;

        public LoadGame()
        {
            this.InitializeComponent();
        }

        private void btnBack_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void btnLoadGame_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.LoadGame();

            _ = Frame.Navigate(typeof(MainPage), ViewModel.gWrapper);
        }
    }
}