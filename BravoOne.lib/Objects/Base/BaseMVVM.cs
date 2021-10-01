using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BravoOne.lib.Objects.Base
{
    public class BaseMVVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}