using System;
using System.Net.Http;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorApp;
using Microsoft.Extensions.DependencyInjection;
using BlazorApp.Api.Face;
using BlazorApp.Api.Text;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});

builder.Services.AddSingleton<IFaceService, FaceService>(
    sp => new FaceService("https://YOUR-INSTANCENAME.cognitiveservices.azure.com/", "KEY")
);

builder.Services.AddSingleton<ITextService, TextService>(
    sp => new TextService("https://YOUR-INSTANCENAME.cognitiveservices.azure.com/", "KEY")
);

await builder.Build().RunAsync();