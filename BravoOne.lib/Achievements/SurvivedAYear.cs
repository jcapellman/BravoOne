using BravoOne.lib.Achievements.Base;
using BravoOne.lib.Objects;

namespace BravoOne.lib.Achievements
{
    public class SurvivedAYear : BaseAchievement
    {
        public override string Title => "Survivor for a Year";

        public override string Description => "Lasted a year";

        public override bool VerifyAchievement(Game currentGame)
        {
            if (currentGame.gsMonths == 12)
            {
                return true;
            }

            return false;
        }
    }
}