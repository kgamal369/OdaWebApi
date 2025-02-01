using Microsoft.EntityFrameworkCore;
using OdaWepApi.API.DomainEndpoints;
using OdaWepApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Configure DbContext with connection string
builder.Services.AddDbContext<OdaDbContext>(options =>
    options.UseNpgsql("Host=dpg-cuc1s39opnds738s419g-a.oregon-postgres.render.com;Database=odadb;Username=odadb_user;Password=iwiEqjZ2mwcqFuREbb8U1GNTyfxKbgGw;Port=5432;SslMode=Require;TrustServerCertificate=True"));


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    Console.WriteLine("Connection string is missing or not loaded.");
}
else
{
    Console.WriteLine($"Loaded connection string: {connectionString}");
}

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add Loggigng 
builder.Logging.ClearProviders();
builder.Logging.AddConsole();


// Configure Kestrel
if (builder.Environment.IsDevelopment())
{
    builder.WebHost.ConfigureKestrel(serverOptions =>
    {
        serverOptions.ListenAnyIP(5188); // HTTP
        serverOptions.ListenAnyIP(7205, listenOptions =>
        {
            listenOptions.UseHttps(); // HTTPS
        });
    });
}
else
{
    builder.WebHost.UseUrls("http://*:5188"); // Use HTTP in production (Render handles HTTPS)
}

var app = builder.Build();

// Enable Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "OdaWepApi v1");
});

// Only enable HTTPS redirection in development
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

app.Run();
