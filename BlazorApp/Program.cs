using System;
using System.Net.Http;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorApp;
using Microsoft.Extensions.DependencyInjection;
using BlazorApp.Api.Face;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});

builder.Services.AddSingleton<IFaceService, FaceService>(
    sp => new FaceService("https://YOURCOGNITIVESERVICE.cognitiveservices.azure.com/", "YOURKEY")
);
await builder.Build().RunAsync();