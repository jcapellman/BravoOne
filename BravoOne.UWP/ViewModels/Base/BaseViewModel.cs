using BravoOne.lib.Objects;

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BravoOne.UWP.ViewModels.Base
{
    public class BaseViewModel : INotifyPropertyChanged
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

        public void UpdateGame(Game game)
        {
            CurrentGame = game;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}