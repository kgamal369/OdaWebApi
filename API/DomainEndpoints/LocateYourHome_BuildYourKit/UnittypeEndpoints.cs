using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Domain.Models.LocateYourHome_BuildYourKit;
using OdaWepApi.Infrastructure;


namespace OdaWepApi.API.DomainEndpoints.LocateYourHome_BuildYourKit
{
    public static class UnittypeEndpoints
    {
        public static void MapUnittypeEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/UnitType").WithTags(nameof(Unittype));

            //1.Get All Unit Types
            group.MapGet("/", async (OdaDbContext db) =>
            {
                return await db.Unittypes.AsNoTracking().ToListAsync();
            })
            .WithName("GetAllUnittypes")
            .WithOpenApi();

            // 2. Get Unit Types by ID
            group.MapGet("/{id}", async Task<Results<Ok<Unittype>, NotFound>> (int id, OdaDbContext db) =>
            {
                // Find the project by Id
                var unittypes = await db.Unittypes.AsNoTracking()
                    .FirstOrDefaultAsync(d => d.Unittypeid == id);
                return unittypes is not null
                    ? TypedResults.Ok(unittypes)
                    : TypedResults.NotFound();
            })
            .WithName("GetUnittypesById")
            .WithOpenApi();
        }
    }
}
