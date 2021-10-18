using BravoOne.lib;
using BravoOne.UWP.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BravoOne.UWP
{
    public sealed partial class LoadGame : Page
    {
        public LoadGameViewModel ViewModel => (LoadGameViewModel)DataContext;

        public LoadGame()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataContext = new LoadGameViewModel((GameWrapper)e.Parameter);

            base.OnNavigatedTo(e);
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