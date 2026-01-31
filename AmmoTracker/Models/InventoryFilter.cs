namespace AmmoTracker.Models;

public class InventoryFilter
{
    public long? ManufacturerId { get; set; }
    public long? CaliberId { get; set; }
    public long? GrainId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? LotNumber { get; set; }

}