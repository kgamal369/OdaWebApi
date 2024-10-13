using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Models;

namespace OdaWepApi.Models;

public partial class Package
{
    public int Packageid { get; set; }

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
	public static void MapPackageEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Package").WithTags(nameof(Package));

        group.MapGet("/", async (OdaDbContext db) =>
        {
            return await db.Packages.ToListAsync();
        })
        .WithName("GetAllPackages")
        .WithOpenApi();

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

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int packageid, Package package, OdaDbContext db) =>
        {
            var affected = await db.Packages
                .Where(model => model.Packageid == packageid)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Packageid, package.Packageid)
                  .SetProperty(m => m.Packagename, package.Packagename)
                  .SetProperty(m => m.Apartmentid, package.Apartmentid)
                  .SetProperty(m => m.Price, package.Price)
                  .SetProperty(m => m.Description, package.Description)
                  .SetProperty(m => m.Assignedpackage, package.Assignedpackage)
                  .SetProperty(m => m.Createdatetime, package.Createdatetime)
                  .SetProperty(m => m.Lastmodifieddatetime, package.Lastmodifieddatetime)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdatePackage")
        .WithOpenApi();

        group.MapPost("/", async (Package package, OdaDbContext db) =>
        {
            db.Packages.Add(package);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Package/{package.Packageid}",package);
        })
        .WithName("CreatePackage")
        .WithOpenApi();

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