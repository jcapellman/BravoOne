using BravoOne.lib.DAL.Base;
using BravoOne.UWP.ViewModels;

using Windows.UI.Xaml.Controls;

namespace BravoOne.UWP
{
    public sealed partial class MainMenu : Page
    {
        private MainMenuViewModel viewModel => (MainMenuViewModel)DataContext;

        public MainMenu(BaseDAL dal)
        {
            InitializeComponent();

            DataContext = new MainMenuViewModel(dal);
        }
    }
}