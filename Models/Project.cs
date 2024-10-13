using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Models;

namespace OdaWepApi.Models;

public partial class Project
{
    public int Projectid { get; set; }

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
	public static void MapProjectEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Project").WithTags(nameof(Project));

        group.MapGet("/", async (OdaDbContext db) =>
        {
            return await db.Projects.ToListAsync();
        })
        .WithName("GetAllProjects")
        .WithOpenApi();

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

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int projectid, Project project, OdaDbContext db) =>
        {
            var affected = await db.Projects
                .Where(model => model.Projectid == projectid)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Projectid, project.Projectid)
                  .SetProperty(m => m.Projectname, project.Projectname)
                  .SetProperty(m => m.Location, project.Location)
                  .SetProperty(m => m.Amenities, project.Amenities)
                  .SetProperty(m => m.Totalunits, project.Totalunits)
                  .SetProperty(m => m.Projectlogo, project.Projectlogo)
                  .SetProperty(m => m.Createdatetime, project.Createdatetime)
                  .SetProperty(m => m.Lastmodifieddatetime, project.Lastmodifieddatetime)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateProject")
        .WithOpenApi();

        group.MapPost("/", async (Project project, OdaDbContext db) =>
        {
            db.Projects.Add(project);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Project/{project.Projectid}",project);
        })
        .WithName("CreateProject")
        .WithOpenApi();

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