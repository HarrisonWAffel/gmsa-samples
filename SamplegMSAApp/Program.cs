using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionStringFromEnv = Environment.GetEnvironmentVariable("CONNECTION_STRING");
if (!string.IsNullOrEmpty(connectionStringFromEnv))
{
    builder.Configuration["ConnectionStrings:sqltest"] = connectionStringFromEnv;
    Console.WriteLine("Using connection string from CONNECTION_STRING environment variable.");
}

// GetConnectionString will now resolve the value from the environment variable if it was set,
// otherwise it will fall back to appsettings.json.
var connString = builder.Configuration.GetConnectionString("sqltest") ?? throw new InvalidOperationException("Connection string 'sqltest' not found.");

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
