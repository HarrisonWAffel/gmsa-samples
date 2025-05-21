using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using WebApplication2.Data;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
    .AddCommandLine(args)
    .Build();

var connString = config.GetValue<string>("connectionString:value");

if (connString == "")
{
    Console.WriteLine("The connection string environment variable was not detected. Attempting to use hard coded connection string from appsettings");
    connString = builder.Configuration.GetConnectionString("sqltest") ?? throw new InvalidOperationException("default connection string 'sqltest' not found.");
} else
{
    Console.WriteLine("The connection string environment variable was detected");
    Console.WriteLine(connString);
}

builder.Services.AddRazorPages();
builder.Services.AddDbContext<WebApplication2Context>(options =>
    options.UseSqlServer(connString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
