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
            if (currentGame.Contracts != null)
            {
                return currentGame;
            }

            currentGame.Contracts = new System.Collections.ObjectModel.ObservableCollection<Contract>();

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