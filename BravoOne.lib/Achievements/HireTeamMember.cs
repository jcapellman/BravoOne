using BravoOne.lib.Achievements.Base;
using BravoOne.lib.Objects;

namespace BravoOne.lib.Achievements
{
    public class HireTeamMember : BaseAchievement
    {
        
        public override string Title => "Mereceny for Hire";

        public override string Description => "Hired a team member";

        public override int Id => 1;

        public override bool VerifyAchievement(Game currentGame)
        {
            if (currentGame.TeamMembers.Count > 0)
            {
                return true;
            }

            return false;
        }
    }
}