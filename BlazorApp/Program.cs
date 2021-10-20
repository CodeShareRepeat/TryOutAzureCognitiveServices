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

const string url = "https://NAME.cognitiveservices.azure.com/";
const string key = "KEY";

builder.Services.AddSingleton<IFaceService, FaceService>(
    sp => new FaceService(url, key)
);

builder.Services.AddSingleton<ITextService, TextService>(
    sp => new TextService(url, key)
);

await builder.Build().RunAsync();