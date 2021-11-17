using System;
using System.Collections.Generic;

using BravoOne.lib.Enums;

namespace BravoOne.lib.Objects
{
    public class Contract
    {
        public Guid Id { get; set; }

        public ContractType CType { get; set; }

        public ulong Income { get; set; }

        public ulong Penalty { get; set; }

        public string Name { get; set; }

        public ContractStatus Status { get; set; }

        public string CompletedDateString { get; set; }

        public DateTime CompleteDate { get; set; }

        public int TeamMemberToll { get; set; }

        public uint SkillPointsRemaining { get; set; }

        public Dictionary<Specialties, int> SpecialtiesRequired { get; set; }

        public List<Guid> AssignedTeamMembers { get; set; }
    }
}