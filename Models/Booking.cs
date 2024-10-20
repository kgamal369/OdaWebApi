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
    [Range(10, 10000000, ErrorMessage = "Total Amount must be between 10 and 10M EGP.")]
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

            // Ensure Apartment is not booked more than once
            if (await db.Bookings.AnyAsync(b => b.Apartmentid == booking.Apartmentid && b.Bookingid != bookingid))
                return TypedResults.BadRequest("Apartment is already booked.");

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

            // Ensure Apartment is not booked more than once
            if (await db.Bookings.AnyAsync(b => b.Apartmentid == booking.Apartmentid))
                return Results.BadRequest("Apartment is already booked.");

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

        // Get All Bookings for a Customer
        group.MapGet("/customer/{customerId}", async Task<Results<Ok<IEnumerable<Booking>>, NotFound>> (int customerId, OdaDbContext db) =>
        {
            var bookings = await db.Bookings.Where(b => b.Customerid == customerId).ToListAsync();
            return bookings.Count > 0 ? TypedResults.Ok(bookings.AsEnumerable()) : TypedResults.NotFound();
        })
        .WithName("GetBookingsByCustomer")
        .WithOpenApi();

        // Check if Related Invoice Amount Equals Booking Amount
        group.MapGet("/{id}/check-invoice-amount", async Task<Results<Ok<bool>, NotFound>> (int bookingid, OdaDbContext db) =>
        {
            var booking = await db.Bookings
                .Include(b => b.Invoices)
                .FirstOrDefaultAsync(b => b.Bookingid == bookingid);

            if (booking == null) return TypedResults.NotFound();

            var totalInvoiceAmount = booking.Invoices.Sum(i => i.Invoiceamount) ?? 0;
            var isEqual = totalInvoiceAmount == booking.Totalamount;

            return TypedResults.Ok(isEqual);
        })
        .WithName("CheckBookingInvoiceAmount")
        .WithOpenApi();


        // Booking Workflow - Create and Start Booking Process
        group.MapPost("/start", async Task<IResult> (int customerid, int apartmentid, OdaDbContext db) =>
        {
            // Validate Apartment status ("Template" or "Standalone")
            var apartment = await db.Apartments
                .Include(a => a.Packages)
                .Include(a => a.Apartmentaddons)
                .FirstOrDefaultAsync(a => a.Apartmentid == apartmentid &&
                    (a.Apartmentstatus == "Template" || a.Apartmentstatus == "Standalone"));

            if (apartment == null)
                return TypedResults.BadRequest("Apartment must have 'Template' or 'Standalone' status to be booked.");

            if (apartment.Packages.Any(p => p.Assignedpackage == true) ||
                apartment.Apartmentaddons.Any(aa => aa.Assignedaddons == true))
                return TypedResults.BadRequest("Template or Standalone apartments should not have assigned packages or addons.");

            // Clone the apartment directly
            var clonedApartment = new Apartment
            {
                Apartmentname = $"{apartment.Apartmentname} - Draft",
                Apartmenttype = apartment.Apartmenttype,
                Apartmentstatus = "Draft",
                Apartmentspace = apartment.Apartmentspace,
                Description = apartment.Description,
                Projectid = apartment.Projectid,
                Floornumber = apartment.Floornumber,
                Availabilitydate = apartment.Availabilitydate,
                Createddatetime = DateTime.UtcNow,
                Lastmodifieddatetime = DateTime.UtcNow
            };

            db.Apartments.Add(clonedApartment);
            await db.SaveChangesAsync();

            // Clone the available packages
            foreach (var package in apartment.Packages.Where(p => p.Assignedpackage == false || p.Assignedpackage == null))
            {
                var newPackage = new Package
                {
                    Packagename = package.Packagename,
                    Price = package.Price,
                    Description = package.Description,
                    Assignedpackage = false,
                    Apartmentid = clonedApartment.Apartmentid,
                    Createdatetime = DateTime.UtcNow,
                    Lastmodifieddatetime = DateTime.UtcNow
                };
                db.Packages.Add(newPackage);
            }

            // Clone the available addons
            foreach (var addon in apartment.Apartmentaddons.Where(a => a.Availableaddons == false || a.Availableaddons == null))
            {
                var newAddon = new Apartmentaddon
                {
                    Apartmentid = clonedApartment.Apartmentid,
                    Addonid = addon.Addonid,
                    Availableaddons = true,
                    Assignedaddons = false,
                    Maxavailable = addon.Maxavailable,
                    Installedamount = 0,
                    Createdatetime = DateTime.UtcNow,
                    Lastmodifieddatetime = DateTime.UtcNow
                };
                db.Apartmentaddons.Add(newAddon);
            }

            await db.SaveChangesAsync();

            // Create a new Booking
            var maxBookingId = await db.Bookings.MaxAsync(b => (int?)b.Bookingid) ?? 0;
            var booking = new Booking
            {
                Bookingid = maxBookingId + 1,
                Customerid = customerid,
                Apartmentid = clonedApartment.Apartmentid,
                Bookingstatus = "InProgress",
                Createdatetime = DateTime.UtcNow,
                Lastmodifieddatetime = DateTime.UtcNow,
                Totalamount = clonedApartment.CalculateApartmentTotalPrice()
            };

            db.Bookings.Add(booking);
            await db.SaveChangesAsync();

            return TypedResults.Ok(booking);
        })
        .WithName("StartBooking")
        .WithOpenApi();


        // Confirm and Proceed to Payment Workflow
        group.MapPut("/{id}/confirm", async Task<IResult> (int id, OdaDbContext db) =>
        {
            var booking = await db.Bookings.Include(b => b.Apartment).FirstOrDefaultAsync(b => b.Bookingid == id);

            if (booking == null)
                return TypedResults.NotFound();

            var apartment = booking.Apartment;
            if (apartment == null)
                return TypedResults.BadRequest("Linked apartment not found.");

            // Turn Apartment to 'ForSale' and allow customer to assign packages and addons
            apartment.Apartmentstatus = "ForSale";
            await db.SaveChangesAsync();

            return TypedResults.Ok();
        })
        .WithName("ConfirmBooking")
        .WithOpenApi();


        // Complete Booking and Set Final Statuses
        group.MapPut("/{id}/complete", async Task<Results<Ok, NotFound>> (int id, OdaDbContext db) =>
        {
            var booking = await db.Bookings.Include(b => b.Apartment).FirstOrDefaultAsync(b => b.Bookingid == id);

            if (booking == null)
                return TypedResults.NotFound();

            // Turn Apartment to 'InReview' and Booking to 'InProgress'
            booking.Apartment.Apartmentstatus = "InReview";
            booking.Bookingstatus = "InProgress";
            booking.Lastmodifieddatetime = DateTime.UtcNow;

            await db.SaveChangesAsync();
            return TypedResults.Ok();
        })
        .WithName("CompleteBooking")
        .WithOpenApi();
    }
}