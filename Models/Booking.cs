using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Models;

namespace OdaWepApi.Models;

public partial class Booking
{
    [Key]
    public int Bookingid { get; set; }

    [Required(ErrorMessage = "Customer Id is required.")]
    public int? Customerid { get; set; }

    [Required(ErrorMessage = "Apartment Id is required.")]
    public int? Apartmentid { get; set; }

    public DateTime? Createdatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }

    [Required(ErrorMessage = "Booking Status is required.")]
    [EnumDataType(typeof(Enum.BookingStatus), ErrorMessage = "Invalid Booking Status.")]
    public string? Bookingstatus { get; set; }

    public int? Userid { get; set; }

    [Required(ErrorMessage = "Total Amount is required.")]
    [Range(10, 10000000, ErrorMessage = "Space must be between 10 and 10M EGP.")]
    public decimal? Totalamount { get; set; }

    public virtual Apartment? Apartment { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual User? User { get; set; }
}


public static class BookingEndpoints
{
    public static void MapBookingEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Booking").WithTags(nameof(Booking));

        // Get all Bookings
        group.MapGet("/", async (OdaDbContext db) =>
        {
            return await db.Bookings.ToListAsync();
        })
        .WithName("GetAllBookings")
        .WithOpenApi();

        // Get Booking by Id
        group.MapGet("/{id}", async Task<Results<Ok<Booking>, NotFound>> (int bookingid, OdaDbContext db) =>
        {
            return await db.Bookings.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Bookingid == bookingid)
                is Booking model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetBookingById")
        .WithOpenApi();


        // Update Booking
        group.MapPut("/{id}", async Task<IResult> (int bookingid, Booking booking, OdaDbContext db) =>
        {
            // Validate Apartment, Customer, and User IDs
            if (!await db.Apartments.AnyAsync(a => a.Apartmentid == booking.Apartmentid))
                return TypedResults.BadRequest("Invalid Apartment ID.");
            if (!await db.Customers.AnyAsync(c => c.Customerid == booking.Customerid))
                return TypedResults.BadRequest("Invalid Customer ID.");
            if (!await db.Users.AnyAsync(u => u.Userid == booking.Userid))
                return TypedResults.BadRequest("Invalid User ID.");

            // Recalculate Total Amount from linked apartment
            var apartment = await db.Apartments
                .Include(a => a.Packages)
                .Include(a => a.Apartmentaddons)
                    .ThenInclude(aa => aa.Addon)
                .FirstOrDefaultAsync(a => a.Apartmentid == booking.Apartmentid);

            if (apartment == null) return TypedResults.BadRequest("Apartment not found.");

            booking.Totalamount = apartment.CalculateApartmentTotalPrice();


            // Update LastModifiedDateTime
            booking.Lastmodifieddatetime = DateTime.UtcNow;


            var affected = await db.Bookings
                .Where(model => model.Bookingid == bookingid)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Bookingid, booking.Bookingid)
                  .SetProperty(m => m.Customerid, booking.Customerid)
                  .SetProperty(m => m.Apartmentid, booking.Apartmentid)
                //  .SetProperty(m => m.Createdatetime, booking.Createdatetime)
                  .SetProperty(m => m.Lastmodifieddatetime, booking.Lastmodifieddatetime)
                  .SetProperty(m => m.Bookingstatus, booking.Bookingstatus)
                  .SetProperty(m => m.Userid, booking.Userid)
                  .SetProperty(m => m.Totalamount, booking.Totalamount)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateBooking")
        .WithOpenApi();

        // Create Booking
        group.MapPost("/", async (Booking booking, OdaDbContext db) =>
        {
            // Validate Apartment, Customer, and User IDs
            if (!await db.Apartments.AnyAsync(a => a.Apartmentid == booking.Apartmentid))
                return Results.BadRequest("Invalid Apartment ID.");
            if (!await db.Customers.AnyAsync(c => c.Customerid == booking.Customerid))
                return Results.BadRequest("Invalid Customer ID.");
            if (!await db.Users.AnyAsync(u => u.Userid == booking.Userid))
                return Results.BadRequest("Invalid User ID.");

            // Set Booking ID to MaxBookingId + 1
            var maxBookingId = await db.Bookings.MaxAsync(b => (int?)b.Bookingid) ?? 0;
            booking.Bookingid = maxBookingId + 1;

            // Set CreateDateTime and LastModifiedDateTime
            booking.Createdatetime = DateTime.UtcNow;
            booking.Lastmodifieddatetime = booking.Createdatetime;

            // Recalculate Total Amount from linked apartment
            var apartment = await db.Apartments
                .Include(a => a.Packages)
                .Include(a => a.Apartmentaddons)
                    .ThenInclude(aa => aa.Addon)
                .FirstOrDefaultAsync(a => a.Apartmentid == booking.Apartmentid);

            if (apartment == null) return Results.BadRequest("Apartment not found.");

            booking.Totalamount = apartment.CalculateApartmentTotalPrice();


            db.Bookings.Add(booking);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Booking/{booking.Bookingid}", booking);
        })
        .WithName("CreateBooking")
        .WithOpenApi();


        // Delete Booking
        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int bookingid, OdaDbContext db) =>
        {
            var affected = await db.Bookings
                .Where(model => model.Bookingid == bookingid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteBooking")
        .WithOpenApi();
    }
}