using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Domain.Models;
using OdaWepApi.Infrastructure;

namespace OdaWepApi.API.Endpoints
{
    public static class ApartmentEndpoints
    {
        public static void MapApartmentEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Apartment").WithTags(nameof(Apartment));

            //Get all Apartment
            group.MapGet("/", async (OdaDbContext db) =>
                await db.Apartments.ToListAsync()
            ).WithName("GetAllApartments").WithOpenApi();

            group.MapGet("/{id}", async Task<Results<Ok<Apartment>, NotFound>> (int apartmentid, OdaDbContext db) =>
                await db.Apartments.AsNoTracking()
                    .FirstOrDefaultAsync(a => a.Apartmentid == apartmentid)
                    is Apartment apartment
                        ? TypedResults.Ok(apartment)
                        : TypedResults.NotFound()
            ).WithName("GetApartmentById").WithOpenApi();

            group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int apartmentid, Apartment apartment, OdaDbContext db) =>
            {
                apartment.Lastmodifieddatetime = DateTime.UtcNow;

                var affected = await db.Apartments
                    .Where(a => a.Apartmentid == apartmentid)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(a => a.Apartmentname, apartment.Apartmentname)
                        .SetProperty(a => a.Apartmenttype, apartment.Apartmenttype)
                        .SetProperty(a => a.Apartmentstatus, apartment.Apartmentstatus)
                        .SetProperty(a => a.Apartmentspace, apartment.Apartmentspace)
                        .SetProperty(a => a.Description, apartment.Description)
                        .SetProperty(a => a.Floornumber, apartment.Floornumber)
                        .SetProperty(a => a.Availabilitydate, apartment.Availabilitydate)
                        .SetProperty(a => a.Lastmodifieddatetime, apartment.Lastmodifieddatetime));

                return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
            }).WithName("UpdateApartment").WithOpenApi();

            group.MapPost("/", async (Apartment apartment, OdaDbContext db) =>
            {
                var maxId = await db.Apartments.MaxAsync(a => (int?)a.Apartmentid) ?? 0;
                apartment.Apartmentid = maxId + 1;
                apartment.Createddatetime = DateTime.UtcNow;
                apartment.Lastmodifieddatetime = apartment.Createddatetime;

                db.Apartments.Add(apartment);
                await db.SaveChangesAsync();
                return TypedResults.Created($"/api/Apartment/{apartment.Apartmentid}", apartment);
            }).WithName("CreateApartment").WithOpenApi();

            group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int apartmentid, OdaDbContext db) =>
            {
                var affected = await db.Apartments
                    .Where(a => a.Apartmentid == apartmentid)
                    .ExecuteDeleteAsync();

                return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
            }).WithName("DeleteApartment").WithOpenApi();
        }
    }
}
