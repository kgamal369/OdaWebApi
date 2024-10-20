using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Models;

namespace OdaWepApi.Models;

public partial class User
{
    [Key]
    public int Userid { get; set; }

    [Required(ErrorMessage = "User Name is required.")]
    [StringLength(20, MinimumLength = 2, ErrorMessage = "User Name must be between 2 and 20 characters.")]
    public string? Username { get; set; }

    [Required]
    public string? Passwordhash { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 2, ErrorMessage = "User Name must be between 2 and 20 characters.")]
    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "User Name must be between 2 and 20 characters.")]
    public string? Email { get; set; }

    public string? Phonenumber { get; set; }

    public DateTime? Createdatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }

    public DateTime? Lastlogin { get; set; }

    [Required]
    public int? Roleid { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Role? Role { get; set; }
}


public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/User").WithTags(nameof(User));

        // Get All Users
        group.MapGet("/", async (OdaDbContext db) =>
        {
            return await db.Users.ToListAsync();
        })
        .WithName("GetAllUsers")
        .WithOpenApi();

        // Get User by Id
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

        // Update User
        group.MapPut("/{id}", async Task<IResult> (int userid, User user, OdaDbContext db) =>
        {
            // Validate Role ID
            if (!await db.Roles.AnyAsync(r => r.Roleid == user.Roleid))
                return TypedResults.BadRequest("Invalid Role ID.");

            var existingUser = await db.Users.FindAsync(userid);
            if (existingUser == null)
                return TypedResults.NotFound();

            // Update properties
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
        })
        .WithName("UpdateUser")
        .WithOpenApi();

        // Create User
        group.MapPost("/", async (User user, OdaDbContext db) =>
        {
            // Validate Role ID
            if (!await db.Roles.AnyAsync(r => r.Roleid == user.Roleid))
                return Results.BadRequest("Invalid Role ID.");

            // Set User ID to MaxUserid + 1
            var maxUserid = await db.Users.MaxAsync(u => (int?)u.Userid) ?? 0;
            user.Userid = maxUserid + 1;

            // Set CreateDateTime and LastModifiedDateTime
            user.Createdatetime = DateTime.UtcNow;
            user.Lastmodifieddatetime = user.Createdatetime;

            db.Users.Add(user);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/User/{user.Userid}", user);
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