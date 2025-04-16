using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Domain.Models.LocateYourHome_BuildYourKit;
using OdaWepApi.Infrastructure;

namespace OdaWepApi.API.DomainEndpoints.LocateYourHome_BuildYourKit
{
    public static class AddonEndpoints
    {
        public static void MapAddonEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Addon").WithTags(nameof(Addon));

            // Get all Addons
            group.MapGet("/", async (OdaDbContext db) =>
            {
                return await db.Addons
                .AsNoTracking()
                .OrderBy(a => a.Displayorder)
                .ToListAsync();
            })
            .WithName("GetAllAddons")
            .WithOpenApi();

            // Get Addon by ID
            group.MapGet("/{id}", async Task<Results<Ok<Addon>, NotFound>> (int id, OdaDbContext db) =>
            {
                var addon = await db.Addons.AsNoTracking()
                    .FirstOrDefaultAsync(a => a.Addonid == id);
                return addon is not null
                    ? TypedResults.Ok(addon)
                    : TypedResults.NotFound();
            })
            .WithName("GetAddonById")
            .WithOpenApi();

            // Update Addon
            group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Addon addon, OdaDbContext db) =>
            {
                var existingAddon = await db.Addons.FirstOrDefaultAsync(a => a.Addonid == id);
                if (existingAddon is null)
                {
                    return TypedResults.NotFound();
                }

                existingAddon.Addonname = addon.Addonname;
                existingAddon.Addongroup = addon.Addongroup;
                existingAddon.Price = addon.Price;
                existingAddon.Description = addon.Description;
                existingAddon.Brand = addon.Brand;
                existingAddon.Unitormeter = addon.Unitormeter;
                existingAddon.Lastmodifieddatetime = DateTime.UtcNow;

                await db.SaveChangesAsync();
                return TypedResults.Ok();
            })
            .WithName("UpdateAddon")
            .WithOpenApi();

            //// Create Addon
            //group.MapPost("/", async (Addon addon, OdaDbContext db) =>
            //{
            //    addon.Createddatetime = DateTime.UtcNow;
            //    addon.Lastmodifieddatetime = addon.Createddatetime;

            //    // Enum validation: Ensure a valid UnitOrMeterType is passed
            //    if (!Enum.IsDefined(typeof(UnitOrMeterType), addon.Unitormeter))
            //    {
            //        return TypedResults.BadRequest($"Invalid value for UnitOrMeter. Valid values are: {string.Join(", ", Enum.GetNames(typeof(UnitOrMeterType)))}");
            //    }

            //    db.Addons.Add(addon);
            //    await db.SaveChangesAsync();
            //    return TypedResults.Created($"/api/Addon/{addon.Addonid}", addon);
            //})
            //.WithName("CreateAddon")
            //.WithOpenApi();

            // Delete Addon
            group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, OdaDbContext db) =>
            {
                var addon = await db.Addons.FirstOrDefaultAsync(a => a.Addonid == id);
                if (addon is null)
                {
                    return TypedResults.NotFound();
                }

                db.Addons.Remove(addon);
                await db.SaveChangesAsync();
                return TypedResults.Ok();
            })
            .WithName("DeleteAddon")
            .WithOpenApi();
        }
    }
}
