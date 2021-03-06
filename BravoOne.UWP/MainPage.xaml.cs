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

        private async void btnEndMonth_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var result = await ViewModel.EndMonthAsync();

            if (result)
            {
                return;
            }

            _ = Frame.Navigate(typeof(EndGame), ViewModel.gWrapper);
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

        private async void btnExitGame_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ContentDialog confirmExit = new ContentDialog
            {
                Title = "Exit game?",
                Content = "Are you sure you want to exit?",
                PrimaryButtonText = "Yes",
                CloseButtonText = "No"
            };

            ContentDialogResult result = await confirmExit.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                _ = Frame.Navigate(typeof(MainMenu), ViewModel.gWrapper);
            }
        }

        private void btnEquipment_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _ = Frame.Navigate(typeof(Equipment), ViewModel.gWrapper);
        }

        private void btnContracts_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _ = Frame.Navigate(typeof(Contracts), ViewModel.gWrapper);
        }
    }
}