using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using motinfoapp;
using motinfoapp.Services;
using motinfoapp.Services.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient 
{ 
    BaseAddress = new Uri("http://localhost:5088") 
});
builder.Services.AddScoped<IVehicleInfoService, VehicleInfoService>();

await builder.Build().RunAsync();

