using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Domain.Models;
using OdaWepApi.Infrastructure;

namespace OdaWepApi.Endpoints
{
    public static class PermissionEndpoints
    {
        public static void MapPermissionEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Permission").WithTags(nameof(Permission));

            // Get all Permissions
            group.MapGet("/", async (OdaDbContext db) =>
                await db.Permissions.ToListAsync()
            ).WithName("GetAllPermissions").WithOpenApi();

            // Get Permission by Id
            group.MapGet("/{id}", async Task<Results<Ok<Permission>, NotFound>> (int permissionid, OdaDbContext db) =>
                await db.Permissions.AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Permissionid == permissionid)
                    is Permission permission
                        ? TypedResults.Ok(permission)
                        : TypedResults.NotFound()
            ).WithName("GetPermissionById").WithOpenApi();

            // Update Permission
            group.MapPut("/{id}", async Task<IResult> (int permissionid, Permission permission, OdaDbContext db) =>
            {
                if (!await db.Roles.AnyAsync(r => r.Roleid == permission.Roleid))
                    return TypedResults.BadRequest("Invalid Role ID.");

                permission.Lastmodifieddatetime = DateTime.UtcNow;

                var affected = await db.Permissions
                    .Where(p => p.Permissionid == permissionid)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(p => p.Entityname, permission.Entityname)
                        .SetProperty(p => p.Action, permission.Action)
                        .SetProperty(p => p.Roleid, permission.Roleid)
                        .SetProperty(p => p.Lastmodifieddatetime, permission.Lastmodifieddatetime));

                return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
            }).WithName("UpdatePermission").WithOpenApi();

            // Create Permission
            group.MapPost("/", async (Permission permission, OdaDbContext db) =>
            {
                if (!await db.Roles.AnyAsync(r => r.Roleid == permission.Roleid))
                    return Results.BadRequest("Invalid Role ID.");

                var maxPermissionId = await db.Permissions.MaxAsync(p => (int?)p.Permissionid) ?? 0;
                permission.Permissionid = maxPermissionId + 1;
                permission.Createdatetime = DateTime.UtcNow;
                permission.Lastmodifieddatetime = permission.Createdatetime;

                db.Permissions.Add(permission);
                await db.SaveChangesAsync();
                return TypedResults.Created($"/api/Permission/{permission.Permissionid}", permission);
            }).WithName("CreatePermission").WithOpenApi();

            // Delete Permission
            group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int permissionid, OdaDbContext db) =>
            {
                var affected = await db.Permissions
                    .Where(p => p.Permissionid == permissionid)
                    .ExecuteDeleteAsync();

                return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
            }).WithName("DeletePermission").WithOpenApi();
        }
    }
}