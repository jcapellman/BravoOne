using BravoOne.lib;
using BravoOne.UWP.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BravoOne.UWP
{
    public sealed partial class Options : Page
    {
        public Options()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataContext = new OptionsViewModel((GameWrapper)e.Parameter);

            base.OnNavigatedTo(e);
        }

        private void btnBack_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}