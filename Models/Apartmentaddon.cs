using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Models;

namespace OdaWepApi.Models;

public partial class Apartmentaddon
{
    [Key]
    public int Apartmentaddonsid { get; set; }

    public int? Apartmentid { get; set; }

    public int? Addonid { get; set; }

    public bool? Availableaddons { get; set; }

    public bool? Assignedaddons { get; set; }

    public int? Maxavailable { get; set; }

    public int? Installedamount { get; set; }

    public DateTime? Createdatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }

    public virtual Addon? Addon { get; set; }

    public virtual Apartment? Apartment { get; set; }
}


public static class ApartmentaddonEndpoints
{
    public static void MapApartmentaddonEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Apartmentaddon").WithTags(nameof(Apartmentaddon));

        group.MapGet("/", async (OdaDbContext db) =>
        {
            return await db.Apartmentaddons.ToListAsync();
        })
        .WithName("GetAllApartmentaddons")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Apartmentaddon>, NotFound>> (int apartmentaddonsid, OdaDbContext db) =>
        {
            return await db.Apartmentaddons.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Apartmentaddonsid == apartmentaddonsid)
                is Apartmentaddon model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetApartmentaddonById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int apartmentaddonsid, Apartmentaddon apartmentaddon, OdaDbContext db) =>
        {
            var affected = await db.Apartmentaddons
                .Where(model => model.Apartmentaddonsid == apartmentaddonsid)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Apartmentaddonsid, apartmentaddon.Apartmentaddonsid)
                  .SetProperty(m => m.Apartmentid, apartmentaddon.Apartmentid)
                  .SetProperty(m => m.Addonid, apartmentaddon.Addonid)
                  .SetProperty(m => m.Availableaddons, apartmentaddon.Availableaddons)
                  .SetProperty(m => m.Assignedaddons, apartmentaddon.Assignedaddons)
                  .SetProperty(m => m.Maxavailable, apartmentaddon.Maxavailable)
                  .SetProperty(m => m.Installedamount, apartmentaddon.Installedamount)
                  .SetProperty(m => m.Createdatetime, apartmentaddon.Createdatetime)
                  .SetProperty(m => m.Lastmodifieddatetime, apartmentaddon.Lastmodifieddatetime)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateApartmentaddon")
        .WithOpenApi();

        group.MapPost("/", async (Apartmentaddon apartmentaddon, OdaDbContext db) =>
        {
            db.Apartmentaddons.Add(apartmentaddon);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Apartmentaddon/{apartmentaddon.Apartmentaddonsid}", apartmentaddon);
        })
        .WithName("CreateApartmentaddon")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int apartmentaddonsid, OdaDbContext db) =>
        {
            var affected = await db.Apartmentaddons
                .Where(model => model.Apartmentaddonsid == apartmentaddonsid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteApartmentaddon")
        .WithOpenApi();
    }
}