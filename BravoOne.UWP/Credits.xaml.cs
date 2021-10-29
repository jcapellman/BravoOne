using System;

using BravoOne.lib;
using BravoOne.UWP.ViewModels;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BravoOne.UWP
{
    public sealed partial class Credits : Page
    {
        private DispatcherTimer scroller = new DispatcherTimer();
        private double margin = ApplicationView.GetForCurrentView().VisibleBounds.Height;

        public Credits()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataContext = new CreditsViewModel((GameWrapper)e.Parameter);

            scroller.Tick += Scroller_Tick;
            scroller.Interval = new TimeSpan(0, 0, 0, 0, 20);
            scroller.Start();

            gCredits.Margin = new Thickness(0, margin, 0, 0);

            base.OnNavigatedTo(e);
        }

        private void Scroller_Tick(object sender, object e)
        {
            margin -= 3;

            gCredits.Margin = new Thickness(0, margin, 0, 0);

            if (margin <= 30)
            {
                scroller.Stop();
            }
        }

        private void btnBack_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}