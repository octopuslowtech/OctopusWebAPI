using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OctopusBlazor;
using OctopusBlazor.Service;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//builder.Services.AddHttpClient<IUserService, UserService>();
builder.Services.AddTransient<IAuthService, AuthService>();

builder.Services.AddSingleton<HttpClient>();

await builder.Build().RunAsync();
