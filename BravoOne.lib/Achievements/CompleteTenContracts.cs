using BravoOne.lib.Achievements.Base;
using BravoOne.lib.Objects;

using System.Linq;

namespace BravoOne.lib.Achievements
{
    public class CompleteTenContracts : BaseAchievement
    {
        public override string Title => "Contract King";

        public override string Description => "Complete 10 Contracts";

        public override bool VerifyAchievement(Game currentGame) => currentGame.Contracts.Count(a => a.Status == Enums.ContractStatus.Completed) > 10;
    }
}