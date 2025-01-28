using Microsoft.EntityFrameworkCore;
using OdaWepApi.API.DomainEndpoints;
using OdaWepApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Configure DbContext with connection string
builder.Services.AddDbContext<OdaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
app.MapCustomerEndpoints();
app.MapDeveloperEndpoints();
app.MapInvoiceEndpoints();
app.MapPermissionEndpoints();
app.MapProjectEndpoints();
app.MapRoleEndpoints();
app.MapUserEndpoints();

app.Run();
