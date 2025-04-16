using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Domain.Models.LocateYourHome_BuildYourKit;
using OdaWepApi.Infrastructure;

namespace OdaWepApi.API.DomainEndpoints.LocateYourHome_BuildYourKit
{
    public static class AddperrequestEndpoints
    {
        public static void MapAddperrequestEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Addperrequest").WithTags(nameof(Addperrequest));

            // Get all Addperrequests
            group.MapGet("/", async (OdaDbContext db) =>
            {
                return await db.Addperrequests
                .AsNoTracking()
                .OrderBy(a => a.Displayorder)
                .ToListAsync();
            })
           .WithName("GetAllAddperrequests")
           .WithOpenApi();

            // Get Addperrequest by ID
            group.MapGet("/{id}", async Task<Results<Ok<Addperrequest>, NotFound>> (int id, OdaDbContext db) =>
            {
                var addperrequest = await db.Addperrequests.AsNoTracking()
                    .FirstOrDefaultAsync(a => a.Addperrequestid == id);
                return addperrequest is not null
                    ? TypedResults.Ok(addperrequest)
                    : TypedResults.NotFound();
            })
          .WithName("GetAddperrequestById")
          .WithOpenApi();

            // Update Addperrequest
            group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Addperrequest addperrequest, OdaDbContext db) =>
            {
                var existingAddperrequest = await db.Addperrequests.FirstOrDefaultAsync(a => a.Addperrequestid == id);
                if (existingAddperrequest is null)
                {
                    return TypedResults.NotFound();
                }

                existingAddperrequest.Addperrequestname = addperrequest.Addperrequestname;
                existingAddperrequest.Price = addperrequest.Price;
                existingAddperrequest.Description = addperrequest.Description;
                existingAddperrequest.Lastmodifieddatetime = DateTime.UtcNow;

                await db.SaveChangesAsync();
                return TypedResults.Ok();
            })
            .WithName("UpdateAddperrequest")
            .WithOpenApi();

            // Create Addperrequest
            group.MapPost("/", async (Addperrequest addperrequest, OdaDbContext db) =>
            {
                addperrequest.Createddatetime = DateTime.UtcNow;
                addperrequest.Lastmodifieddatetime = addperrequest.Createddatetime;

                db.Addperrequests.Add(addperrequest);
                await db.SaveChangesAsync();
                return TypedResults.Created($"/api/Addperrequest/{addperrequest.Addperrequestid}", addperrequest);
            })
            .WithName("CreateAddperrequest")
            .WithOpenApi();

            // Delete Addperrequest
            group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, OdaDbContext db) =>
            {
                var addperrequest = await db.Addperrequests.FirstOrDefaultAsync(a => a.Addperrequestid == id);
                if (addperrequest is null)
                {
                    return TypedResults.NotFound();
                }

                db.Addperrequests.Remove(addperrequest);
                await db.SaveChangesAsync();
                return TypedResults.Ok();
            })
            .WithName("DeleteAddperrequest")
            .WithOpenApi();
        }
    }
}
