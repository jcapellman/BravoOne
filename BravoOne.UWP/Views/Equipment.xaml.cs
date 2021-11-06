using BravoOne.lib;
using BravoOne.UWP.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BravoOne.UWP.Views
{
    public sealed partial class Equipment : Page
    {
        private EquipmentViewModel ViewModel => (EquipmentViewModel)DataContext;

        public Equipment()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataContext = new EquipmentViewModel((GameWrapper)e.Parameter);

            base.OnNavigatedTo(e);
        }

        private void btnBack_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), ViewModel.gWrapper);
        }

        private void btnPurchase_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var btn = (Button)sender;

            ViewModel.AddEquipment((lib.Objects.Equipment)btn.DataContext);
        }
    }
}