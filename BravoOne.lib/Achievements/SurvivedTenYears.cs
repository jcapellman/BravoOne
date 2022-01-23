using BravoOne.lib.Achievements.Base;
using BravoOne.lib.Objects;

namespace BravoOne.lib.Achievements
{
    public class SurvivedTenYears : BaseAchievement
    {
        public override string Title => "Survivor for a 10 Years";

        public override string Description => "Lasted 10 years";

        public override bool VerifyAchievement(Game currentGame)
        {
            if (currentGame.gsMonths == 120)
            {
                return true;
            }

            return false;
        }
    }
}