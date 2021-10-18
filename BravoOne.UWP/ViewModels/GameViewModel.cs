using BravoOne.lib;

using BravoOne.UWP.ViewModels.Base;

namespace BravoOne.UWP.ViewModels
{
    public class GameViewModel : BaseViewModel
    {
        public GameViewModel(GameWrapper gWrapper) : base(gWrapper)
        {
        }

        public void SaveGame()
        {
            gWrapper.DAL.Add(gWrapper.CurrentGame);
        }

        public void EndMonth()
        {
            gWrapper.CurrentGame.EndTurn();
        }
    }
}