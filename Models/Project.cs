using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Models;

namespace OdaWepApi.Models;

public partial class Project
{
    [Key]
    public int Projectid { get; set; }

    [Required(ErrorMessage = "Project Name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Project Name must be between 2 and 100 characters.")]
    public string? Projectname { get; set; }

    public string? Location { get; set; }

    public string? Amenities { get; set; }

    public int? Totalunits { get; set; }

    public List<byte[]>? Projectlogo { get; set; }

    public DateTime? Createdatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }

    public virtual ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();
}


public static class ProjectEndpoints
{
    public static void MapProjectEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Project").WithTags(nameof(Project));

        // Get All Projects
        group.MapGet("/", async (OdaDbContext db) =>
        {
            return await db.Projects.ToListAsync();
        })
        .WithName("GetAllProjects")
        .WithOpenApi();

        // Get Project by Id
        group.MapGet("/{id}", async Task<Results<Ok<Project>, NotFound>> (int projectid, OdaDbContext db) =>
        {
            return await db.Projects.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Projectid == projectid)
                is Project model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetProjectById")
        .WithOpenApi();

        // Update Project
        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int projectid, HttpRequest request, OdaDbContext db) =>
        {
            // Deserialize JSON data from the form
            var projectJson = request.Form["project"].ToString();
            var project = JsonSerializer.Deserialize<Project>(projectJson);

            var existingProject = await db.Projects.FindAsync(projectid);
            if (existingProject == null)
            {
                return TypedResults.NotFound();
            }

            // Update properties
            existingProject.Projectname = project?.Projectname;
            existingProject.Location = project?.Location;
            existingProject.Amenities = project?.Amenities;
            existingProject.Totalunits = project?.Totalunits;
            existingProject.Lastmodifieddatetime = DateTime.UtcNow;

            // Update Project Logo if provided
            if (request.Form.Files.Count > 0)
            {
                var logo = request.Form.Files[0];
                if (logo.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await logo.CopyToAsync(memoryStream);
                        existingProject.Projectlogo = new List<byte[]> { memoryStream.ToArray() };
                    }
                }
            }

            await db.SaveChangesAsync();
            return TypedResults.Ok();
        })
 .WithName("UpdateProject")
 .WithOpenApi();



        // Create Project
        group.MapPost("/", async (HttpRequest request, OdaDbContext db) =>
        {
            // Deserialize JSON data from the form
            var projectJson = request.Form["project"].ToString();
            var project = JsonSerializer.Deserialize<Project>(projectJson);

            // Set Project ID to MaxProjectId + 1
            var maxProjectId = await db.Projects.MaxAsync(p => (int?)p.Projectid) ?? 0;
            project.Projectid = maxProjectId + 1;

            // Set CreateDateTime and LastModifiedDateTime
            project.Createdatetime = DateTime.UtcNow;
            project.Lastmodifieddatetime = project.Createdatetime;

            // Handle Project Logo if provided
            if (request.Form.Files.Count > 0)
            {
                var logo = request.Form.Files[0];
                if (logo.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await logo.CopyToAsync(memoryStream);
                        project.Projectlogo = new List<byte[]> { memoryStream.ToArray() };
                    }
                }
            }

            db.Projects.Add(project);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Project/{project.Projectid}", project);
        })
        .WithName("CreateProject")
        .WithOpenApi();


        // Delete Project
        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int projectid, OdaDbContext db) =>
        {
            var affected = await db.Projects
                .Where(model => model.Projectid == projectid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteProject")
        .WithOpenApi();
    }
}