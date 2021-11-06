using BravoOne.lib;
using BravoOne.lib.Objects;
using BravoOne.UWP.ViewModels.Base;

namespace BravoOne.UWP.ViewModels
{
    public class ContractsViewModel : BaseViewModel
    {
        public ContractsViewModel(GameWrapper wrapper) : base(wrapper)
        {
        }

        public void AcceptContract(Contract contract)
        {
            gWrapper.CurrentGame.AddContract(contract);
        }
    }
}