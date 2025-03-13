using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Domain.Models;
using OdaWepApi.Infrastructure;

namespace OdaWepApi.API.FaceLiftEndpoints
{
    public static class FaceLiftroomEndpoints
    {
        public static void MapFaceLiftroomEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/FaceLiftRoom").WithTags(nameof(Faceliftroom));

            // Get all Apartments
            group.MapGet("/", async (OdaDbContext db) =>
            {
                return await db.Faceliftrooms
                    .Include(a => a.Automation)
                    .AsNoTracking()
                    .ToListAsync();
            })
            .WithName("GetAllFaceLiftRooms")
            .WithOpenApi();
        }
    }
}