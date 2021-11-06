using BravoOne.lib;
using BravoOne.lib.Objects;
using BravoOne.UWP.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BravoOne.UWP.Views
{
    public sealed partial class Contracts : Page
    {
        private ContractsViewModel ViewModel => (ContractsViewModel)DataContext;

        public Contracts()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataContext = new ContractsViewModel((GameWrapper)e.Parameter);

            base.OnNavigatedTo(e);
        }

        private void btnBack_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), ViewModel.gWrapper);
        }

        private void btnAccept_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var btn = (Button)sender;

            ViewModel.AcceptContract((Contract)btn.DataContext);
        }
    }
}
