using BravoOne.lib;

using BravoOne.UWP.ViewModels.Base;
using System;

namespace BravoOne.UWP.ViewModels
{
    public class GameViewModel : BaseViewModel
    {
        public GameViewModel(GameWrapper gWrapper) : base(gWrapper)
        {
        }

        public void EndMonth()
        {
            gWrapper.CurrentGame.EndTurn();
        }
    }
}