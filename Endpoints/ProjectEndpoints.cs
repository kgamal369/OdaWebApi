using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using OdaWepApi.Infrastructure;
using OdaWepApi.Domain.Models;

namespace OdaWepApi.Endpoints
{
    public static class ProjectEndpoints
    {
        public static void MapProjectEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Project").WithTags(nameof(Project));

            // Get All Projects
            group.MapGet("/", async (OdaDbContext db) =>
                await db.Projects.ToListAsync()
            ).WithName("GetAllProjects").WithOpenApi();

            // Get Project by Id
            group.MapGet("/{id}", async Task<Results<Ok<Project>, NotFound>> (int projectid, OdaDbContext db) =>
                await db.Projects.AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Projectid == projectid)
                    is Project project
                        ? TypedResults.Ok(project)
                        : TypedResults.NotFound()
            ).WithName("GetProjectById").WithOpenApi();

            // Update Project
            group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int projectid, HttpRequest request, OdaDbContext db) =>
            {
                var projectJson = request.Form["project"].ToString();
                var project = JsonSerializer.Deserialize<Project>(projectJson);

                var existingProject = await db.Projects.FindAsync(projectid);
                if (existingProject == null)
                    return TypedResults.NotFound();

                // Update properties
                existingProject.Projectname = project?.Projectname;
                existingProject.Location = project?.Location;
                existingProject.Amenities = project?.Amenities;
                existingProject.Totalunits = project?.Totalunits;
                existingProject.Lastmodifieddatetime = DateTime.UtcNow;

                // Handle project logo if provided
                if (request.Form.Files.Count > 0)
                {
                    var logo = request.Form.Files[0];
                    if (logo.Length > 0)
                    {
                        using var memoryStream = new MemoryStream();
                        await logo.CopyToAsync(memoryStream);
                        existingProject.Projectlogo = new List<byte[]> { memoryStream.ToArray() };
                    }
                }

                await db.SaveChangesAsync();
                return TypedResults.Ok();
            }).WithName("UpdateProject").WithOpenApi();

            // Create Project
            group.MapPost("/", async (HttpRequest request, OdaDbContext db) =>
            {
                var projectJson = request.Form["project"].ToString();
                var project = JsonSerializer.Deserialize<Project>(projectJson);

                var maxProjectId = await db.Projects.MaxAsync(p => (int?)p.Projectid) ?? 0;
                project.Projectid = maxProjectId + 1;
                project.Createdatetime = DateTime.UtcNow;
                project.Lastmodifieddatetime = project.Createdatetime;

                if (request.Form.Files.Count > 0)
                {
                    var logo = request.Form.Files[0];
                    if (logo.Length > 0)
                    {
                        using var memoryStream = new MemoryStream();
                        await logo.CopyToAsync(memoryStream);
                        project.Projectlogo = new List<byte[]> { memoryStream.ToArray() };
                    }
                }

                db.Projects.Add(project);
                await db.SaveChangesAsync();
                return TypedResults.Created($"/api/Project/{project.Projectid}", project);
            }).WithName("CreateProject").WithOpenApi();

            // Delete Project
            group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int projectid, OdaDbContext db) =>
            {
                var affected = await db.Projects
                    .Where(p => p.Projectid == projectid)
                    .ExecuteDeleteAsync();

                return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
            }).WithName("DeleteProject").WithOpenApi();
        }
    }
}