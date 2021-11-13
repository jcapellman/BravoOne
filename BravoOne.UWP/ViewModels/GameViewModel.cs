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

        public async Task<bool> EndMonthAsync()
        {
            var endofGame = await gWrapper.EndTurn();
            
            if (!endofGame)
            {
                return false;
            }

            if (gWrapper.Option.AutoSave)
            {
                SaveGame();
            }

            return true;
        }
    }
}