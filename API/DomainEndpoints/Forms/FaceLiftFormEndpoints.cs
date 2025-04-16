using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.DataFlows;
using OdaWepApi.Domain.Models.Forms;
using OdaWepApi.Infrastructure;

namespace OdaWepApi.API.DomainEndpoints.Forms
{
    public static class FaceliftformEndpoints
    {
        public static void MapFaceLiftFormEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/faceliftform").WithTags(nameof(Faceliftform));
            // Get all FaceLift Forms
            group.MapGet("/", async (OdaDbContext db) =>
                await db.Faceliftforms.ToListAsync()
            ).WithName("GetAllFaceliftForms").WithOpenApi();

            // Create FaceLift Form
            group.MapPost("/", async Task<IResult> (Faceliftform faceliftform, OdaDbContext db) =>
           {
               db.Faceliftforms.Add(faceliftform);
               await db.SaveChangesAsync();
               return TypedResults.Created($"/api/faceliftform/{faceliftform.FaceLiftFormId}", faceliftform);
           })
           .WithName("CreateFaceliftForm")
           .WithOpenApi();
        }
    }
}