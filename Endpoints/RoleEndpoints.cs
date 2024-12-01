using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Models;

namespace OdaWepApi.Endpoints
{
    public static class RoleEndpoints
    {
        public static void MapRoleEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Role").WithTags(nameof(Role));

            // Get all Roles
            group.MapGet("/", async (OdaDbContext db) =>
                await db.Roles.ToListAsync()
            ).WithName("GetAllRoles").WithOpenApi();

            // Get Role by Id
            group.MapGet("/{id}", async Task<Results<Ok<Role>, NotFound>> (int roleid, OdaDbContext db) =>
                await db.Roles.AsNoTracking()
                    .FirstOrDefaultAsync(r => r.Roleid == roleid)
                    is Role role
                        ? TypedResults.Ok(role)
                        : TypedResults.NotFound()
            ).WithName("GetRoleById").WithOpenApi();

            // Update Role
            group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int roleid, Role role, OdaDbContext db) =>
            {
                role.Lastmodifieddatetime = DateTime.UtcNow;

                var affected = await db.Roles
                    .Where(r => r.Roleid == roleid)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(r => r.Rolename, role.Rolename)
                        .SetProperty(r => r.Description, role.Description)
                        .SetProperty(r => r.Lastmodifieddatetime, role.Lastmodifieddatetime));

                return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
            }).WithName("UpdateRole").WithOpenApi();

            // Create Role
            group.MapPost("/", async (Role role, OdaDbContext db) =>
            {
                var maxRoleId = await db.Roles.MaxAsync(r => (int?)r.Roleid) ?? 0;
                role.Roleid = maxRoleId + 1;
                role.Createdatetime = DateTime.UtcNow;
                role.Lastmodifieddatetime = role.Createdatetime;

                db.Roles.Add(role);
                await db.SaveChangesAsync();
                return TypedResults.Created($"/api/Role/{role.Roleid}", role);
            }).WithName("CreateRole").WithOpenApi();

            // Delete Role
            group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int roleid, OdaDbContext db) =>
            {
                var affected = await db.Roles
                    .Where(r => r.Roleid == roleid)
                    .ExecuteDeleteAsync();

                return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
            }).WithName("DeleteRole").WithOpenApi();
        }
    }
}