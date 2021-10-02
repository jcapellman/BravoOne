using System;
using System.Collections.Generic;
using System.Linq;

using BravoOne.lib.Enums;

namespace BravoOne.lib.Objects
{
    public class Contract
    {
        public Guid Id { get; set; }

        public ulong Income { get; set; }

        public ulong Penalty { get; set; }

        public string Name { get; set; }

        public ContractStatus Status { get; set; }

        public string CompletedDateString { get; set; }

        public DateTime CompleteDate { get; set; }

        public int TeamMemberToll { get; set; }

        public int SkillPointsRemaining { get; set; }

        public List<Guid> AssignedTeamMembers { get; set; }

        internal List<TeamMember> EndTurn(DateTime currentDate, Dictionary<Guid, TeamMember> teamMembers)
        {
            if (Status != ContractStatus.InProgress)
            {
                return teamMembers.Values.ToList();
            }

            foreach (Guid guid in AssignedTeamMembers)
            {
                SkillPointsRemaining -= teamMembers[guid].SkillPoints;
                teamMembers[guid].Status -= TeamMemberToll;
            }

            if (SkillPointsRemaining <= 0)
            {
                Status = ContractStatus.Completed;
            } else if (currentDate > CompleteDate)
            {
                Status = ContractStatus.Failed;
            }

            return teamMembers.Values.ToList();
        }
    }
}