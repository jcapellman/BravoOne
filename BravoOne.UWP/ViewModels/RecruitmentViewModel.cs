using BravoOne.lib.Objects;

using BravoOne.UWP.ViewModels.Base;

namespace BravoOne.UWP.ViewModels
{
    public class RecruitmentViewModel : BaseViewModel
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
    }
}