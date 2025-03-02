using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using OdaWepApi.Domain.Enums;
using OdaWepApi.Domain.Models;
using OdaWepApi.Infrastructure;
using System.Threading;

namespace OdaWepApi.API.DomainEndpoints
{
    public static class ProjectEndpoints
    {
        public static void MapProjectEndpoints(this IEndpointRouteBuilder routes)
        {
            //     var group = routes.MapGroup("/api/Project").WithTags(nameof(Project));

            //     // Get All Projects
            //     group.MapGet("/", async (OdaDbContext db) =>
            //         await db.Projects.ToListAsync()
            //     ).WithName("GetAllProjects").WithOpenApi();

            //     // Get All Project Names
            //     group.MapGet("/names", async (OdaDbContext db) =>
            //         await db.Projects
            //             .Select(p => p.Projectname)
            //             .Where(name => name != null)
            //             .ToListAsync()
            //     ).WithName("GetAllProjectNames").WithOpenApi();


            //     // Get All Available Apartment Spaces for a Selected Project Name
            //     group.MapGet("/by-name/{projectName}/available-apartment-spaces", async Task<Results<Ok<List<decimal>>, NotFound>> (string projectName, OdaDbContext db) =>
            //     {
            //         // Find the project by Id
            //         var project = await db.Projects
            //             .Include(p => p.Apartments) // Include Apartments to access them
            //             .AsNoTracking()
            //             .FirstOrDefaultAsync(p => p.Projectname == projectName);

            //         // Return NotFound if the project does not exist
            //         if (project == null)
            //         {
            //             return TypedResults.NotFound();
            //         }

            //         // Get available apartment spaces (adjust the status filter as necessary)
            //         var availableSpaces = project.Apartments
            //             .Where(a => a.Apartmentstatus == Apartmentstatus.ForSale) // Include other statuses if needed
            //             .Select(a => a.Apartmentspace ?? 0) // Ensure non-null values
            //                 .ToList();

            //         return TypedResults.Ok(availableSpaces);
            //     }).WithName("GetAvailableApartmentSpacesByProjectName").WithOpenApi();


            //     // Get All Available Apartment Spaces for a Selected Project ID
            //     group.MapGet("/by-id/{projectId:int}/available-apartment-spaces", async Task<Results<Ok<List<object>>, NotFound>> (int projectId, OdaDbContext db) =>
            //     {
            //         // Find the project by Id
            //         var project = await db.Projects
            //             .Include(p => p.Apartments) // Include Apartments to access them
            //             .AsNoTracking()
            //             .FirstOrDefaultAsync(p => p.Projectid == projectId);

            //         // Return NotFound if the project does not exist
            //         if (project == null)
            //         {
            //             return TypedResults.NotFound();
            //         }

            //         // Get available apartments with space not null and greater than zero
            //         var availableApartments = project.Apartments
            //             .Where(a => a.Apartmentstatus == Apartmentstatus.ForSale && a.Apartmentspace.HasValue && a.Apartmentspace.Value > 0) // Filter out null and zero spaces
            //             .Select(a => new { a.Apartmentid, Apartmentspace = a.Apartmentspace.Value }) // Ensure non-null value
            //             .ToList();

            //         return availableApartments.Any()
            //             ? TypedResults.Ok(availableApartments.Cast<object>().ToList())
            //             : TypedResults.NotFound();
            //     })
            //     .WithName("GetAvailableApartmentSpacesByProjectId")
            //     .WithOpenApi();

            //     // Get Project by Id
            //     group.MapGet("/{id}", async Task<Results<Ok<Project>, NotFound>> (int projectid, OdaDbContext db) =>
            //         await db.Projects.AsNoTracking()
            //             .FirstOrDefaultAsync(p => p.Projectid == projectid)
            //             is Project project
            //                 ? TypedResults.Ok(project)
            //                 : TypedResults.NotFound()
            //     ).WithName("GetProjectById").WithOpenApi();


            //     // Update Project
            //     group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int projectid, HttpRequest request, OdaDbContext db) =>
            //     {
            //         var projectJson = request.Form["project"].ToString();
            //         var project = JsonSerializer.Deserialize<Project>(projectJson);

            //         var existingProject = await db.Projects.FindAsync(projectid);
            //         if (existingProject == null)
            //             return TypedResults.NotFound();

            //         // Update properties
            //         existingProject.Projectname = project?.Projectname;
            //         existingProject.Location = project?.Location;
            //         existingProject.Amenities = project?.Amenities;
            //         existingProject.Totalunits = project?.Totalunits;
            //         existingProject.Lastmodifieddatetime = DateTime.UtcNow;

            //         // Handle project logo if provided
            //         if (request.Form.Files.Count > 0)
            //         {
            //             var logo = request.Form.Files[0];
            //             if (logo.Length > 0)
            //             {
            //                 using var memoryStream = new MemoryStream();
            //                 await logo.CopyToAsync(memoryStream);
            //                 //    existingProject.Projectlogo = new List<byte[]> { memoryStream.ToArray() };
            //             }
            //         }

            //         await db.SaveChangesAsync();
            //         return TypedResults.Ok();
            //     }).WithName("UpdateProject").WithOpenApi();


            //     // Create Project
            //     group.MapPost("/", async (HttpRequest request, OdaDbContext db) =>
            //     {
            //         var projectJson = request.Form["project"].ToString();
            //         var project = JsonSerializer.Deserialize<Project>(projectJson);

            //         var maxProjectId = await db.Projects.MaxAsync(p => (int?)p.Projectid) ?? 0;
            //         project.Projectid = maxProjectId + 1;
            //         project.Createdatetime = DateTime.UtcNow;
            //         project.Lastmodifieddatetime = project.Createdatetime;

            //         if (request.Form.Files.Count > 0)
            //         {
            //             var logo = request.Form.Files[0];
            //             if (logo.Length > 0)
            //             {
            //                 using var memoryStream = new MemoryStream();
            //                 await logo.CopyToAsync(memoryStream);
            //                 //    project.Projectlogo = new List<byte[]> { memoryStream.ToArray() };
            //             }
            //         }

            //         db.Projects.Add(project);
            //         await db.SaveChangesAsync();
            //         return TypedResults.Created($"/api/Project/{project.Projectid}", project);
            //     }).WithName("CreateProject").WithOpenApi();


            //     // Delete Project
            //     group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int projectid, OdaDbContext db) =>
            //     {
            //         var affected = await db.Projects
            //             .Where(p => p.Projectid == projectid)
            //             .ExecuteDeleteAsync();

            //         return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
            //     }).WithName("DeleteProject").WithOpenApi();
            // 
        }
    }
}