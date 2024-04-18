using System.Text.Json;
using Microsoft.AspNetCore.Components;
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

    public static async Task<VehicleInfoViewModel> GetVehicleInfo(string vehicleRegistration)
    {
        try
        {
            string apiUrl = $"https://beta.check-mot.service.gov.uk/trade/vehicles/mot-tests?registration={vehicleRegistration}";
            
            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            request.Headers.Add("x-api-key", "fZi8YcjrZN1cGkQeZP7Uaa4rTxua8HovaswPuIno");
            
            Console.WriteLine(request.ToString());
            
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            
            Console.WriteLine(response);

            // var getVehicleMotResponse = await _httpClient.GetAsync($"trade/vehicles/mot-tests?registration={vehicleRegistration}");

             var result = await response.Content.ReadAsStringAsync();
             
             Console.WriteLine(result);

            var vehicleMotData = JsonSerializer.Deserialize<VehicleInfoViewModel>(result);

            return vehicleMotData;


        }
        catch (Exception ex)
        {
            // Handle exceptions (e.g., network errors)
            Console.WriteLine($"Error: {ex.Message}");
            return null;
        }
    }

    public class VehicleInfo
    {
        public string registration { get; set; }
        public string make { get; set; }
        public string model { get; set; }
        public string primaryColour { get; set; }
        public List<MotData> motTests { get; set; }
    }

    public class MotData
    {
        public DateTime expiryDate { get; set; }
        public int odometerValue { get; set; }
    } 
}