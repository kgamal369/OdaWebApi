using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Models;

namespace OdaWepApi.Models;

public partial class Package
{
    [Key]
    public int Packageid { get; set; }

    [Required(ErrorMessage = "Package Name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Package Name must be between 2 and 100 characters.")]
    public string? Packagename { get; set; }

    public int? Apartmentid { get; set; }

    public decimal? Price { get; set; }

    public string? Description { get; set; }

    public bool? Assignedpackage { get; set; }

    public DateTime? Createdatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }

    public virtual Apartment? Apartment { get; set; }
}


public static class PackageEndpoints
{
    public static void MapPackageEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Package").WithTags(nameof(Package));

        // Get all Packages
        group.MapGet("/", async (OdaDbContext db) =>
        {
            return await db.Packages.ToListAsync();
        })
        .WithName("GetAllPackages")
        .WithOpenApi();

        // Get Package by Id
        group.MapGet("/{id}", async Task<Results<Ok<Package>, NotFound>> (int packageid, OdaDbContext db) =>
        {
            return await db.Packages.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Packageid == packageid)
                is Package model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetPackageById")
        .WithOpenApi();

        // Update Package
        group.MapPut("/{id}", async Task<IResult> (int packageid, Package package, OdaDbContext db) =>
        {
            // Validate ApartmentId
            if (package.Apartmentid != null && !db.Apartments.Any(a => a.Apartmentid == package.Apartmentid))
            {
                return Results.BadRequest("ApartmentId is invalid.");
            }

            // Set the Lastmodifieddatetime to the current time for updates
            package.Lastmodifieddatetime = DateTime.UtcNow;


            var affected = await db.Packages
                .Where(model => model.Packageid == packageid)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Packageid, package.Packageid)
                  .SetProperty(m => m.Packagename, package.Packagename)
                  .SetProperty(m => m.Apartmentid, package.Apartmentid)
                  .SetProperty(m => m.Price, package.Price)
                  .SetProperty(m => m.Description, package.Description)
                  .SetProperty(m => m.Assignedpackage, package.Assignedpackage)
                  .SetProperty(m => m.Lastmodifieddatetime, package.Lastmodifieddatetime)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdatePackage")
        .WithOpenApi();

        // Update Package Price
        group.MapPut("/{id}/price", async Task<Results<Ok, NotFound>> (int packageid, decimal newPrice, OdaDbContext db) =>
        {

            var affected = await db.Packages
                .Where(p => p.Packageid == packageid)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(p => p.Price, newPrice)
                    .SetProperty(p => p.Lastmodifieddatetime, DateTime.UtcNow)
                );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdatePackagePrice")
        .WithOpenApi();

        // Create a new Package
        group.MapPost("/", async (Package package, OdaDbContext db) =>
        {
            // Validate ApartmentId
            if (package.Apartmentid != null && !db.Apartments.Any(a => a.Apartmentid == package.Apartmentid))
            {
                return Results.BadRequest("ApartmentId is invalid.");
            }

            // Set PackageId to MaxPackageId + 1
            var maxPackageId = await db.Packages.MaxAsync(p => (int?)p.Packageid) ?? 0;
            package.Packageid = maxPackageId + 1;


            // Set the Createdatetime for the first insertion
            package.Createdatetime = DateTime.UtcNow;
            package.Lastmodifieddatetime = package.Createdatetime;

            db.Packages.Add(package);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Package/{package.Packageid}", package);
        })
        .WithName("CreatePackage")
        .WithOpenApi();

        // Delete Package
        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int packageid, OdaDbContext db) =>
        {
            var affected = await db.Packages
                .Where(model => model.Packageid == packageid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeletePackage")
        .WithOpenApi();
    }
}