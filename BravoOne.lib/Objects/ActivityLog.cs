using System;

using BravoOne.lib.Enums;

namespace BravoOne.lib.Objects
{
    public class ActivityLog
    {
        public Guid Id { get; set; }

        public DateTime TimeStamp { get; set; }

        public ActivityType ActivityLogType { get; set; }

        public string IconImagePath { get; set; }

        public string Title { get; set; }

        public string Detail { get; set; }
    }
}