using System.Net.Http.Json;
using motinfoapp.Models;
using motinfoapp.Services.Interfaces;

namespace motinfoapp.Services;

public class VehicleInfoService : IVehicleInfoService
{
    private static HttpClient _httpClient;

    public VehicleInfoService(
        HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<VehicleInfoViewModel?> GetVehicleInfo(string vehicleRegistration)
    {
        var endpoint = $"/api/VehicleInfo/RetrieveVehicleDetails?vehicleRegistration={vehicleRegistration}";

        var response = await _httpClient.GetAsync(endpoint);
        
        var vehicleDetails = await response.Content.ReadFromJsonAsync<VehicleInfoViewModel>();
        return vehicleDetails;
    }
}