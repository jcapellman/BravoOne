using BravoOne.lib.Objects;
using BravoOne.UWP.ViewModels.Base;

namespace BravoOne.UWP.ViewModels
{
    public class GameViewModel : BaseViewModel
    {
        public GameViewModel()
        {
            CurrentGame = new Game();
        }

        public void EndMonth()
        {
            CurrentGame.EndTurn();
        }
    }
}