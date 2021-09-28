using System;
using System.Collections.Generic;

namespace BravoOne.lib.Objects
{
    public class Game
    {
        public List<TeamMember> TeamMembers { get; set; }

        public DateTime CurrentDate { get; set; }

        public ulong Money { get; set; }

        public Game()
        {
            CurrentDate = DateTime.Now;

            TeamMembers = new List<TeamMember>();

            Money = 10000000;
        }

        public void EndTurn()
        {
            CurrentDate = CurrentDate.AddMonths(1);

            foreach (TeamMember member in TeamMembers)
            {
                Money -= member.MonthlySalary;
            }
        }
    }
}
