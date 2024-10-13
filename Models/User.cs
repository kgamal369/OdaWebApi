using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Models;

namespace OdaWepApi.Models;

public partial class User
{
    public int Userid { get; set; }

    public string? Username { get; set; }

    public string? Passwordhash { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string? Email { get; set; }

    public string? Phonenumber { get; set; }

    public DateTime? Createdatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }

    public DateTime? Lastlogin { get; set; }

    public int? Roleid { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Role? Role { get; set; }
}


public static class UserEndpoints
{
	public static void MapUserEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/User").WithTags(nameof(User));

        group.MapGet("/", async (OdaDbContext db) =>
        {
            return await db.Users.ToListAsync();
        })
        .WithName("GetAllUsers")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<User>, NotFound>> (int userid, OdaDbContext db) =>
        {
            return await db.Users.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Userid == userid)
                is User model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetUserById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int userid, User user, OdaDbContext db) =>
        {
            var affected = await db.Users
                .Where(model => model.Userid == userid)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Userid, user.Userid)
                  .SetProperty(m => m.Username, user.Username)
                  .SetProperty(m => m.Passwordhash, user.Passwordhash)
                  .SetProperty(m => m.Firstname, user.Firstname)
                  .SetProperty(m => m.Lastname, user.Lastname)
                  .SetProperty(m => m.Email, user.Email)
                  .SetProperty(m => m.Phonenumber, user.Phonenumber)
                  .SetProperty(m => m.Createdatetime, user.Createdatetime)
                  .SetProperty(m => m.Lastmodifieddatetime, user.Lastmodifieddatetime)
                  .SetProperty(m => m.Lastlogin, user.Lastlogin)
                  .SetProperty(m => m.Roleid, user.Roleid)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateUser")
        .WithOpenApi();

        group.MapPost("/", async (User user, OdaDbContext db) =>
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/User/{user.Userid}",user);
        })
        .WithName("CreateUser")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int userid, OdaDbContext db) =>
        {
            var affected = await db.Users
                .Where(model => model.Userid == userid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteUser")
        .WithOpenApi();
    }
}