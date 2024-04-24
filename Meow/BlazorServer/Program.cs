using BlazorServer.Data;
using DataLayer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using ServiceLayer2;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddDbContext<MeowDbContext>(options => 
    options.UseSqlServer("Server=TIMI-PC;Database=MeowDBV2.1;Trusted_Connection=True;Encrypt=False;MultipleActiveResultSets=true"),
    ServiceLifetime.Scoped
);

builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<CarContext, CarContext>();
builder.Services.AddScoped<CarManager, CarManager>();

builder.Services.AddScoped<ModelContext, ModelContext>();
builder.Services.AddScoped<ModelManager, ModelManager>();

builder.Services.AddScoped<AircraftContext, AircraftContext>();
builder.Services.AddScoped<AircraftManager, AircraftManager>();

var app = builder.Build();

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
