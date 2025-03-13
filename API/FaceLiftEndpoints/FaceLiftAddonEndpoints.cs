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
        }
    }
}