using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Domain.Models;
using OdaWepApi.Infrastructure;

namespace OdaWepApi.Endpoints
{
    public static class AddonEndpoints
    {
        public static void MapAddonEndpoints(this IEndpointRouteBuilder routes)
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
                addon.Lastmodifieddatetime = DateTime.UtcNow;

                var affected = await db.Addons
                    .Where(model => model.Addonid == addonid)
                    .ExecuteUpdateAsync(setters => setters
                      .SetProperty(m => m.Addonid, addon.Addonid)
                      .SetProperty(m => m.Addonname, addon.Addonname)
                      .SetProperty(m => m.Addontype, addon.Addontype)
                      .SetProperty(m => m.Priceperunit, addon.Priceperunit)
                      .SetProperty(m => m.Description, addon.Description)
                      .SetProperty(m => m.Brand, addon.Brand)
                      .SetProperty(m => m.Lastmodifieddatetime, addon.Lastmodifieddatetime));
                return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
            })
            .WithName("UpdateAddon")
            .WithOpenApi();

            group.MapPost("/", async (Addon addon, OdaDbContext db) =>
            {
                var maxAddonId = await db.Addons.MaxAsync(a => (int?)a.Addonid) ?? 0;
                addon.Addonid = maxAddonId + 1;
                addon.Createddatetime = DateTime.UtcNow;
                addon.Lastmodifieddatetime = addon.Createddatetime;

                db.Addons.Add(addon);
                await db.SaveChangesAsync();
                return TypedResults.Created($"/api/Addon/{addon.Addonid}", addon);
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
}
