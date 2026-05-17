using BravoOne.lib.Achievements.Base;
using BravoOne.lib.Objects;

namespace BravoOne.lib.Achievements
{
    public class HireTenTeamMembers : BaseAchievement
    {

        public override string Title => "Hiring Manager";

        public override string Description => "Hired 10 team members";

        public override bool VerifyAchievement(Game currentGame) => currentGame.TeamMembers.Count > 10;
    }
}