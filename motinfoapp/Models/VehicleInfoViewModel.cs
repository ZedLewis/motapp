namespace motinfoapp.Models;

public class VehicleInfoViewModel
{
    public string registration { get; set; }
    public string make { get; set; }
    public string model { get; set; }
    public string colour { get; set; }
    public DateTime motExpiry { get; set; }
    public int mileageLastMot { get; set; }
}