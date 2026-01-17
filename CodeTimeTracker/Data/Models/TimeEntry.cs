namespace CodeTimeTracker.Data.Models;

public class TimeEntry
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CodeObjectId { get; set; }            // foreign key to CodeObject
    public string TaskName { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }            // null = currently running
    public string Notes { get; set; } = string.Empty; // optional, for extra context

    public bool IsDeleted { get; set; } = false;

    // Computed properties (not stored)
    public TimeSpan Duration =>
        EndTime.HasValue ? EndTime.Value - StartTime : DateTime.Now - StartTime;

    public string DurationFormatted =>
        $"{Duration.Hours:D2}:{Duration.Minutes:D2}:{Duration.Seconds:D2}";
}