using Blazored.Modal;
using BlazorUI.Authentication;
using Domain.Interfaces;
using HttpServices;
using Microsoft.AspNetCore.Components.Authorization;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSyncfusionBlazor(options => { options.IgnoreScriptIsolation = true; });
builder.Services.AddScoped<AuthenticationStateProvider, SimpleAuthenticationStateProvider>();
builder.Services.AddScoped<IAuth, AuthImpl>();
builder.Services.AddScoped<IUser, UserHttpClient>();
builder.Services.AddScoped<IFlight, FlightHttpClient>();
builder.Services.AddScoped<IAmadeus, AmadeusHttpClient>();
builder.Services.AddBlazoredModal();

var app = builder.Build();

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjEzODAxQDMyMzAyZTMxMmUzMGRGVWZlR2cyM1ltNktSTUZWc1QrL2J1Vy83eW5zcDlPR2EwcW53TmZIM3c9");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
