using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Models;

namespace OdaWepApi.Models;

public partial class Permission
{
    public int Permissionid { get; set; }

    public string? Entityname { get; set; }

    public string? Action { get; set; }

    public int? Roleid { get; set; }

    public DateTime? Createdatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }

    public virtual Role? Role { get; set; }
}


public static class PermissionEndpoints
{
	public static void MapPermissionEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Permission").WithTags(nameof(Permission));

        group.MapGet("/", async (OdaDbContext db) =>
        {
            return await db.Permissions.ToListAsync();
        })
        .WithName("GetAllPermissions")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Permission>, NotFound>> (int permissionid, OdaDbContext db) =>
        {
            return await db.Permissions.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Permissionid == permissionid)
                is Permission model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetPermissionById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int permissionid, Permission permission, OdaDbContext db) =>
        {
            var affected = await db.Permissions
                .Where(model => model.Permissionid == permissionid)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Permissionid, permission.Permissionid)
                  .SetProperty(m => m.Entityname, permission.Entityname)
                  .SetProperty(m => m.Action, permission.Action)
                  .SetProperty(m => m.Roleid, permission.Roleid)
                  .SetProperty(m => m.Createdatetime, permission.Createdatetime)
                  .SetProperty(m => m.Lastmodifieddatetime, permission.Lastmodifieddatetime)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdatePermission")
        .WithOpenApi();

        group.MapPost("/", async (Permission permission, OdaDbContext db) =>
        {
            db.Permissions.Add(permission);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Permission/{permission.Permissionid}",permission);
        })
        .WithName("CreatePermission")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int permissionid, OdaDbContext db) =>
        {
            var affected = await db.Permissions
                .Where(model => model.Permissionid == permissionid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeletePermission")
        .WithOpenApi();
    }
}