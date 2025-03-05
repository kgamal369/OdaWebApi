using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Domain.Models;
using OdaWepApi.Infrastructure;

namespace OdaWepApi.API.DomainEndpoints
{
    public static class OdaambassadorEndpoints
    {
        public static void MapOdaAmbassadorEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Odaambassador").WithTags(nameof(Odaambassador));

            // Get all Roles
            group.MapGet("/", async (OdaDbContext db) =>
                await db.Odaambassadors.ToListAsync()
            ).WithName("GetAllOdaAmbassadors").WithOpenApi();

            // Create Role
            group.MapPost("/", async (Odaambassador oda, OdaDbContext db) =>
            {
                db.Odaambassadors.Add(oda);
                await db.SaveChangesAsync();
                return TypedResults.Created($"/api/Odaambassador/{oda.Odaambassadorid}", oda);
            }).WithName("CreateOdaAmbassador").WithOpenApi();
        }
    }
}