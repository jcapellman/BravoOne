using BravoOne.lib;
using BravoOne.lib.Objects;
using BravoOne.UWP.ViewModels.Base;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BravoOne.UWP.ViewModels
{
    public class GameViewModel : BaseViewModel
    {
        private List<TeamMember> _teamMembers;

        public List<TeamMember> TeamMembers
        {
            get => _teamMembers;

            set
            {
                _teamMembers = value;

                OnPropertyChanged();
            }
        }

        public GameViewModel(GameWrapper gWrapper) : base(gWrapper)
        {
            TeamMembers = gWrapper.CurrentGame.TeamMembers.Where(a => a.Status == lib.Enums.TeamMemberStatus.OnTeam).OrderBy(b => b.Name).ToList();
        }

        public void SaveGame()
        {
            gWrapper.DAL.Add(gWrapper.CurrentGame);
        }

        public void FireTeamMember(TeamMember member)
        {
            gWrapper.CurrentGame.FireTeamMember(member);
            TeamMembers = gWrapper.CurrentGame.TeamMembers.Where(a => a.Status == lib.Enums.TeamMemberStatus.OnTeam).OrderBy(b => b.Name).ToList();
        }

        // Returns a human-readable deadline string for an in-progress contract.
        public string GetContractDeadline(Contract contract)
        {
            var turns = contract.TurnsRemaining(gWrapper.CurrentGame.CurrentDate);

            if (turns <= 0)
            {
                return "Overdue";
            }

            return turns == 1 ? "1 month left" : $"{turns} months left";
        }

        public async Task<bool> EndMonthAsync()
        {
            var endofGame = await gWrapper.EndTurn();

            TeamMembers = gWrapper.CurrentGame.TeamMembers.Where(a => a.Status == lib.Enums.TeamMemberStatus.OnTeam).OrderBy(b => b.Name).ToList();

            if (gWrapper.Option.AutoSave)
            {
                SaveGame();
            }

            return endofGame;
        }

        // Builds the plain-text content for the end-of-month summary dialog.
        public string BuildTurnSummaryText()
        {
            var summary = gWrapper.CurrentGame.LastTurnSummary;
            if (summary == null || !summary.HasEvents)
                return null;

            var sb = new StringBuilder();

            var sign = summary.MoneyDelta >= 0 ? "+" : "";
            sb.AppendLine($"FUNDS  {sign}${summary.MoneyDelta}");

            if (summary.ContractsCompleted.Count > 0)
            {
                sb.AppendLine();
                sb.AppendLine("CONTRACTS COMPLETED:");
                foreach (var c in summary.ContractsCompleted)
                    sb.AppendLine($"  ✓  {c}");
            }

            if (summary.ContractsFailed.Count > 0)
            {
                sb.AppendLine();
                sb.AppendLine("CONTRACTS FAILED:");
                foreach (var c in summary.ContractsFailed)
                    sb.AppendLine($"  ✗  {c}");
            }

            if (summary.OperatorLevelUps.Count > 0)
            {
                sb.AppendLine();
                sb.AppendLine("OPERATOR EXPERIENCE:");
                foreach (var o in summary.OperatorLevelUps)
                    sb.AppendLine($"  ▲  {o}");
            }

            if (summary.OperatorsInjured.Count > 0)
            {
                sb.AppendLine();
                sb.AppendLine("CASUALTIES — INJURED:");
                foreach (var o in summary.OperatorsInjured)
                    sb.AppendLine($"  ⚕  {o}");
            }

            if (summary.OperatorsKilled.Count > 0)
            {
                sb.AppendLine();
                sb.AppendLine("CASUALTIES — KIA:");
                foreach (var o in summary.OperatorsKilled)
                    sb.AppendLine($"  ✝  {o}");
            }

            if (!string.IsNullOrEmpty(summary.RandomEventDescription))
            {
                sb.AppendLine();
                sb.AppendLine("INTEL EVENT:");
                sb.AppendLine($"  {summary.RandomEventDescription}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
