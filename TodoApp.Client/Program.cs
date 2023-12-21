using Blazored.Toast;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TodoApp.Client;
using TodoApp.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["BackendApiUrl"])});

builder.Services.AddScoped<ITaskApiClient, TaskApiClient>();
builder.Services.AddScoped<IUserApiClient, UserApiClient>();
builder.Services.AddBlazoredToast();

await builder.Build().RunAsync();
