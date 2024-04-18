namespace motinfoapp.Models;

public class VehicleInfoViewModel
{
    public string Registration { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public string Colour { get; set; }
    public DateTimeOffset MotExpiry { get; set; }
    public int MileageLastMot { get; set; }
}