using BravoOne.lib.DAL.Base;
using BravoOne.lib.Objects;
using BravoOne.UWP.ViewModels.Base;

namespace BravoOne.UWP.ViewModels
{
    public class GameViewModel : BaseViewModel
    {
        public GameViewModel(BaseDAL dal) : base(dal)
        {
            CurrentGame = new Game();
        }

        public void EndMonth()
        {
            CurrentGame.EndTurn();
        }
    }
}