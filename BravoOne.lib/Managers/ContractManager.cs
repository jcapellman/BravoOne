using BravoOne.lib.DAL.Base;
using BravoOne.lib.Enums;
using BravoOne.lib.Managers.Base;
using BravoOne.lib.Objects;
using BravoOne.lib.PlatformAbstractions;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace BravoOne.lib.Managers
{
    public class ContractManager : BaseManager
    {
        public ContractManager(IStorage storage, BaseDAL dal) : base(storage, dal)
        {
        }

        public override async Task<Game> InitializeAsync(Game currentGame)
        {
            currentGame.AvailableContracts = new System.Collections.ObjectModel.ObservableCollection<Contract>();

            var randomName = new Random((int)DateTime.Now.Ticks);
            var randomPrefix = new Random((int)DateTime.Now.Ticks+1);
            var randomSkillLevel = new Random((int)DateTime.Now.Ticks + 1);
            var randomType = new Random((int)DateTime.Now.Ticks + 1);
            var randomSkillPoints = new Random((int)DateTime.Now.Ticks + 1);
            var randomIncome = new Random((int)DateTime.Now.Ticks + 1);
            var randomPenalty = new Random((int)DateTime.Now.Ticks + 1);
            var randomToll = new Random((int)DateTime.Now.Ticks + 1);
            .
            for (var x = 0; x < 10; x++)
            {
                var contract = new Contract
                {
                    Id = Guid.NewGuid(),
                    Status = ContractStatus.NotStarted,
                    SpecialtiesRequired = new System.Collections.Generic.Dictionary<Specialties, int>(),
                    AssignedTeamMembers = new System.Collections.Generic.List<Guid>()
                };

                do
                {
                    var name = Common.Constants.MISSION_NAMES[randomName.Next(0, Common.Constants.MISSION_NAMES.Length - 1)];
                    var prefix = Common.Constants.MISSION_PREFIX[randomPrefix.Next(0, Common.Constants.MISSION_PREFIX.Length - 1)];

                    contract.Name = $"{prefix} {name}";
                } while (currentGame.AvailableContracts.Any(a => a.Name == contract.Name) || 
                    currentGame.Contracts.Any(a => a.Name == contract.Name));

                contract.CType = (ContractType)randomType.Next(0, Enum.GetValues(typeof(ContractType)).Length - 1);
                contract.SkillPointsRemaining = (uint)randomSkillPoints.Next(currentGame.TeamLevel, currentGame.TeamLevel * currentGame.TeamMembers.Count);
                contract.Income = (ulong)randomIncome.Next((int)(contract.SkillPointsRemaining * 10), (int)(contract.SkillPointsRemaining * 25));
                contract.TeamMemberToll = randomToll.Next(5, 20);
                contract.Penalty = (ulong)randomPenalty.Next((int)contract.Income / 2, (int)(contract.Income * 2));

                switch (contract.CType)
                {
                    case ContractType.DEMOLITION:
                        contract.SpecialtiesRequired.Add(Specialties.DEMOLITION, 1);
                        contract.SpecialtiesRequired.Add(Specialties.RECON, 1);
                        break;
                    case ContractType.INFILTRATION:
                        contract.SpecialtiesRequired.Add(Specialties.ASSAULT, 1);
                        contract.SpecialtiesRequired.Add(Specialties.DEMOLITION, 1);
                        contract.SpecialtiesRequired.Add(Specialties.RECON, 1);
                        contract.SpecialtiesRequired.Add(Specialties.SNIPER, 1);
                        break;
                    case ContractType.RESCUE:
                        contract.SpecialtiesRequired.Add(Specialties.ASSAULT, 1);
                        contract.SpecialtiesRequired.Add(Specialties.SNIPER, 1);
                        contract.SpecialtiesRequired.Add(Specialties.RECON, 1);
                        break;
                    case ContractType.RECON:
                        contract.SpecialtiesRequired.Add(Specialties.RECON, 1);
                        contract.SpecialtiesRequired.Add(Specialties.SNIPER, 1);
                        break;
                }

                currentGame.AvailableContracts.Add(contract);
            }

            return currentGame;
        }

        public override async Task<(TurnStatus Status, Game CurrentGame)> ProcessTurnAsync(Game currentGame)
        {
            for (int x = 0; x < currentGame.Contracts.Count; x++)
            {
                if (currentGame.Contracts[x].Status != ContractStatus.InProgress)
                {
                    continue;
                }

                foreach (Guid guid in currentGame.Contracts[x].AssignedTeamMembers)
                {
                    var teamMember = currentGame.TeamMembers.FirstOrDefault(a => a.Id == guid);

                    currentGame.Contracts[x].SkillPointsRemaining -= teamMember.SkillPoints;
                }

                if (currentGame.Contracts[x].SkillPointsRemaining <= 0)
                {
                    currentGame.Contracts[x].Status = ContractStatus.Completed;
                }
                else if (currentGame.CurrentDate > currentGame.Contracts[x].CompleteDate)
                {
                    currentGame.Contracts[x].Status = ContractStatus.Failed;
                }

                switch (currentGame.Contracts[x].Status)
                {
                    case Enums.ContractStatus.Completed:
                        currentGame.Money += currentGame.Contracts[x].Income;
                        break;
                    case Enums.ContractStatus.Failed:
                        currentGame.Money -= currentGame.Contracts[x].Penalty;
                        break;
                    case Enums.ContractStatus.NotStarted:
                        break;
                    case Enums.ContractStatus.InProgress:
                        break;
                    default:
                        break;
                }
            }

            return (TurnStatus.OK, currentGame);
        }
    }
}