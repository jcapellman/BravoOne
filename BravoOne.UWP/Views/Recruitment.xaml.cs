using BravoOne.lib;

using BravoOne.UWP.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BravoOne.UWP.Views
{
    public sealed partial class Recruitment : Page
    {
        private RecruitmentViewModel ViewModel => (RecruitmentViewModel)DataContext;

        public Recruitment()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataContext = new RecruitmentViewModel((GameWrapper)e.Parameter);
            
            base.OnNavigatedTo(e);
        }

        private void btnBack_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), ViewModel.gWrapper);
        }
    }
}