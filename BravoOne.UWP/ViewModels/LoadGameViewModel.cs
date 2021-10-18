using System.Collections.Generic;

using BravoOne.lib;
using BravoOne.lib.Objects;
using BravoOne.UWP.ViewModels.Base;

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

        private void Validate()
        {
            Enabled = SelectedGame != null;
        }

        public LoadGameViewModel(GameWrapper wrapper) : base(wrapper)
        {
            Games = gWrapper.DAL.GetAll<Game>(a => a != null);

            SelectedGame = null;
        }

        public void LoadGame()
        {
            gWrapper.CurrentGame = SelectedGame;
        }
    }
}