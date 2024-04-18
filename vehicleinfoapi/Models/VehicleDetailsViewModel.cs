namespace vehicleinfoapi.Models;

public class VehicleDetailsViewModel
{
    public string Registration { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public string Colour { get; set; }
    public DateTime MotExpiry { get; set; }
    public int MileageLastMot { get; set; }
}