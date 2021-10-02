using System;

namespace BravoOne.lib.Objects
{
    public class TeamMember
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public uint MonthlySalary { get; set; }

        public DateTime StartDate { get; set; }

        public int Health { get; set; }

        public int Status { get; set; }

        public int SkillPoints { get; set; }
    }
}