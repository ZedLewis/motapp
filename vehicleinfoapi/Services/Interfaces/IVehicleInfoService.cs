using vehicleinfoapi.Models;

namespace vehicleinfoapi.Services.Interfaces;

public interface IVehicleInfoService
{
    Task<VehicleDetailsViewModel> RetrieveVehicleDetails(string vehicleRegistration);
}