using Microsoft.AspNetCore.Mvc;
using vehicleinfoapi.Models;
using vehicleinfoapi.Services.Interfaces;

namespace vehicleinfoapi.Controllers;

[Route("api/[controller]")]
public class VehicleInfoController : Controller
{
    private readonly IVehicleInfoService _vehicleInfoService;

    public VehicleInfoController(
        IVehicleInfoService vehicleInfoService
    )
    {
        _vehicleInfoService = vehicleInfoService;
    }

    [HttpGet("RetrieveVehicleDetails")]
    public async Task<VehicleDetailsViewModel> RetrieveVehicleDetails([FromQuery] string vehicleRegistration)
    {
        return await _vehicleInfoService.RetrieveVehicleDetails(vehicleRegistration);
    }
}