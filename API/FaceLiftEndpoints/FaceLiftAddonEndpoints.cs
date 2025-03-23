using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Domain.Models;
using OdaWepApi.Infrastructure;

namespace OdaWepApi.API.FaceLiftEndpoints
{
    public static class aceLiftAddonEndpoints
    {
        public static void MapFaceLiftAddonEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/FaceLiftAddons").WithTags(nameof(Faceliftaddon));

            // Get all FaceLiftAddonsAddons
            group.MapGet("/", async (OdaDbContext db) =>
            {
                return await db.Faceliftaddons.AsNoTracking().ToListAsync();
            })
            .WithName("GetAllFaceLiftAddons")
            .WithOpenApi();

            // Get grouped by room type
            group.MapGet("/GroupedByRoom", async (OdaDbContext db) =>
             {
                 var grouped = await db.Faceliftaddons
                     .AsNoTracking()
                     .GroupBy(a => a.FaceLiftRoomType)
                     .ToDictionaryAsync(
                         g => g.Key.ToString(),
                         g => g.OrderBy(a => a.Addonid).ToList()
                     );

                 return Results.Ok(grouped);
             })
             .WithName("GetAllAddonGroupedByRoomType")
             .WithOpenApi();

            // Get by ID
            group.MapGet("/{id}", async (int id, OdaDbContext db) =>
            {
                return await db.Faceliftaddons.FindAsync(id)
                    is Faceliftaddon addon
                        ? Results.Ok(addon)
                        : Results.NotFound();
            })
            .WithName("GetFaceLiftAddonById")
            .WithOpenApi();

            // Create
            group.MapPost("/", async (Faceliftaddon addon, OdaDbContext db) =>
            {
                addon.Createddatetime = DateTime.UtcNow;
                db.Faceliftaddons.Add(addon);
                await db.SaveChangesAsync();
                return Results.Created($"/api/FaceLiftAddons/{addon.Addonid}", addon);
            })
            .WithName("CreateFaceLiftAddon")
            .WithOpenApi();

            // Update
            group.MapPut("/{id}", async (int id, Faceliftaddon input, OdaDbContext db) =>
            {
                var existing = await db.Faceliftaddons.FindAsync(id);
                if (existing is null) return Results.NotFound();

                // Update properties
                existing.Addonname = input.Addonname;
                existing.Addongroup = input.Addongroup;
                existing.Price = input.Price;
                existing.Description = input.Description;
                existing.Brand = input.Brand;
                existing.FaceLiftRoomType = input.FaceLiftRoomType;
                existing.Unitormeter = input.Unitormeter;
                existing.Lastmodifieddatetime = DateTime.UtcNow;

                await db.SaveChangesAsync();
                return Results.NoContent();
            })
            .WithName("UpdateFaceLiftAddon")
            .WithOpenApi();

            // Delete
            group.MapDelete("/{id}", async (int id, OdaDbContext db) =>
            {
                var existing = await db.Faceliftaddons.FindAsync(id);
                if (existing is null) return Results.NotFound();

                db.Faceliftaddons.Remove(existing);
                await db.SaveChangesAsync();
                return Results.Ok();
            })
            .WithName("DeleteFaceLiftAddon")
            .WithOpenApi();
        }
    }
}