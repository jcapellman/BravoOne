using BravoOne.lib.Achievements.Base;
using BravoOne.lib.Objects;

namespace BravoOne.lib.Achievements
{
    public class BuyTenEquipment : BaseAchievement
    {
        public override string Title => "Arms Dealer";

        public override string Description => "Owned ten pieces of equipment";

        public override bool VerifyAchievement(Game currentGame) => currentGame.TeamEquipment.Count >= 10;
    }
}