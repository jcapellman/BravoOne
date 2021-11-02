using BravoOne.lib;
using BravoOne.UWP.ViewModels;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BravoOne.UWP.Views
{
    public sealed partial class EndGame : Page
    {
        private EndGameViewModel ViewModel => (EndGameViewModel)DataContext;

        public EndGame()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataContext = new EndGameViewModel((GameWrapper)e.Parameter);

            base.OnNavigatedTo(e);
        }

        private void btnEndGame_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.gWrapper.CurrentGame = null;

            _ = Frame.Navigate(typeof(MainMenu), ViewModel.gWrapper);
        }
    }
}