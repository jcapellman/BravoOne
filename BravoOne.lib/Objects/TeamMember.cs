using System;

namespace BravoOne.lib.Objects
{
    public class TeamMember
    {
        public Guid Id { get; set; }

        public bool OnTeam { get; set; }

        public string Name { get; set; }

        public uint MonthlySalary { get; set; }

        public DateTime StartDate { get; set; }

        public int Health { get; set; }

        public int Status { get; set; }

        public uint SkillPoints { get; set; }

        public string Specialty { get; set; }

        public string Comments { get; set; }

        public string AvatarImagePath { get; set; }
    }
}