using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using BravoOne.lib.Enums;

namespace BravoOne.lib.Objects
{
    public class Contract : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public Guid Id { get; set; }

        private ContractType _ctype;
        public ContractType CType
        {
            get => _ctype;
            set { _ctype = value; OnPropertyChanged(); }
        }

        private ulong _income;
        public ulong Income
        {
            get => _income;
            set { _income = value; OnPropertyChanged(); }
        }

        private ulong _penalty;
        public ulong Penalty
        {
            get => _penalty;
            set { _penalty = value; OnPropertyChanged(); }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        private ContractStatus _status;
        public ContractStatus Status
        {
            get => _status;
            set { _status = value; OnPropertyChanged(); }
        }

        private string _completedDateString;
        public string CompletedDateString
        {
            get => _completedDateString;
            set { _completedDateString = value; OnPropertyChanged(); }
        }

        private DateTime _completeDate;
        public DateTime CompleteDate
        {
            get => _completeDate;
            set { _completeDate = value; OnPropertyChanged(); }
        }

        private int _teamMemberToll;
        public int TeamMemberToll
        {
            get => _teamMemberToll;
            set { _teamMemberToll = value; OnPropertyChanged(); }
        }

        private uint _skillPointsRemaining;
        public uint SkillPointsRemaining
        {
            get => _skillPointsRemaining;
            set
            {
                _skillPointsRemaining = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SkillPointsCompleted));
            }
        }

        // Set once at creation; used for XP calculation so harder contracts reward more.
        private uint _skillPointsTotal;
        public uint SkillPointsTotal
        {
            get => _skillPointsTotal;
            set { _skillPointsTotal = value; OnPropertyChanged(); OnPropertyChanged(nameof(SkillPointsCompleted)); }
        }

        // Completed work = total minus what remains — used for progress bars.
        public uint SkillPointsCompleted => SkillPointsTotal >= SkillPointsRemaining
            ? SkillPointsTotal - SkillPointsRemaining
            : 0;

        // Flavor text shown to the player when browsing available contracts.
        public string Briefing { get; set; }

        // Set at generation; used when the contract is accepted to calculate CompleteDate.
        public int DeadlineMonths { get; set; }

        public Dictionary<Specialties, int> SpecialtiesRequired { get; set; }

        public List<Guid> AssignedTeamMembers { get; set; }

        public int TurnsRemaining(DateTime currentDate)
        {
            if (CompleteDate <= currentDate)
            {
                return 0;
            }

            return (CompleteDate.Year - currentDate.Year) * 12 + CompleteDate.Month - currentDate.Month;
        }
    }
}