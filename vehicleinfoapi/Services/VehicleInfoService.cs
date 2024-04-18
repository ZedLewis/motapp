using System.Text.Json;
using vehicleinfoapi.Models;
using vehicleinfoapi.Services.Interfaces;

namespace vehicleinfoapi.Services;

public class VehicleInfoService : IVehicleInfoService
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;

    public VehicleInfoService(
        IConfiguration configuration
    )
    {
        _configuration = configuration;
        var apiUrl = _configuration["AppSettings:BaseAddress"];
        var apiKey = _configuration["AppSettings:ApiKey"];
        var apiName = _configuration["AppSettings:ApiKeyHeaderName"];

        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(apiUrl);
        _httpClient.DefaultRequestHeaders.Add(apiName!, apiKey);
    }

    public async Task<VehicleDetailsViewModel> RetrieveVehicleDetails(string vehicleRegistration)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"/trade/vehicles/mot-tests?registration={vehicleRegistration}");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var response = await _httpClient.SendAsync(request);

            var jsonString = await response.Content.ReadAsStringAsync();

            var motDetails = JsonSerializer.Deserialize<List<VehicleInfo>>(jsonString, options);
            var latestMot = GetLatestMot(motDetails);


            return new VehicleDetailsViewModel
            {
                Registration = vehicleRegistration.ToUpper(),
                Make = motDetails[0].make,
                Model = motDetails[0].model,
                Colour = motDetails[0].primaryColour,
                MotExpiry = DateTime.Parse(latestMot.expiryDate).Date,
                MileageLastMot = int.Parse(latestMot.odometerValue)
            };
        }
        catch
        {
            throw new Exception("No vehicle MOT details found.");
        }
    }

    private static MotTest GetLatestMot(List<VehicleInfo> motDetails)
    {
        return new MotTest
        {
            expiryDate = motDetails[0].motTests[0].expiryDate,
            odometerValue = motDetails[0].motTests[0].odometerValue
        };
    }

    private class VehicleInfo
    {
        public string make { get; set; }
        public string model { get; set; }
        public string primaryColour { get; set; }
        public List<MotTest> motTests { get; set; }
    }

    private class MotTest
    {
        public string expiryDate { get; set; }
        public string odometerValue { get; set; }
    }
}