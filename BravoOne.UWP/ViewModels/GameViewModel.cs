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

        private Game game;

        public GameViewModel()
        {
            game = new Game();

            UpdateDate();
        }

        private void UpdateDate()
        {
            CurrentDateString = $"{game.CurrentDate:MMMM} {game.CurrentDate.Year}";
        }

        public void EndMonth()
        {
            game.EndTurn();

            UpdateDate();
        }
    }
}