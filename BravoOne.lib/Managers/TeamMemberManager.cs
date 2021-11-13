using BravoOne.lib.Enums;
using BravoOne.lib.Objects;
using BravoOne.lib.PlatformAbstractions;
using BravoOne.lib.DAL.Base;
using BravoOne.lib.Managers.Base;

using System.Linq;

namespace BravoOne.lib.Managers
{
    public class TeamMemberManager : BaseManager
    {
        public TeamMemberManager(IStorage storage, BaseDAL dal) : base(storage, dal)
        {
        }

        public override (TurnStatus Status, Game CurrentGame) ProcessTurn(Game currentGame)
        {
            foreach (TeamMember member in currentGame.TeamMembers.Where(a => a.Status == TeamMemberStatus.OnTeam))
            {
                if (member.MonthlySalary > currentGame.Money)
                {
                    return (TurnStatus.OUT_OF_MONEY, currentGame);
                }

                currentGame.Money -= member.MonthlySalary;
            }

            return (TurnStatus.OK, currentGame);
        }
    }
}