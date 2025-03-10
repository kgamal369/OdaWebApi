﻿using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.API.DomainEndpoints;
using OdaWepApi.API.DTOEndpoints;
using OdaWepApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Detect if running as a Windows Service
builder.Host.UseWindowsService();

// Add services to the container
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// Load Connection String from Configuration (appsettings.json or Environment Variables)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    Console.WriteLine("❌ Connection string is missing! App will not start.");
    throw new Exception("Connection string is missing.");
}
else
{
    Console.WriteLine($"✅ Loaded connection string: {connectionString}");
}

// Configure DbContext
builder.Services.AddDbContext<OdaDbContext>(options =>
    options.UseNpgsql(connectionString));

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Enable CORS with specific configuration
var corsPolicy = "_myCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicy, builder =>
    {
        builder.AllowAnyOrigin() // Allow all origins (Change to .WithOrigins("https://yourfrontend.com") in production for security)
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Add Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Configure Kestrel for https
builder.WebHost.ConfigureKestrel(serverOptions =>
  {
      serverOptions.ListenAnyIP(5188); // HTTP for local dev

      if (builder.Environment.IsDevelopment())
      {
          // Use default dev cert in development
          serverOptions.ListenAnyIP(7205, listenOptions =>
            {
                listenOptions.UseHttps();
            });
      }
      else
      {
          // Production environment requires a valid certificate
          var certPath = Environment.GetEnvironmentVariable("CERT_PATH");
          var certPassword = Environment.GetEnvironmentVariable("CERT_PASSWORD");

          if (!string.IsNullOrEmpty(certPath) && !string.IsNullOrEmpty(certPassword))
          {
              serverOptions.ListenAnyIP(7205, listenOptions =>
                {
                    listenOptions.UseHttps(certPath, certPassword);
                });
          }
          else
          {
              Console.WriteLine("❌ HTTPS certificate path or password is missing!");
              // Consider logging or handling this scenario appropriately
          }
      }
  });


var app = builder.Build();

// Enable Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "OdaWepApi v1");
});


// Enable HTTPS redirection
app.UseHttpsRedirection();

// Apply CORS Middleware (before authorization)
app.UseCors(corsPolicy);

app.UseAuthorization();
app.MapControllers();

// Map endpoints
app.MapAddonEndpoints();
app.MapAddperrequestEndpoints();
app.MapApartmentEndpoints();
app.MapAutomationEndpoints();
app.MapAutomationDetailsEndpoints();
app.MapBookingEndpoints();
app.MapCustomerEndpoints();
app.MapDeveloperEndpoints();
app.MapInstallmentBreakdownEndpoints();
app.MapInvoiceEndpoints();
app.MapPlanEndpoints();
app.MapPaymentPlanEndpoints();
app.MapPlanDetailsEndpoints();
app.MapPermissionEndpoints();
app.MapProjectEndpoints();
app.MapRoleEndpoints();
app.MapUserEndpoints();
app.MapBookingDataInEndpoints();
app.MapBookingDataOutEndpoints();
app.MapUnittypeEndpoints();
app.MapOdaAmbassadorEndpoints();
app.MapContactUsEndpoints();
// Ensure Database is Ready Before App Starts
try
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<OdaDbContext>();
        Console.WriteLine("🔄 Ensuring database is available...");
        dbContext.Database.EnsureCreated();
        Console.WriteLine("✅ Database is ready!");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Database initialization failed: {ex.Message}");
    throw;
}

// Start the application
app.Run();
