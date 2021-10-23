using System.Collections.Generic;
using System.Linq;

using BravoOne.lib;
using BravoOne.lib.Objects;
using BravoOne.UWP.ViewModels.Base;
using Windows.UI.Xaml;

namespace BravoOne.UWP.ViewModels
{
    public class LoadGameViewModel : BaseViewModel
    {
        private List<Game> _games;

        public List<Game> Games
        {
            get => _games;

            set
            {
                _games = value;

                OnPropertyChanged();
            }
        }

        private Game _selectedGame;

        public Game SelectedGame
        {
            get => _selectedGame;

            set
            {
                _selectedGame = value;

                OnPropertyChanged();

                Validate();
            }
        }

        private bool _enabled;

        public bool Enabled
        {
            get => _enabled;

            set
            {
                _enabled = value;
                OnPropertyChanged();
            }
        }

        private Visibility _gamesListingVisibility;

        public Visibility GamesListingVisibility
        {
            get => _gamesListingVisibility;

            set
            {
                _gamesListingVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _noGamesListingVisibility;

        public Visibility NoGamesListingVisibility
        {
            get => _noGamesListingVisibility;

            set
            {
                _noGamesListingVisibility = value;
                OnPropertyChanged();
            }
        }

        private void Validate()
        {
            Enabled = SelectedGame != null;

            GamesListingVisibility = Games.Any() ? Visibility.Visible : Visibility.Collapsed;

            NoGamesListingVisibility = !Games.Any() ? Visibility.Visible : Visibility.Collapsed;
        }

        public LoadGameViewModel(GameWrapper wrapper) : base(wrapper)
        {
            Games = gWrapper.DAL.GetAll<Game>(a => a != null);

            SelectedGame = Games.FirstOrDefault();
        }

        public void LoadGame()
        {
            gWrapper.CurrentGame = SelectedGame;
        }
    }
}