using System;
using System.Collections.Generic;

using BravoOne.lib.Enums;

namespace BravoOne.lib.Objects
{
    public class TeamMember
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public uint MonthlySalary { get; set; }

        public DateTime StartDate { get; set; }

        public int Health { get; set; }

        public TeamMemberStatus Status { get; set; }

        public uint SkillPoints { get; set; }

        public string Specialty { get; set; }

        public string Comments { get; set; }

        public string AvatarImagePath { get; set; }

        public bool Available { get; set; }

        public List<TeamEquipment> Equipment { get; set; }
    }
}