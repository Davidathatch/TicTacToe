using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TicTacToe;
using TicTacToe.Models.ConstantModels;
using TicTacToe.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//JsStylingFunctions is used to call JS functions that alter the game's visuals
builder.Services.AddSingleton<JsStylingFunctions>();

//Config contains string values used across the app
builder.Services.AddSingleton<GameConstants>();

await builder.Build().RunAsync();