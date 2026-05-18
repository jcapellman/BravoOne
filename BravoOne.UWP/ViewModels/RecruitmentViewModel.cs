using BravoOne.lib;
using BravoOne.lib.Objects;
using BravoOne.UWP.ViewModels.Base;

using System.Collections.Generic;
using System.Linq;

namespace BravoOne.UWP.ViewModels
{
    public class RecruitmentViewModel : BaseViewModel
    {
        private List<RecruitItem> _recruits;

        public List<RecruitItem> Recruits
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
            var availableMembers = gWrapper.CurrentGame.TeamMembers
                .Where(a => a.Status == lib.Enums.TeamMemberStatus.Available)
                .ToList();

            var items = new List<RecruitItem>();

            foreach (var recruit in availableMembers)
            {
                var item = new RecruitItem { Member = recruit };

                if (recruit.SkillPoints > gWrapper.CurrentGame.TeamLevel)
                {
                    item.StatusLabel = "TOO EXPERIENCED";
                    item.CanRecruit = false;
                }
                else if (recruit.MonthlySalary > gWrapper.CurrentGame.Money)
                {
                    item.StatusLabel = "INSUFFICIENT FUNDS";
                    item.CanRecruit = false;
                }
                else
                {
                    item.StatusLabel = "AVAILABLE";
                    item.CanRecruit = true;
                }

                items.Add(item);
            }

            // Available recruits always sort to the top, then by skill descending
            Recruits = items
                .OrderByDescending(a => a.CanRecruit)
                .ThenByDescending(a => a.Member.SkillPoints)
                .ToList();
        }

        public void AddTeamMember(RecruitItem item)
        {
            gWrapper.CurrentGame.AddTeamMember(item.Member);
            LoadRecruits();
        }
    }

    public class RecruitItem
    {
        public TeamMember Member { get; set; }
        public bool CanRecruit { get; set; }
        public string StatusLabel { get; set; }
        public string StatusColor => CanRecruit ? "#FF44FF55" : "#FFFF4400";
    }
}