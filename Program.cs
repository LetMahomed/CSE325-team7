// Program.cs — Application entry point for Remy's Recipes Blazor WebAssembly app.
// Configures services (HttpClient, RecipeService) and launches the app.

using CSE325_team7;
using CSE325_team7.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Register root Blazor components
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register HttpClient for fetching static assets (e.g., recipes.json)
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

// Register RecipeService for localStorage-backed recipe CRUD operations
builder.Services.AddScoped<RecipeService>();

await builder.Build().RunAsync();
