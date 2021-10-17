using BravoOne.lib;
using BravoOne.UWP.ViewModels.Base;

namespace BravoOne.UWP.ViewModels
{
    public class NewGameViewModel : BaseViewModel
    {
        private string _name;

        public string Name
        {
            get => _name;

            set
            {
                _name = value;
                OnPropertyChanged();

                Validate();
            }
        }

        private bool _enabled;

        public bool Enabled
        {
            get=> _enabled;

            set
            {
                _enabled = value;
                OnPropertyChanged();
            }
        }

        private void Validate()
        {
            Enabled = !string.IsNullOrEmpty(Name);
        }

        public NewGameViewModel(GameWrapper gWrapper) : base(gWrapper)
        {
            Name = string.Empty;
        }
    }
}