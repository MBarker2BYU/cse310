namespace AmmoTracker.Models;

public class InventoryItem
{
    public long TypeID { get; set; }
    public string ManufacturerName { get; set; } = string.Empty;
    public string CaliberName { get; set; } = string.Empty;
    public string GrainValue { get; set; } = string.Empty;
    public long CurrentRounds { get; set; }
    public long MinimumThreshold { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal TotalValue { get; set; }
}