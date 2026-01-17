namespace CodeTimeTracker.Data.Models;

public class Project
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;  // e.g. "Customer Portal", "Inventory System"
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}