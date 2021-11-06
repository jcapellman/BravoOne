using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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

        public async void InitializeTeamMembers(IStorage storage)
        {
            TeamMembers = TeamMembers.Where(a => a.Status != TeamMemberStatus.Available).ToList();

            var randomFirst = new Random((int)DateTime.Now.Ticks);
            var randomLast = new Random((int)DateTime.Now.Ticks+1);
            var randomSkill = new Random((int)DateTime.Now.Ticks);
            var randomSpecialty = new Random((int)DateTime.Now.Ticks);

            var randomAvatar = new Random((int)DateTime.Now.Ticks+5);

            var specialties = Enum.GetNames(typeof(Specialties));
            var avatarImages = await storage.GetAvatarImagesAsync();

            for (var x = 0; x < 50; x++)
            {
                var member = new TeamMember
                {
                    Health = 100,
                    Status = TeamMemberStatus.Available,
                    Id = Guid.NewGuid()
                };

                do {
                    var firstName = Common.Constants.FIRST_NAMES[randomFirst.Next(0, Common.Constants.FIRST_NAMES.Length - 1)];
                    var lastName = Common.Constants.LAST_NAMES[randomLast.Next(0, Common.Constants.LAST_NAMES.Length - 1)];

                    member.Name = $"{firstName} {lastName}";
                } while (TeamMembers.Any(a => a.Name == member.Name));
                
                member.SkillPoints = (uint)randomSkill.Next(1, TeamLevel + 5);

                member.MonthlySalary = 10000 * member.SkillPoints;
                
                member.Specialty = specialties[randomSpecialty.Next(0, specialties.Length - 1)];
                member.AvatarImagePath = avatarImages[randomAvatar.Next(0, avatarImages.Count() - 1)];

                TeamMembers.Add(member);
            }
        }

        public Game()
        {
            CurrentDate = DateTime.Now;

            Contracts = new ObservableCollection<Contract>();
            TeamMembers = new List<TeamMember>();
            TeamEquipment = new List<Equipment>();

            TeamLevel = 1;

            gsMonths = 0;
            gsContracts = 0;
            gsXP = 0;

            Money = 100000;
        }

        public async void InitializeEquipment(IStorage storage)
        {
            AvailableEquipment = (await storage.GetEquipmentListAsync()).OrderByDescending(a => a.Cost).ThenBy(b => b.Name).ToList();
        }

        public void AddTeamMember(TeamMember member)
        {
            member.Status = TeamMemberStatus.OnTeam;

            var index = TeamMembers.FindIndex(a => a.Id == member.Id);

            TeamMembers[index] = member;
        }

        public void AddContract(Contract contract)
        {
            Contracts.Add(contract);
        }

        public bool EndTurn(IStorage storage)
        {
            CurrentDate = CurrentDate.AddMonths(1);

            foreach (TeamMember member in TeamMembers.Where(a => a.Status == TeamMemberStatus.OnTeam))
            {
                if (member.MonthlySalary > Money)
                {
                    return false;
                }

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

            gsMonths++;

            InitializeTeamMembers(storage);

            return true;
        }
    }
}