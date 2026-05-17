using BravoOne.lib.Achievements.Base;
using BravoOne.lib.Objects;

using System.Linq;

namespace BravoOne.lib.Achievements
{
    public class HireTeamMember : BaseAchievement
    {
        public override string Title => "Mercenary for Hire";

        public override string Description => "Hired a team member";

        public override bool VerifyAchievement(Game currentGame) =>
            currentGame.TeamMembers.Any(a => a.Status == Enums.TeamMemberStatus.OnTeam);
    }
}