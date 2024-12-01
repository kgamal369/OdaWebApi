using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Domain.Models;
using OdaWepApi.Infrastructure;

namespace OdaWepApi.Endpoints
{
    public static class ApartmentaddonEndpoints
    {
        public static void MapApartmentaddonEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Apartmentaddon").WithTags(nameof(Apartmentaddon));

            // Get all Apartmentaddons
            group.MapGet("/", async (OdaDbContext db) =>
                await db.Apartmentaddons.ToListAsync()
            ).WithName("GetAllApartmentaddons").WithOpenApi();

            // Get Apartmentaddon by Id
            group.MapGet("/{id}", async Task<Results<Ok<Apartmentaddon>, NotFound>> (int apartmentaddonsid, OdaDbContext db) =>
                await db.Apartmentaddons.AsNoTracking()
                    .FirstOrDefaultAsync(a => a.Apartmentaddonsid == apartmentaddonsid)
                    is Apartmentaddon apartmentaddon
                        ? TypedResults.Ok(apartmentaddon)
                        : TypedResults.NotFound()
            ).WithName("GetApartmentaddonById").WithOpenApi();

            // Update Apartmentaddon
            group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int apartmentaddonsid, Apartmentaddon apartmentaddon, OdaDbContext db) =>
            {
                var affected = await db.Apartmentaddons
                    .Where(a => a.Apartmentaddonsid == apartmentaddonsid)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(a => a.Apartmentid, apartmentaddon.Apartmentid)
                        .SetProperty(a => a.Addonid, apartmentaddon.Addonid)
                        .SetProperty(a => a.Availableaddons, apartmentaddon.Availableaddons)
                        .SetProperty(a => a.Assignedaddons, apartmentaddon.Assignedaddons)
                        .SetProperty(a => a.Maxavailable, apartmentaddon.Maxavailable)
                        .SetProperty(a => a.Installedamount, apartmentaddon.Installedamount)
                        .SetProperty(a => a.Lastmodifieddatetime, DateTime.UtcNow));

                return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
            }).WithName("UpdateApartmentaddon").WithOpenApi();

            // Create Apartmentaddon
            group.MapPost("/", async (Apartmentaddon apartmentaddon, OdaDbContext db) =>
            {
                db.Apartmentaddons.Add(apartmentaddon);
                await db.SaveChangesAsync();
                return TypedResults.Created($"/api/Apartmentaddon/{apartmentaddon.Apartmentaddonsid}", apartmentaddon);
            }).WithName("CreateApartmentaddon").WithOpenApi();

            // Delete Apartmentaddon
            group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int apartmentaddonsid, OdaDbContext db) =>
            {
                var affected = await db.Apartmentaddons
                    .Where(a => a.Apartmentaddonsid == apartmentaddonsid)
                    .ExecuteDeleteAsync();

                return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
            }).WithName("DeleteApartmentaddon").WithOpenApi();
        }
    }
}