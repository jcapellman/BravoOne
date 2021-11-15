using BravoOne.lib;
using BravoOne.UWP.ViewModels;

using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BravoOne.UWP
{
    public sealed partial class LoadGame : Page
    {
        public LoadGameViewModel ViewModel => (LoadGameViewModel)DataContext;

        public LoadGame()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataContext = new LoadGameViewModel((GameWrapper)e.Parameter);

            base.OnNavigatedTo(e);
        }

        private void btnBack_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void btnLoadGame_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.LoadGame();

            _ = Frame.Navigate(typeof(MainPage), ViewModel.gWrapper);
        }

        private async void btnDeleteGame_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ContentDialog confirmDelete = new ContentDialog
            {
                Title = "Delete game?",
                Content = "Are you sure you want to delete the selected game?",
                PrimaryButtonText = "Yes",
                CloseButtonText = "No"
            };

            ContentDialogResult result = await confirmDelete.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                ViewModel.DeleteGame();
            }
        }
    }
}