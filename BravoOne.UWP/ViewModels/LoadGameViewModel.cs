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
            }
        }

        protected LoadGameViewModel(GameWrapper wrapper) : base(wrapper)
        {
            Games = gWrapper.DAL.GetAll<Game>(a => a != null);
        }

        public void LoadGame()
        {
            gWrapper.CurrentGame = SelectedGame;
        }
    }
}