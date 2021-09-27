using System;
using System.Collections.Generic;

namespace BravoOne.lib.Objects
{
    public class Game
    {
        public List<TeamMember> TeamMembers { get; set; }

        public DateTime CurrentDate { get; set; }

        public Game()
        {
            CurrentDate = DateTime.Now;
        }

        public void EndTurn()
        {
            CurrentDate = CurrentDate.AddMonths(1);
        }
    }
}
