using BravoOne.lib.Objects;
using BravoOne.lib.Objects.Base;

namespace BravoOne.UWP.ViewModels.Base
{
    public class BaseViewModel : BaseMVVM
    {
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

        public void UpdateGame(Game game)
        {
            CurrentGame = game;
        }
    }
}