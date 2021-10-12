using BravoOne.lib.DAL.Base;
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

        protected BaseDAL baseDAL;

        protected BaseViewModel(BaseDAL dal)
        {
            baseDAL = dal;
        }

        public void UpdateGame(Game game)
        {
            CurrentGame = game;
        }
    }
}