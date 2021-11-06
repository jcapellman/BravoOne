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
            var availableMembers = gWrapper.CurrentGame.TeamMembers.Where(a => a.Status == lib.Enums.TeamMemberStatus.Available).OrderByDescending(b => b.MonthlySalary).ToList();

            Recruits = new List<TeamMember>();

            foreach (var recruit in availableMembers)
            {
                recruit.Available = true;

                recruit.Comments = "Available for your team";

                if (recruit.SkillPoints > gWrapper.CurrentGame.TeamLevel)
                {
                    recruit.Comments = "Recruit is too experienced for your team";
                    recruit.Available = false;
                }
                else if (recruit.MonthlySalary > gWrapper.CurrentGame.Money)
                {
                    recruit.Comments = "Recruit is too expensive for your team";
                    recruit.Available = false;
                }

                Recruits.Add(recruit);
            }
        }

        public void AddTeamMember(TeamMember recruit)
        {
            gWrapper.CurrentGame.AddTeamMember(recruit);

            LoadRecruits();
        }
    }
}