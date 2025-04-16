using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Serilog;
using OdaWepApi.API.DomainEndpoints;
using OdaWepApi.API.DTOEndpoints;
using OdaWepApi.Infrastructure;
using OdaWepApi.API.DomainEndpoints.Forms;
using OdaWepApi.API.DomainEndpoints.FaceLiftEndpoints;
using OdaWepApi.API.DomainEndpoints.LocateYourHome_BuildYourKit;
using OdaWepApi.API.DomainEndpoints.Common;

// Create Host Builder
var builder = WebApplication.CreateBuilder(args);

// Detect if running as a Windows Service
builder.Host.UseWindowsService();

// ✅ Setup Serilog for File Logging
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/OdaWebApi.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// ✅ Configure Logging
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders();
    loggingBuilder.AddSerilog();
});

// ✅ Add Controllers & JSON Options
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// ✅ Load Connection String from Configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    Log.Fatal("❌ Connection string is missing! App will not start.");
    throw new Exception("Connection string is missing.");
}
else
{
    Log.Information($"✅ Loaded connection string: {connectionString}");
}

// ✅ Configure Database Context
builder.Services.AddDbContext<OdaDbContext>(options =>
    options.UseNpgsql(connectionString));

// ✅ Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Configure CORS
var corsPolicy = "_myCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicy, builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// ✅ Configure Kestrel Server
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5188); // HTTP Port

    if (builder.Environment.IsDevelopment())
    {
        serverOptions.ListenAnyIP(7205, listenOptions =>
        {
            listenOptions.UseHttps();
        });
    }
    else
    {
        var certPath = Environment.GetEnvironmentVariable("CERT_PATH");
        var certPassword = Environment.GetEnvironmentVariable("CERT_PASSWORD");

        if (!string.IsNullOrEmpty(certPath) && !string.IsNullOrEmpty(certPassword) && File.Exists(certPath))
        {
            var certificate = new X509Certificate2(certPath, certPassword);
            serverOptions.ListenAnyIP(7205, listenOptions =>
            {
                listenOptions.UseHttps(certificate);
            });
            Log.Information($"✅ Loaded SSL certificate from: {certPath}");
        }
        else
        {
            Log.Warning("❌ HTTPS certificate path or password is missing, or the file does not exist!");
        }
    }
});

var app = builder.Build();

// ✅ Enable Serilog Request Logging
app.UseSerilogRequestLogging();

// ✅ Enable Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "OdaWepApi v1");
});

// ✅ Enable HTTPS Redirection
app.UseHttpsRedirection();

// ✅ Apply CORS Middleware
app.UseCors(corsPolicy);

app.UseAuthorization();
app.MapControllers();

// ✅ Map Additional Endpoints
app.MapAddonEndpoints();
app.MapAddperrequestEndpoints();
app.MapAnswerEndpoints();
app.MapApartmentEndpoints();
app.MapAutomationDetailsEndpoints();
app.MapAutomationEndpoints();
app.MapBookingDataInEndpoints();
app.MapBookingDataOutEndpoints();
app.MapBookingEndpoints();
app.MapContactUsEndpoints();
app.MapCustomerAnswerEndpoints();
app.MapCustomerEndpoints();
app.MapDeveloperEndpoints();
app.MapFaceLiftAddperrequestEndpoints();
app.MapFaceLiftAddonEndpoints();
app.MapFaceLiftApartmentDTOEndpoints();
app.MapFaceLiftBookingDataInDTOEndpoints();
app.MapFaceLiftBookingDataOutDTOEndpoints();
app.MapFaceLiftFormEndpoints();
app.MapInstallmentBreakdownEndpoints();
app.MapInvoiceEndpoints();
app.MapOdaAmbassadorEndpoints();
app.MapPaymentPlanEndpoints();
app.MapPermissionEndpoints();
app.MapPlanDetailsEndpoints();
app.MapPlanEndpoints();
app.MapProjectEndpoints();
app.MapQuestionEndpoints();
app.MapRoleEndpoints();
app.MapUnittypeEndpoints();
app.MapUserEndpoints();

// ✅ Ensure Database is Ready
try
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<OdaDbContext>();
        dbContext.Database.EnsureCreated();
        Log.Information("✅ Database is ready!");
    }
}
catch (Exception ex)
{
    Log.Fatal($"❌ Database initialization failed: {ex.Message}");
    throw;
}

// ✅ Run as a Windows Service
app.Run();
