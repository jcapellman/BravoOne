using BravoOne.lib.Achievements.Base;
using BravoOne.lib.Objects;

namespace BravoOne.lib.Achievements
{
    public class BuyEquipment : BaseAchievement
    {
        public override string Title => "Equipment Purchaser";

        public override string Description => "Bought your first equipment";

        public override bool VerifyAchievement(Game currentGame)
        {
            if (currentGame.TeamEquipment.Count > 0)
            {
                return true;
            }

            return false;
        }
    }
}