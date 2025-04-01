using System;

namespace LifeTrack.Core.Models
{
    public class AppUsage
    {
        public int Id { get; set; }

        public string AppName { get; set; }

        public string PackageName { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan UsageDuration { get; set; }

        public int LaunchCount { get; set; }
    }
}