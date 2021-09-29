using BravoOne.lib.Objects;

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

            DataContext = new RecruitmentViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.CurrentGame = (Game)e.Parameter;

            base.OnNavigatedTo(e);
        }
    }
}