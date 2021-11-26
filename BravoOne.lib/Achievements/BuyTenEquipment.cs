using BravoOne.lib.Achievements.Base;
using BravoOne.lib.Objects;

namespace BravoOne.lib.Achievements
{
    internal class BuyTenEquipment : BaseAchievement
    {
        public override string Title => "Arms Dealer";

        public override string Description => "Owned ten pieces of equipment";

        public override bool VerifyAchievement(Game currentGame)
        {
            if (currentGame.TeamEquipment.Count >= 10)
            {
                return true;
            }

            return false;
        }
    }
}