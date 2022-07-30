using Blazored.LocalStorage;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OctopusBlazor;
using OctopusBlazor.Data;
using OctopusBlazor.Service;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredToast();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration["BackendApiUrl"])
});

await builder.Build().RunAsync();
