using System;

using BravoOne.lib;
using BravoOne.UWP.ViewModels;
using BravoOne.UWP.Views;

using Windows.UI.Popups;
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
            DataContext = new GameViewModel((GameWrapper)e.Parameter);

            base.OnNavigatedTo(e);
        }

        private void btnEndMonth_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.EndMonth();
        }

        private void btnRecruitment_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _ = Frame.Navigate(typeof(Recruitment), ViewModel.gWrapper);
        }

        private void btnManageTeamMember_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _ = Frame.Navigate(typeof(ManageTeamMember), ViewModel.gWrapper);
        }

        private async void btnSaveGame_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.SaveGame();

            var dialog = new MessageDialog("Saved successfully");

            await dialog.ShowAsync();
        }
    }
}