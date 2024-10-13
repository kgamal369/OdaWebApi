using Microsoft.EntityFrameworkCore;
using OdaWepApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Configure DbContext with connection string
builder.Services.AddDbContext<OdaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Swagger services, etc.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.MapApartmentaddonEndpoints();

app.MapBookingEndpoints();

app.MapCustomerEndpoints();

app.MapInvoiceEndpoints();

app.MapPackageEndpoints();

app.MapPermissionEndpoints();

app.MapProjectEndpoints();

app.MapRoleEndpoints();

app.MapUserEndpoints();
app.Run();
