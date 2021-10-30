using BravoOne.lib;
using BravoOne.lib.Objects;
using BravoOne.UWP.ViewModels.Base;

using System.Collections.Generic;
using System.Linq;

namespace BravoOne.UWP.ViewModels
{
    public class RecruitmentViewModel : BaseViewModel
    {
        private List<TeamMember> _recruits;

        public List<TeamMember> Recruits
        {
            get => _recruits;

            set
            {
                _recruits = value;

                OnPropertyChanged();
            }
        }

        public RecruitmentViewModel(GameWrapper gWrapper) : base(gWrapper)
        {
            LoadRecruits();
        }

        private void LoadRecruits()
        {
            Recruits = gWrapper.CurrentGame.TeamMembers.Where(a => !a.OnTeam).OrderByDescending(b => b.MonthlySalary).ToList();
        }

        public void AddTeamMember(TeamMember recruit)
        {
            var index = gWrapper.CurrentGame.TeamMembers.IndexOf(recruit);

            recruit.OnTeam = true;

            gWrapper.CurrentGame.TeamMembers[index] = recruit;

            LoadRecruits();
        }
    }
}