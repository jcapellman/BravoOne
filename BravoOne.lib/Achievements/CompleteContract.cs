using BravoOne.lib.Achievements.Base;
using BravoOne.lib.Objects;

using System.Linq;

namespace BravoOne.lib.Achievements
{
    public class CompleteContract : BaseAchievement
    {
        public override string Title => "Completed a Gig";

        public override string Description => "Completed your first contract";

        public override bool VerifyAchievement(Game currentGame) => currentGame.Contracts.Any(a => a.Status == Enums.ContractStatus.Completed);
    }
}