using System.Collections.Generic;

namespace CodeTimeTracker.Data.Models
{
    public class TimeTrackerData
    {
        public List<Project> Projects { get; set; } = new List<Project>();
        public List<CodeObject> CodeObjects { get; set; } = new List<CodeObject>();
        public List<TimeEntry> TimeEntries { get; set; } = new List<TimeEntry>();
    }
}