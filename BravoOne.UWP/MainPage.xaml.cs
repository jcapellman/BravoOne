using BravoOne.UWP.ViewModels;

using Windows.UI.Xaml.Controls;

namespace BravoOne.UWP
{
    public sealed partial class MainPage : Page
    {
        public GameViewModel ViewModel => (GameViewModel)DataContext;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void btnEndMonth_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.EndMonth();
        }
    }
}