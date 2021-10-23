using BravoOne.lib;
using BravoOne.UWP.ViewModels.Base;

namespace BravoOne.UWP.ViewModels
{
    public class OptionsViewModel : BaseViewModel
    {
        public lib.Objects.Options Option
        {
            get => gWrapper.Option;

            set
            {
                gWrapper.Option = value;

                OnPropertyChanged();
            }
        }

        public OptionsViewModel(GameWrapper wrapper) : base(wrapper)
        {
            Option = gWrapper.Option;
        }
    }
}