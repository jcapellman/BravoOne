using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BravoOne.lib.DAL.Base;
using BravoOne.lib.Enums;
using BravoOne.lib.Objects.Base;
using BravoOne.lib.PlatformAbstractions;

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

        public static Game LoadGame(BaseDAL dal, int id)
        {
            return dal.Get<Game>(a => a.Id == id);
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

            var index = TeamMembers.FindIndex(a => a.Id == member.Id);

            TeamMembers[index] = member;

            AddActivityLog(ActivityType.TEAM_MEMBER_HIRED, "New Team Member Hired", $"{member.Specialty} {member.Name} has been hired");
        }

        public void AddContract(Contract contract)
        {
            Contracts.Add(contract);
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