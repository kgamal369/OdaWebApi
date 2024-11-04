using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Models;

namespace OdaWepApi.Models;

public partial class Role
{
    [Key]
    public int Roleid { get; set; }

    [Required(ErrorMessage = "Role Name is required.")]
    [StringLength(30, MinimumLength = 2, ErrorMessage = "Role Name must be between 2 and 20 characters.")]

    public string? Rolename { get; set; }

    public string? Description { get; set; }

    public DateTime? Createdatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}


public static class RoleEndpoints
{
    public static void MapRoleEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Role").WithTags(nameof(Role));

        //Get ALL
        group.MapGet("/", async (OdaDbContext db) =>
        {
            return await db.Roles.ToListAsync();
        })
        .WithName("GetAllRoles")
        .WithOpenApi();

        //Get By ID
        group.MapGet("/{id}", async Task<Results<Ok<Role>, NotFound>> (int roleid, OdaDbContext db) =>


        {
            return await db.Roles.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Roleid == roleid)
                is Role model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetRoleById")
        .WithOpenApi();

        //Update Role
        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int roleid, Role role, OdaDbContext db) =>
        {
            // Update LastModifiedDateTime
            role.Lastmodifieddatetime = DateTime.UtcNow;

            var affected = await db.Roles
                .Where(model => model.Roleid == roleid)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Roleid, role.Roleid)
                  .SetProperty(m => m.Rolename, role.Rolename)
                  .SetProperty(m => m.Description, role.Description)
                  .SetProperty(m => m.Lastmodifieddatetime, role.Lastmodifieddatetime)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateRole")
        .WithOpenApi();


        //Create
        group.MapPost("/", async (Role role, OdaDbContext db) =>
        {
            // Set Role ID to MaxRoleId + 1
            var maxRoleId = await db.Roles.MaxAsync(r => (int?)r.Roleid) ?? 0;
            role.Roleid = maxRoleId + 1;

            // Set CreateDateTime and LastModifiedDateTime
            role.Createdatetime = DateTime.UtcNow;
            role.Lastmodifieddatetime = role.Createdatetime;

            db.Roles.Add(role);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Role/{role.Roleid}", role);
        })
        .WithName("CreateRole")
        .WithOpenApi();

        //Remove
        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int roleid, OdaDbContext db) =>
        {
            var affected = await db.Roles
                .Where(model => model.Roleid == roleid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteRole")
        .WithOpenApi();
    }
}