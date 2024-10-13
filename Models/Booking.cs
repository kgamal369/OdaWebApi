using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Models;

namespace OdaWepApi.Models;

public partial class Booking
{
    public int Bookingid { get; set; }

    public int? Customerid { get; set; }

    public int? Apartmentid { get; set; }

    public DateTime? Createdatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }

    public string? Bookingstatus { get; set; }

    public int? Userid { get; set; }

    public decimal? Totalamount { get; set; }

    public virtual Apartment? Apartment { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual User? User { get; set; }
}


public static class BookingEndpoints
{
	public static void MapBookingEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Booking").WithTags(nameof(Booking));

        group.MapGet("/", async (OdaDbContext db) =>
        {
            return await db.Bookings.ToListAsync();
        })
        .WithName("GetAllBookings")
        .WithOpenApi();

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

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int bookingid, Booking booking, OdaDbContext db) =>
        {
            var affected = await db.Bookings
                .Where(model => model.Bookingid == bookingid)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Bookingid, booking.Bookingid)
                  .SetProperty(m => m.Customerid, booking.Customerid)
                  .SetProperty(m => m.Apartmentid, booking.Apartmentid)
                  .SetProperty(m => m.Createdatetime, booking.Createdatetime)
                  .SetProperty(m => m.Lastmodifieddatetime, booking.Lastmodifieddatetime)
                  .SetProperty(m => m.Bookingstatus, booking.Bookingstatus)
                  .SetProperty(m => m.Userid, booking.Userid)
                  .SetProperty(m => m.Totalamount, booking.Totalamount)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateBooking")
        .WithOpenApi();

        group.MapPost("/", async (Booking booking, OdaDbContext db) =>
        {
            db.Bookings.Add(booking);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Booking/{booking.Bookingid}",booking);
        })
        .WithName("CreateBooking")
        .WithOpenApi();

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