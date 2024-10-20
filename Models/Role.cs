using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Models;

namespace OdaWepApi.Models;

public partial class Role
{
    public int Roleid { get; set; }

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

        group.MapGet("/", async (OdaDbContext db) =>
        {
            return await db.Roles.ToListAsync();
        })
        .WithName("GetAllRoles")
        .WithOpenApi();

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

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int roleid, Role role, OdaDbContext db) =>
        {
            var affected = await db.Roles
                .Where(model => model.Roleid == roleid)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Roleid, role.Roleid)
                  .SetProperty(m => m.Rolename, role.Rolename)
                  .SetProperty(m => m.Description, role.Description)
                  .SetProperty(m => m.Createdatetime, role.Createdatetime)
                  .SetProperty(m => m.Lastmodifieddatetime, role.Lastmodifieddatetime)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateRole")
        .WithOpenApi();

        group.MapPost("/", async (Role role, OdaDbContext db) =>
        {
            db.Roles.Add(role);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Role/{role.Roleid}", role);
        })
        .WithName("CreateRole")
        .WithOpenApi();

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