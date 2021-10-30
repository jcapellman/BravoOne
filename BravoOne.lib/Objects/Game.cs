﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using BravoOne.lib.DAL.Base;
using BravoOne.lib.Objects.Base;

namespace BravoOne.lib.Objects
{
    public class Game : BaseMVVM
    {
        public int Id { get; set; }

        private string _teamLeaderName;

        public string TeamLeaderName {
            get => _teamLeaderName;

            set
            {
                _teamLeaderName = value;

                OnPropertyChanged();
            }
        }

        private string _teamLogo;

        public string TeamLogo
        {
            get => _teamLogo;

            set
            {
                _teamLogo = value;

                OnPropertyChanged();
            }
        }

        private List<TeamMember> _teamMembers { get; set; }

        public List<TeamMember> TeamMembers
        {
            get => _teamMembers;

            set
            {
                _teamMembers = value;

                OnPropertyChanged();
            }
        }

        private ObservableCollection<Contract> _contracts { get; set; }

        public ObservableCollection<Contract> Contracts
        {
            get => _contracts;

            set
            {
                _contracts = value;

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

        public static Game LoadGame(BaseDAL dal, int id)
        {
            return dal.Get<Game>(a => a.Id == id);
        }

        private void PopulateRecruitment()
        {
            var randomFirst = new Random((int)DateTime.Now.Ticks);
            var randomLast = new Random((int)DateTime.Now.Ticks+1);
            var randomSkill = new Random((int)DateTime.Now.Ticks);

            for (var x = 0; x < 50; x++)
            {
                var member = new TeamMember
                {
                    OnTeam = false,
                    Health = 100,
                    Status = 100,
                    Id = Guid.NewGuid()
                };

                do {
                    var firstName = Common.Constants.FIRST_NAMES[randomFirst.Next(0, Common.Constants.FIRST_NAMES.Length - 1)];
                    var lastName = Common.Constants.LAST_NAMES[randomLast.Next(0, Common.Constants.LAST_NAMES.Length - 1)];

                    member.Name = $"{firstName} {lastName}";
                } while (TeamMembers.Any(a => a.Name == member.Name));

                member.SkillPoints = (uint)randomSkill.Next(1, 50);

                member.MonthlySalary = 10000 * member.SkillPoints;

                AddTeamMember(member);
            }
        }
        public Game()
        {
            CurrentDate = DateTime.Now;

            Contracts = new ObservableCollection<Contract>();
            TeamMembers = new List<TeamMember>();

            Money = 100000;

            PopulateRecruitment();

            AddContract(new Contract
            {
                AssignedTeamMembers = TeamMembers.Select(a => a.Id).ToList(),
                CompleteDate = DateTime.Now.AddMonths(4),
                Income = 5000,
                Id = Guid.NewGuid(),
                Name = "Operation Laal Kekra",
                SkillPointsRemaining = 10,
                TeamMemberToll = 4,
                Penalty = 50000,
                CompletedDateString = $"{DateTime.Now.AddMonths(4):MMMM} {DateTime.Now.AddMonths(4).Year}",
                Status = Enums.ContractStatus.InProgress
            });
        }

        public void AddTeamMember(TeamMember member)
        {
            TeamMembers.Add(member);
        }

        public void AddContract(Contract contract)
        {
            Contracts.Add(contract);
        }

        public void EndTurn()
        {
            CurrentDate = CurrentDate.AddMonths(1);

            foreach (TeamMember member in TeamMembers)
            {
                Money -= member.MonthlySalary;
            }

            for (int x = 0; x < Contracts.Count; x++)
            {
                TeamMembers = Contracts[x].EndTurn(CurrentDate, TeamMembers.ToDictionary(a => a.Id));

                switch (Contracts[x].Status)
                {
                    case Enums.ContractStatus.Completed:
                        Money += Contracts[x].Income;
                        break;
                    case Enums.ContractStatus.Failed:
                        Money -= Contracts[x].Penalty;
                        break;
                    case Enums.ContractStatus.NotStarted:
                        break;
                    case Enums.ContractStatus.InProgress:
                        break;
                    default:
                        break;
                }
            }

            Contracts = new ObservableCollection<Contract>(Contracts);
        }
    }
}