using BravoOne.lib;
using BravoOne.lib.Enums;
using BravoOne.lib.Objects;
using BravoOne.UWP.ViewModels.Base;

using System;
using System.Collections.Generic;
using System.Linq;

namespace BravoOne.UWP.ViewModels
{
    public class ManageTeamMemberViewModel : BaseViewModel
    {
        public ManageTeamMemberViewModel(GameWrapper gWrapper) : base(gWrapper)
        {
        }

        public List<Contract> GetActiveContracts()
        {
            return gWrapper.CurrentGame.Contracts
                .Where(a => a.Status == ContractStatus.InProgress)
                .ToList();
        }

        public List<TeamMember> GetOnTeamMembers()
        {
            return gWrapper.CurrentGame.TeamMembers
                .Where(a => a.Status == TeamMemberStatus.OnTeam)
                .OrderBy(b => b.Name)
                .ToList();
        }

        public void AssignMemberToContract(TeamMember member, Contract contract)
        {
            if (!contract.AssignedTeamMembers.Contains(member.Id))
            {
                contract.AssignedTeamMembers.Add(member.Id);
            }
        }

        public void UnassignMemberFromContract(Guid memberId, Contract contract)
        {
            contract.AssignedTeamMembers.Remove(memberId);
        }

        public bool IsMemberAssigned(TeamMember member, Contract contract)
        {
            return contract.AssignedTeamMembers.Contains(member.Id);
        }
    }
}