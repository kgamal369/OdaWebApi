using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Domain.Models;
using OdaWepApi.Infrastructure;

namespace OdaWepApi.API.FaceLiftEndpoints
{
    public static class FaceLiftAddperrequestEndpoints
    {
        public static void MapFaceLiftAddperrequestEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/FaceLiftAddperrequest").WithTags(nameof(Faceliftaddperrequest));

            // Get all FaceLiftAddonsAddons
            group.MapGet("/", async (OdaDbContext db) =>
            {
                return await db.Faceliftaddperrequests
                .AsNoTracking()
                .OrderBy(a => a.Displayorder)
                .ToListAsync();
            })
            .WithName("GetAllFaceLiftAddperrequest")
            .WithOpenApi();
        }
    }
}