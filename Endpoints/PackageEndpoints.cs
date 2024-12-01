using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Models;

namespace OdaWepApi.Endpoints
{
    public static class PackageEndpoints
    {
        public static void MapPackageEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Package").WithTags(nameof(Package));

            // Get all Packages
            group.MapGet("/", async (OdaDbContext db) =>
                await db.Packages.ToListAsync()
            ).WithName("GetAllPackages").WithOpenApi();

            // Get Package by Id
            group.MapGet("/{id}", async Task<Results<Ok<Package>, NotFound>> (int packageid, OdaDbContext db) =>
                await db.Packages.AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Packageid == packageid)
                    is Package package
                        ? TypedResults.Ok(package)
                        : TypedResults.NotFound()
            ).WithName("GetPackageById").WithOpenApi();

            // Update Package
            group.MapPut("/{id}", async Task<IResult> (int packageid, Package package, OdaDbContext db) =>
            {
                if (package.Apartmentid != null && !await db.Apartments.AnyAsync(a => a.Apartmentid == package.Apartmentid))
                {
                    return Results.BadRequest("ApartmentId is invalid.");
                }

                package.Lastmodifieddatetime = DateTime.UtcNow;

                var affected = await db.Packages
                    .Where(p => p.Packageid == packageid)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(p => p.Packagename, package.Packagename)
                        .SetProperty(p => p.Apartmentid, package.Apartmentid)
                        .SetProperty(p => p.Price, package.Price)
                        .SetProperty(p => p.Description, package.Description)
                        .SetProperty(p => p.Assignedpackage, package.Assignedpackage)
                        .SetProperty(p => p.Lastmodifieddatetime, package.Lastmodifieddatetime));

                return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
            }).WithName("UpdatePackage").WithOpenApi();

            // Update Package Price
            group.MapPut("/{id}/price", async Task<Results<Ok, NotFound>> (int packageid, decimal newPrice, OdaDbContext db) =>
            {
                var affected = await db.Packages
                    .Where(p => p.Packageid == packageid)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(p => p.Price, newPrice)
                        .SetProperty(p => p.Lastmodifieddatetime, DateTime.UtcNow));

                return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
            }).WithName("UpdatePackagePrice").WithOpenApi();

            // Create a new Package
            group.MapPost("/", async (Package package, OdaDbContext db) =>
            {
                if (package.Apartmentid != null && !await db.Apartments.AnyAsync(a => a.Apartmentid == package.Apartmentid))
                {
                    return Results.BadRequest("ApartmentId is invalid.");
                }

                var maxPackageId = await db.Packages.MaxAsync(p => (int?)p.Packageid) ?? 0;
                package.Packageid = maxPackageId + 1;
                package.Createdatetime = DateTime.UtcNow;
                package.Lastmodifieddatetime = package.Createdatetime;

                db.Packages.Add(package);
                await db.SaveChangesAsync();
                return TypedResults.Created($"/api/Package/{package.Packageid}", package);
            }).WithName("CreatePackage").WithOpenApi();

            // Delete Package
            group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int packageid, OdaDbContext db) =>
            {
                var affected = await db.Packages
                    .Where(p => p.Packageid == packageid)
                    .ExecuteDeleteAsync();

                return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
            }).WithName("DeletePackage").WithOpenApi();
        }
    }
}