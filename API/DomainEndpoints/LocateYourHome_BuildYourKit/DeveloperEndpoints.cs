using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Domain.Models.LocateYourHome_BuildYourKit;
using OdaWepApi.Infrastructure;

namespace OdaWepApi.API.DomainEndpoints.LocateYourHome_BuildYourKit
{
    public static class DeveloperEndpoints
    {
        public static void MapDeveloperEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Developer").WithTags(nameof(Developer));

            // 1. Get All Developers
            group.MapGet("/", async (OdaDbContext db) =>
            {
                return await db.Developers.AsNoTracking().ToListAsync();
            })
            .WithName("GetAllDevelopers")
            .WithOpenApi();

            // 2. Get Developer by ID
            group.MapGet("/{id}", async Task<Results<Ok<Developer>, NotFound>> (int id, OdaDbContext db) =>
            {
                var developer = await db.Developers.AsNoTracking()
                    .FirstOrDefaultAsync(d => d.Developerid == id);
                return developer is not null
                    ? TypedResults.Ok(developer)
                    : TypedResults.NotFound();
            })
            .WithName("GetDeveloperById")
            .WithOpenApi();

            // 3. Create a Developer
            group.MapPost("/", async Task<Results<Created<Developer>, BadRequest>> (Developer developer, OdaDbContext db) =>
            {
                developer.Createdatetime = DateTime.UtcNow;
                developer.Lastmodifieddatetime = developer.Createdatetime;

                db.Developers.Add(developer);
                await db.SaveChangesAsync();

                return TypedResults.Created($"/api/Developer/{developer.Developerid}", developer);
            })
            .WithName("CreateDeveloper")
            .WithOpenApi();

            // 4. Update a Developer
            group.MapPut("/{id}", async Task<Results<Ok, NotFound, BadRequest>> (int id, Developer updatedDeveloper, OdaDbContext db) =>
            {
                var existingDeveloper = await db.Developers.FindAsync(id);
                if (existingDeveloper is null)
                    return TypedResults.NotFound();

                // Update properties
                existingDeveloper.Developername = updatedDeveloper.Developername;
                existingDeveloper.Description = updatedDeveloper.Description;
                existingDeveloper.Developerlogo = updatedDeveloper.Developerlogo;
                existingDeveloper.Lastmodifieddatetime = DateTime.UtcNow;

                await db.SaveChangesAsync();
                return TypedResults.Ok();
            })
            .WithName("UpdateDeveloper")
            .WithOpenApi();

            // 5. Delete a Developer
            group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, OdaDbContext db) =>
            {
                var developer = await db.Developers.FindAsync(id);
                if (developer is null)
                    return TypedResults.NotFound();

                db.Developers.Remove(developer);
                await db.SaveChangesAsync();

                return TypedResults.Ok();
            })
            .WithName("DeleteDeveloper")
            .WithOpenApi();

            // 6. Get All Project Names for a Developer
            group.MapGet("/{id}/ProjectsIDAndNames", async Task<Results<Ok<List<object>>, NotFound>> (int id, OdaDbContext db) =>
            {
                var projectNames = await db.Projects
                    .Where(p => p.Developerid == id)
                    .Select(p => new { p.Projectid, p.Projectname })
                    .ToListAsync();

                return projectNames.Any()
                     ? TypedResults.Ok(projectNames.Cast<object>().ToList())
                    : TypedResults.NotFound();
            })
            .WithName("GetProjectsByDeveloper")
            .WithOpenApi();
        }
    }
}
