using System;
using System.Collections.Generic;

using BravoOne.lib.Objects.Base;

namespace BravoOne.lib.Objects
{
    public class Game : BaseMVVM
    {
        public List<TeamMember> _teamMembers { get; set; }

        public List<TeamMember> TeamMembers
        {
            get => _teamMembers;

            set
            {
                _teamMembers = value;

                OnPropertyChanged();
            }
        }

        private DateTime _currentDate { get; set; }

        public DateTime CurrentDate
        {
            get => _currentDate;

            set
            {
                _currentDate = value;

                CurrentDateString = $"{CurrentDate:MMMM} {CurrentDate.Year}";

                OnPropertyChanged();
            }
        }

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

        public ulong Money { get; set; }

        public Game()
        {
            CurrentDate = DateTime.Now;

            TeamMembers = new List<TeamMember>();

            Money = 10000000;

            AddTeamMember(new TeamMember
            {
                Health = 100,
                MonthlySalary = 5000,
                Name = "One",
                StartDate = DateTime.Now,
                Status = 60
            });

            AddTeamMember(new TeamMember
            {
                Health = 100,
                MonthlySalary = 1000,
                Name = "Two",
                StartDate = DateTime.Now,
                Status = 100
            });
        }

        public void AddTeamMember(TeamMember member)
        {
            TeamMembers.Add(member);
        }

        public void EndTurn()
        {
            CurrentDate = CurrentDate.AddMonths(1);

            foreach (TeamMember member in TeamMembers)
            {
                Money -= member.MonthlySalary;
            }
        }
    }
}