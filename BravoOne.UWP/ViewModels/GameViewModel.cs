using System;

using BravoOne.UWP.ViewModels.Base;

namespace BravoOne.UWP.ViewModels
{
    public class GameViewModel : BaseViewModel
    {
        private string _currentDateString;

        public string CurrentDateString
        {
            get => _currentDateString;

            set
            {
                _currentDateString = value;
                OnPropertyChanged();
            }
        }

        private DateTime _currentDate;

        public DateTime CurrentDate
        {
            get => _currentDate;

            set
            {
                _currentDate = value;

                CurrentDateString = $"{_currentDate:MMMM} {_currentDate.Year}";
                OnPropertyChanged();
            }
        }
        public GameViewModel()
        {
            CurrentDate = DateTime.Now;
        }

        public void EndMonth()
        {
            CurrentDate = CurrentDate.AddMonths(1);
        }
    }
}