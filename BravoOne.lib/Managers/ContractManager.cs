using BravoOne.lib.DAL.Base;
using BravoOne.lib.Enums;
using BravoOne.lib.Managers.Base;
using BravoOne.lib.Objects;
using BravoOne.lib.PlatformAbstractions;

using System.Linq;

namespace BravoOne.lib.Managers
{
    public class ContractManager : BaseManager
    {
        public ContractManager(IStorage storage, BaseDAL dal) : base(storage, dal)
        {
        }

        public override (TurnStatus Status, Game CurrentGame) ProcessTurn(Game currentGame)
        {
            for (int x = 0; x < currentGame.Contracts.Count; x++)
            {
                currentGame.TeamMembers = currentGame.Contracts[x].EndTurn(currentGame.CurrentDate, currentGame.TeamMembers.ToDictionary(a => a.Id));

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