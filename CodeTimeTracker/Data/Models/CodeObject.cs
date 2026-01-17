namespace CodeTimeTracker.Data.Models;

public class CodeObject
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ProjectId { get; set; }               // foreign key
    public string Name { get; set; } = string.Empty;  // e.g. "MainForm", "UserService", "LoginViewModel"
    public string Type { get; set; } = string.Empty;  // optional: "Form", "Class", "UserControl", "Service", etc.
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
