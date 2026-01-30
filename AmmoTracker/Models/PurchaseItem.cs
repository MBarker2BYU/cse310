namespace AmmoTracker.Models;

public class PurchaseItem
{
    public long PurchaseID { get; set; }
    public long TypeID { get; set; }
    public DateTime PurchaseDate { get; set; }
    public long RoundsAdded { get; set; }
    public long RoundsPerContainer { get; set; }
    public long Containers { get; set; }
    public string? LotNumber { get; set; }
    public decimal CostPerRound { get; set; }
    public decimal TotalCost { get; set; }
}