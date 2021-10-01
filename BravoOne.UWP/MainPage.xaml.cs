using BravoOne.lib.Objects;
using BravoOne.UWP.ViewModels;
using BravoOne.UWP.Views;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BravoOne.UWP
{
    public sealed partial class MainPage : Page
    {
        public GameViewModel ViewModel => (GameViewModel)DataContext;

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null && e.Parameter is Game game)
            {
                ViewModel.UpdateGame(game);
            }

            base.OnNavigatedTo(e);
        }

        private void btnEndMonth_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.EndMonth();
        }

        private void btnRecruitment_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _ = Frame.Navigate(typeof(Recruitment), ViewModel.CurrentGame);
        }

        private void btnManageTeamMember_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _ = Frame.Navigate(typeof(ManageTeamMember), ViewModel.CurrentGame);
        }
    }
}