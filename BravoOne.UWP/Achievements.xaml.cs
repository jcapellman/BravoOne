using BravoOne.lib;
using BravoOne.UWP.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BravoOne.UWP
{
    public sealed partial class Achievements : Page
    {
        private AchievementsViewModel ViewModel => (AchievementsViewModel)DataContext;

        public Achievements()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataContext = new AchievementsViewModel((GameWrapper)e.Parameter);

            ViewModel.LoadListing();

            base.OnNavigatedTo(e);
        }

        private void btnBack_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}