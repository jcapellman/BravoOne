using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using BravoOne.lib.Enums;
using BravoOne.lib.Objects.Base;

namespace BravoOne.lib.Objects
{
    public class Game : BaseMVVM
    {
        public int Id { get; set; }

        public int gsMonths { get; private set; }

        public int gsContracts { get; private set; }

        public int gsXP { get; private set; }

        private string _teamLeaderName;

        public string TeamLeaderName {
            get => _teamLeaderName;

            set
            {
                _teamLeaderName = value;

                OnPropertyChanged();
            }
        }

        private int _teamLevel;

        public int TeamLevel { 
            get => _teamLevel;
        
            set
            {
                _teamLevel = value;

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

        public void AddEquipment(Equipment equipment)
        {
            TeamEquipment.Add(equipment);
        }

        private List<Equipment> _availableEquipment;

        public List<Equipment> AvailableEquipment
        {
            get => _availableEquipment;

            set
            {
                _availableEquipment = value;

                OnPropertyChanged();
            }
        }

        private List<Equipment> _teamEquipment;

        public List<Equipment> TeamEquipment
        {
            get => _teamEquipment;

            set
            {
                _teamEquipment = value;

                OnPropertyChanged();
            }
        }

        internal Dictionary<string, string> ActivityTypeImages;

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

        private ObservableCollection<Contract> _availableContracts { get; set; }

        public ObservableCollection<Contract> AvailableContracts
        {
            get => _availableContracts;

            set
            {
                _availableContracts = value;

                OnPropertyChanged();
            }
        }

        private ObservableCollection<ActivityLog> _activities;

        public ObservableCollection<ActivityLog> Activities
        {
            get => _activities;

            set
            {
                _activities = value;

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

        private ulong _money;

        public ulong Money
        {
            get => _money;

            set
            {
                _money = value;

                OnPropertyChanged();
            }
        }

        public Game()
        {
            CurrentDate = DateTime.Now;

            Contracts = new ObservableCollection<Contract>();
            TeamMembers = new List<TeamMember>();
            TeamEquipment = new List<Equipment>();
            Activities = new ObservableCollection<ActivityLog>();

            TeamLevel = 1;

            gsMonths = 0;
            gsContracts = 0;
            gsXP = 0;

            Money = 100000;
        }

        public void AddTeamMember(TeamMember member)
        {
            member.Status = TeamMemberStatus.OnTeam;
            member.StartDate = CurrentDate;

            var index = TeamMembers.FindIndex(a => a.Id == member.Id);

            TeamMembers[index] = member;

            AddActivityLog(ActivityType.TEAM_MEMBER_HIRED, "New Team Member Hired", $"{member.Specialty} {member.Name} has been hired");
        }

        public void FireTeamMember(TeamMember member)
        {
            member.Status = TeamMemberStatus.Available;

            AddActivityLog(ActivityType.TEAM_MEMBER_FIRED, "Team Member Fired", $"{member.Name} has been let go");
        }

        public bool CanAcceptContract(Contract contract)
        {
            var onTeam = TeamMembers.Where(a => a.Status == TeamMemberStatus.OnTeam).ToList();

            foreach (var required in contract.SpecialtiesRequired)
            {
                var count = onTeam.Count(m => m.Specialty == required.Key);

                if (count < required.Value)
                {
                    return false;
                }
            }

            return true;
        }

        public void AddContract(Contract contract)
        {
            contract.Status = ContractStatus.InProgress;
            contract.CompleteDate = CurrentDate.AddMonths(Math.Max(1, contract.DeadlineMonths));

            AvailableContracts.Remove(contract);

            Contracts.Add(contract);

            AddActivityLog(ActivityType.CONTRACT_COMPLETED, "Contract Accepted", $"Contract {contract.Name} is now in progress");
        }

        public bool DeductMoney(ulong amount)
        {
            if (amount > Money)
            {
                Money = 0;
                return false;
            }

            Money -= amount;
            return true;
        }

        public void CompleteContract(Contract contract)
        {
            contract.Status = ContractStatus.Completed;
            Money += contract.Income;
            RecordContractCompleted((int)contract.SkillPointsTotal + 1);
            AddActivityLog(ActivityType.CONTRACT_COMPLETED, "Contract Completed", $"Contract {contract.Name} completed successfully");
            // Replace the contract in the collection to force UI collection change notification
            var idx = Contracts.IndexOf(contract);
            if (idx >= 0)
            {
                Contracts[idx] = contract;
            }
            else
            {
                OnPropertyChanged(nameof(Contracts));
            }
        }

        // Called by CompleteContract and directly in tests.
        public void RecordContractCompleted(int xpAwarded = 1)
        {
            gsContracts++;
            gsXP += xpAwarded;
            CheckTeamLevelUp();
        }

        public void FailContract(Contract contract)
        {
            contract.Status = ContractStatus.Failed;
            DeductMoney(contract.Penalty);
            AddActivityLog(ActivityType.CONTRACT_FAILED, "Contract Failed", $"Contract {contract.Name} has failed");
            var idx = Contracts.IndexOf(contract);
            if (idx >= 0)
            {
                Contracts[idx] = contract;
            }
            else
            {
                OnPropertyChanged(nameof(Contracts));
            }
        }

        public void ApplyHealthChanges(List<TeamMember> membersToCheck)
        {
            foreach (var member in membersToCheck)
            {
                if (member.Health <= 0)
                {
                    member.Status = TeamMemberStatus.Deceased;
                    AddActivityLog(ActivityType.TEAM_MEMBER_DIED, "Team Member KIA", $"{member.Name} has been killed in action");
                }
                else if (member.Health <= 20)
                {
                    member.Status = TeamMemberStatus.Injured;
                    AddActivityLog(ActivityType.TEAM_MEMBER_RETIRED, "Team Member Injured", $"{member.Name} is critically injured and cannot work");
                }

                if (member.Age >= member.RetirementAge && member.Status == TeamMemberStatus.OnTeam)
                {
                    member.Status = TeamMemberStatus.Retired;
                    AddActivityLog(ActivityType.TEAM_MEMBER_RETIRED, "Team Member Retired", $"{member.Name} has reached retirement age and left the team");
                }
            }
        }

        private void CheckTeamLevelUp()
        {
            var xpThreshold = TeamLevel * TeamLevel * 500;

            if (gsXP >= xpThreshold)
            {
                TeamLevel++;
                AddActivityLog(ActivityType.TEAM_LEVEL_UP, "Team Level Up", $"Your team has reached level {TeamLevel}");
            }
        }

        public void AddActivityLog(ActivityType type, string title, string detail)
        {
            var activity = new ActivityLog
            {
                TimeStamp = CurrentDate,
                ActivityLogType = type,
                Detail = detail,
                Title = title,
                IconImagePath = ActivityTypeImages[type.ToString()]
            };

            Activities.Add(activity);
        }

        public void EndTurn()
        {
            CurrentDate = CurrentDate.AddMonths(1);

            gsMonths++;
        }
    }
}