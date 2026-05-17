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

        // Set once at creation; used for XP calculation so harder contracts reward more.
        public uint SkillPointsTotal { get; set; }

        public Dictionary<Specialties, int> SpecialtiesRequired { get; set; }

        public List<Guid> AssignedTeamMembers { get; set; }

        public int TurnsRemaining(DateTime currentDate)
        {
            if (CompleteDate <= currentDate)
            {
                return 0;
            }

            return (CompleteDate.Year - currentDate.Year) * 12 + CompleteDate.Month - currentDate.Month;
        }
    }
}