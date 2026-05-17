using BravoOne.lib;
using BravoOne.lib.Enums;
using BravoOne.lib.Objects;
using BravoOne.UWP.ViewModels.Base;

using System;
using System.Collections.Generic;
using System.Linq;

namespace BravoOne.UWP.ViewModels
{
    public class ContractsViewModel : BaseViewModel
    {
        public ContractsViewModel(GameWrapper wrapper) : base(wrapper)
        {
        }

        public void AcceptContract(Contract contract)
        {
            gWrapper.CurrentGame.AddContract(contract);
        }

        public List<TeamMember> GetEligibleTeamMembers(Contract contract)
        {
            return gWrapper.CurrentGame.TeamMembers
                .Where(a => a.Status == TeamMemberStatus.OnTeam &&
                    contract.SpecialtiesRequired.ContainsKey(a.Specialty))
                .OrderByDescending(b => b.SkillPoints)
                .ToList();
        }

        public void AssignTeamMember(Contract contract, TeamMember member)
        {
            if (!contract.AssignedTeamMembers.Contains(member.Id))
            {
                contract.AssignedTeamMembers.Add(member.Id);
            }
        }

        public void UnassignTeamMember(Contract contract, Guid memberId)
        {
            contract.AssignedTeamMembers.Remove(memberId);
        }
    }
}