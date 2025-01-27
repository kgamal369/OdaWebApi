using Microsoft.EntityFrameworkCore;
using OdaWepApi.API.DomainEndpoints;
using OdaWepApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Configure DbContext with connection string
builder.Services.AddDbContext<OdaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Swagger services, etc.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Kestrel to listen on specific URLs
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5188); // HTTP
    serverOptions.ListenAnyIP(7205, listenOptions =>
    {
        listenOptions.UseHttps(); // HTTPS
    });
});

var app = builder.Build();

// Enable Swagger middleware in development mode
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "OdaWepApi v1");
    });
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapAddonEndpoints();

app.MapApartmentEndpoints();

app.MapAddperrequestEndpoints();

//app.MapBookingEndpoints();

app.MapCustomerEndpoints();

app.MapDeveloperEndpoints();

app.MapInvoiceEndpoints();

//app.MapPackageEndpoints();

app.MapPermissionEndpoints();

app.MapProjectEndpoints();

app.MapRoleEndpoints();

app.MapUserEndpoints();
app.Run();
