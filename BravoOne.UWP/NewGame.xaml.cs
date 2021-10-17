using BravoOne.lib;
using BravoOne.UWP.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BravoOne.UWP
{
    public sealed partial class NewGame : Page
    {
        public NewGameViewModel ViewModel => (NewGameViewModel)DataContext;

        public NewGame()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataContext = new NewGameViewModel((GameWrapper)e.Parameter);

            base.OnNavigatedTo(e);
        }

        private void btnBack_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void btnStart_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }
    }
}