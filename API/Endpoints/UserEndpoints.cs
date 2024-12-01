using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Domain.Models;
using OdaWepApi.Infrastructure;

namespace OdaWepApi.API.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/User").WithTags(nameof(User));

            // Get all Users
            group.MapGet("/", async (OdaDbContext db) =>
                await db.Users.ToListAsync()
            ).WithName("GetAllUsers").WithOpenApi();

            // Get User by Id
            group.MapGet("/{id}", async Task<Results<Ok<User>, NotFound>> (int userid, OdaDbContext db) =>
                await db.Users.AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Userid == userid)
                    is User user
                        ? TypedResults.Ok(user)
                        : TypedResults.NotFound()
            ).WithName("GetUserById").WithOpenApi();

            // Update User
            group.MapPut("/{id}", async Task<IResult> (int userid, User user, OdaDbContext db) =>
            {
                if (!await db.Roles.AnyAsync(r => r.Roleid == user.Roleid))
                    return TypedResults.BadRequest("Invalid Role ID.");

                var existingUser = await db.Users.FindAsync(userid);
                if (existingUser == null)
                    return TypedResults.NotFound();

                existingUser.Username = user.Username;
                existingUser.Passwordhash = user.Passwordhash;
                existingUser.Firstname = user.Firstname;
                existingUser.Lastname = user.Lastname;
                existingUser.Email = user.Email;
                existingUser.Phonenumber = user.Phonenumber;
                existingUser.Roleid = user.Roleid;
                existingUser.Lastmodifieddatetime = DateTime.UtcNow;

                await db.SaveChangesAsync();
                return TypedResults.Ok();
            }).WithName("UpdateUser").WithOpenApi();

            // Create User
            group.MapPost("/", async (User user, OdaDbContext db) =>
            {
                if (!await db.Roles.AnyAsync(r => r.Roleid == user.Roleid))
                    return Results.BadRequest("Invalid Role ID.");

                var maxUserid = await db.Users.MaxAsync(u => (int?)u.Userid) ?? 0;
                user.Userid = maxUserid + 1;
                user.Createdatetime = DateTime.UtcNow;
                user.Lastmodifieddatetime = user.Createdatetime;

                db.Users.Add(user);
                await db.SaveChangesAsync();
                return TypedResults.Created($"/api/User/{user.Userid}", user);
            }).WithName("CreateUser").WithOpenApi();

            // Delete User
            group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int userid, OdaDbContext db) =>
            {
                var affected = await db.Users
                    .Where(u => u.Userid == userid)
                    .ExecuteDeleteAsync();

                return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
            }).WithName("DeleteUser").WithOpenApi();
        }
    }
}