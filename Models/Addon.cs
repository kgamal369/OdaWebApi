using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Models;

namespace OdaWepApi.Models;

public partial class Addon
{
    public int Addonid { get; set; }

    [Required(ErrorMessage = "Addon Name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "AddOn Name must be between 2 and 100 characters.")]
    public string? Addonname { get; set; }

    [Required(ErrorMessage = "AddOn Type is required.")]
    [EnumDataType(typeof(Enum.AddOnType), ErrorMessage = "Invalid AddOn Type.")]
    public string? Addontype { get; set; }

    [Required(ErrorMessage = "Price per unit is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price per unit must be greater than 0.")]
    public decimal? Priceperunit { get; set; }

    public string? Description { get; set; }

    public string? Brand { get; set; }

    public DateTime? Createddatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }

    public virtual ICollection<Apartmentaddon> Apartmentaddons { get; set; } = new List<Apartmentaddon>();
}


public static class AddonEndpoints
{
	public static void MapAddonEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Addon").WithTags(nameof(Addon));

        group.MapGet("/", async (OdaDbContext db) =>
        {
            return await db.Addons.ToListAsync();
        })
        .WithName("GetAllAddons")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Addon>, NotFound>> (int addonid, OdaDbContext db) =>
        {
            return await db.Addons.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Addonid == addonid)
                is Addon model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetAddonById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int addonid, Addon addon, OdaDbContext db) =>
        {
            var affected = await db.Addons
                .Where(model => model.Addonid == addonid)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Addonid, addon.Addonid)
                  .SetProperty(m => m.Addonname, addon.Addonname)
                  .SetProperty(m => m.Addontype, addon.Addontype)
                  .SetProperty(m => m.Priceperunit, addon.Priceperunit)
                  .SetProperty(m => m.Description, addon.Description)
                  .SetProperty(m => m.Brand, addon.Brand)
                  .SetProperty(m => m.Createddatetime, addon.Createddatetime)
                  .SetProperty(m => m.Lastmodifieddatetime, addon.Lastmodifieddatetime)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateAddon")
        .WithOpenApi();

        group.MapPost("/", async (Addon addon, OdaDbContext db) =>
        {
            db.Addons.Add(addon);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Addon/{addon.Addonid}",addon);
        })
        .WithName("CreateAddon")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int addonid, OdaDbContext db) =>
        {
            var affected = await db.Addons
                .Where(model => model.Addonid == addonid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteAddon")
        .WithOpenApi();
    }
}