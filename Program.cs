using Microsoft.EntityFrameworkCore;
using OdaWepApi.API.DomainEndpoints;
using OdaWepApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

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

// Add Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Configure Kestrel for local development
if (builder.Environment.IsDevelopment())
{
    builder.WebHost.ConfigureKestrel(serverOptions =>
    {
        serverOptions.ListenAnyIP(5188); // HTTP for local dev
        serverOptions.ListenAnyIP(7205, listenOptions =>
        {
            listenOptions.UseHttps(); // HTTPS for local dev
        });
    });
}
else
{
    // Use only HTTP in production (Render handles HTTPS automatically)
    var port = Environment.GetEnvironmentVariable("PORT") ?? "5188";
    builder.WebHost.UseUrls($"http://*:{port}");
}

var app = builder.Build();

// Enable Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "OdaWepApi v1");
});

// Only enable HTTPS redirection in local development
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();
app.MapControllers();

// Map endpoints
app.MapAddonEndpoints();
app.MapAddperrequestEndpoints();
app.MapApartmentEndpoints();
app.MapAutomationEndpoints();
app.MapAutomationDetailsEndpoints();
app.MapCustomerEndpoints();
app.MapDeveloperEndpoints();
app.MapInvoiceEndpoints();
app.MapPlanEndpoints();
app.MapPlanDetailsEndpoints();
app.MapPermissionEndpoints();
app.MapProjectEndpoints();
app.MapRoleEndpoints();
app.MapUserEndpoints();

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
