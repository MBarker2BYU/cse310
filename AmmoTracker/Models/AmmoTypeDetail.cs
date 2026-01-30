namespace AmmoTracker.Models;

public class AmmoTypeDetail
{
    public long TypeID { get; set; }
    public long ManufacturerID { get; set; }
    public long CaliberID { get; set; }
    public long GrainID { get; set; }
    public long MinimumThreshold { get; set; }
}