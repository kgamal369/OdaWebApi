using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Models;

namespace OdaWepApi.Models;

public partial class Permission
{
    [Key]
    public int Permissionid { get; set; }

    [Required(ErrorMessage = "Entities Name is required.")]
    [EnumDataType(typeof(Enum.EntitiesNames), ErrorMessage = "Invalid Entities.")]

    public string? Entityname { get; set; }

    [Required(ErrorMessage = "Permission Actions is required.")]
    [EnumDataType(typeof(Enum.PermissionActions), ErrorMessage = "Invalid Actions.")]

    public string? Action { get; set; }

    [Required]
    public int? Roleid { get; set; }

    public DateTime? Createdatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }

    public virtual Role? Role { get; set; }
}


public static class PermissionEndpoints
{
    public static void MapPermissionEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Permission").WithTags(nameof(Permission));

        //Get All
        group.MapGet("/", async (OdaDbContext db) =>
        {
            return await db.Permissions.ToListAsync();
        })
        .WithName("GetAllPermissions")
        .WithOpenApi();

        //Get By ID
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


        //Update Permssion
        group.MapPut("/{id}", async Task<IResult> (int permissionid, Permission permission, OdaDbContext db) =>
        {
            // Validate Role ID
            if (!await db.Roles.AnyAsync(r => r.Roleid == permission.Roleid))
                return TypedResults.BadRequest("Invalid Role ID.");

            // Update LastModifiedDateTime
            permission.Lastmodifieddatetime = DateTime.UtcNow;

            var affected = await db.Permissions
                .Where(model => model.Permissionid == permissionid)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Permissionid, permission.Permissionid)
                  .SetProperty(m => m.Entityname, permission.Entityname)
                  .SetProperty(m => m.Action, permission.Action)
                  .SetProperty(m => m.Roleid, permission.Roleid)
                  .SetProperty(m => m.Lastmodifieddatetime, permission.Lastmodifieddatetime)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdatePermission")
        .WithOpenApi();

        //Created Permssion
        group.MapPost("/", async (Permission permission, OdaDbContext db) =>
        {
            // Validate Role ID
            if (!await db.Roles.AnyAsync(r => r.Roleid == permission.Roleid))
                return Results.BadRequest("Invalid Role ID.");

            // Set Permission ID to MaxPermissionId + 1
            var maxPermissionId = await db.Permissions.MaxAsync(p => (int?)p.Permissionid) ?? 0;
            permission.Permissionid = maxPermissionId + 1;

            // Set CreateDateTime and LastModifiedDateTime
            permission.Createdatetime = DateTime.UtcNow;
            permission.Lastmodifieddatetime = permission.Createdatetime;

            db.Permissions.Add(permission);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Permission/{permission.Permissionid}", permission);
        })
        .WithName("CreatePermission")
        .WithOpenApi();

        //Delete Persmission
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