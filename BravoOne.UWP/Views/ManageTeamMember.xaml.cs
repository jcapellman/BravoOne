using BravoOne.lib;
using BravoOne.UWP.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BravoOne.UWP.Views
{
    public sealed partial class ManageTeamMember : Page
    {
        private ManageTeamMemberViewModel ViewModel => (ManageTeamMemberViewModel)DataContext;

        public ManageTeamMember()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataContext = new ManageTeamMemberViewModel((GameWrapper)e.Parameter);
            base.OnNavigatedTo(e);
        }

        private void btnBack_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), ViewModel.gWrapper);
        }

        private void btnToggleAssignment_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var item = (MemberAssignmentItem)btn.DataContext;
            ViewModel.ToggleAssignment(item.Contract, item.Member);
        }

        private void btnAssignEquipment_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var item = (PoolEquipmentItem)btn.DataContext;
            ViewModel.AssignEquipment(item.Member, item.Equipment);
        }

        private void btnUnassignEquipment_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var item = (AssignedEquipmentSlot)btn.DataContext;
            ViewModel.UnassignEquipment(item.Member, item.Slot);
        }
    }
}
