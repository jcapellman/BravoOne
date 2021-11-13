using System;

namespace BravoOne.lib.Objects
{
    public class Achievements
    {
        public Guid Id { get; set; }

        public DateTime TimeStamp { get; set; }
        
        public string AchievementTypeName { get; set;  }
    }
}