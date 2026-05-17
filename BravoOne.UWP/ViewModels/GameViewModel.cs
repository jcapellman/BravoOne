using BravoOne.lib;
using BravoOne.lib.Objects;
using BravoOne.UWP.ViewModels.Base;

using System.Collections.Generic;
using System.Linq;
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

            if (!endofGame)
            {
                return false;
            }

            TeamMembers = gWrapper.CurrentGame.TeamMembers.Where(a => a.Status == lib.Enums.TeamMemberStatus.OnTeam).OrderBy(b => b.Name).ToList();

            if (gWrapper.Option.AutoSave)
            {
                SaveGame();
            }

            return true;
        }
    }
}