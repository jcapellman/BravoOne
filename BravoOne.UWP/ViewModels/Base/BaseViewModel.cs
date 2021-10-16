using BravoOne.lib;
using BravoOne.lib.Objects;
using BravoOne.lib.Objects.Base;

namespace BravoOne.UWP.ViewModels.Base
{
    public class BaseViewModel : BaseMVVM
    {
        private GameWrapper _wrapper;

        public GameWrapper gWrapper
        {
            get => _wrapper;

            set
            {
                _wrapper = value;

                OnPropertyChanged();
            }
        }

        protected BaseViewModel(GameWrapper wrapper)
        {
            gWrapper = wrapper;
        }

        public void UpdateGame(Game game)
        {
            gWrapper.CurrentGame = game;
        }
    }
}