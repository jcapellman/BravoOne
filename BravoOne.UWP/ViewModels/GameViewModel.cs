using BravoOne.lib.Objects;
using BravoOne.UWP.ViewModels.Base;

namespace BravoOne.UWP.ViewModels
{
    public class GameViewModel : BaseViewModel
    {
        private string _currentDateString;

        public string CurrentDateString
        {
            get => _currentDateString;

            set
            {
                _currentDateString = value;
                OnPropertyChanged();
            }
        }

        private Game _game;

        public Game CurrentGame
        {
            get => _game;

            set
            {
                _game = value;

                OnPropertyChanged();
            }
        }

        public GameViewModel()
        {
            CurrentGame = new Game();

            UpdateDate();
        }

        private void UpdateDate()
        {
            CurrentDateString = $"{CurrentGame.CurrentDate:MMMM} {CurrentGame.CurrentDate.Year}";
        }

        public void EndMonth()
        {
            CurrentGame.EndTurn();

            UpdateDate();
        }
    }
}