using System;

namespace BravoOne.lib.UIObjects
{
    public class AchievementsListingItem
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public bool Unlocked { get; set; }

        public DateTime? TimeStamp { get; set; }
    }
}