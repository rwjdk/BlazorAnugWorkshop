using AnugBlazorWorkshop;
using AnugBlazorWorkshop.Services;
using AnugBlazorWorkshop.ViewModel;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


//builder.Services.AddScoped<IBookmarkService, BookmarkServiceReal>(); //Configure Injection
builder.Services.AddScoped<IBookmarkService, BookmarkService>(); //Configure Injection
builder.Services.AddScoped<AddEditViewModel>();

builder.Services.AddMudServices();



await builder.Build().RunAsync();
