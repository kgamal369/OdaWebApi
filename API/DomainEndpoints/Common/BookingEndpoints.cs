using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.DataFlows;
using OdaWepApi.Domain.Enums;
using OdaWepApi.Domain.DTOs;
using OdaWepApi.Infrastructure;
using OdaWepApi.Domain.Models.Common;
using OdaWepApi.Domain.Models.LocateYourHome_BuildYourKit;

namespace OdaWepApi.API.DomainEndpoints.Common
{
    public static class BookingEndpoints
    {
        public static void MapBookingEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Booking").WithTags(nameof(Booking));

            // 1. Get All Bookings
            group.MapGet("/", async (OdaDbContext db) =>
            {
                return await db.Bookings
                    .Include(b => b.Customer)
                    .Include(b => b.Apartment)
                    .Include(b => b.Paymentmethod)
                    .Include(b => b.Paymentplan)
                    .Include(b => b.User)
                    .AsNoTracking()
                    .ToListAsync();
            })
            .WithName("GetAllBookings")
            .WithOpenApi();

            // 2. Get Booking by ID
            group.MapGet("/{id}", async Task<Results<Ok<Booking>, NotFound>> (int id, OdaDbContext db) =>
            {
                var booking = await db.Bookings
                    .Include(b => b.Customer)
                    .Include(b => b.Apartment)
                    .Include(b => b.Paymentmethod)
                    .Include(b => b.Paymentplan)
                    .Include(b => b.User)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(b => b.Bookingid == id);

                return booking is not null
                    ? TypedResults.Ok(booking)
                    : TypedResults.NotFound();
            })
            .WithName("GetBookingById")
            .WithOpenApi();

            // 3. Create a New Booking
            group.MapPost("/", async Task<Results<Created<Booking>, BadRequest<string>>> (Booking booking, OdaDbContext db) =>
            {
                // Validate Customer ID
                if (booking.Customerid.HasValue && !await db.Customers.AnyAsync(c => c.Customerid == booking.Customerid))
                {
                    return TypedResults.BadRequest($"Invalid CustomerId: {booking.Customerid}");
                }

                // Validate Apartment ID
                if (booking.Apartmentid.HasValue && !await db.Apartments.AnyAsync(a => a.Apartmentid == booking.Apartmentid))
                {
                    return TypedResults.BadRequest($"Invalid ApartmentId: {booking.Apartmentid}");
                }

                // Validate Payment Plan ID
                if (booking.Paymentplanid.HasValue && !await db.Paymentplans.AnyAsync(p => p.Paymentplanid == booking.Paymentplanid))
                {
                    return TypedResults.BadRequest($"Invalid PaymentPlanId: {booking.Paymentplanid}");
                }

                // Validate Payment Method ID
                if (booking.Paymentmethodid.HasValue && !await db.Paymentmethods.AnyAsync(pm => pm.Paymentmethodid == booking.Paymentmethodid))
                {
                    return TypedResults.BadRequest($"Invalid PaymentMethodId: {booking.Paymentmethodid}");
                }

                // Validate Booking Status
                if (!Enum.IsDefined(typeof(Bookingstatus), booking.Bookingstatus))
                {
                    return TypedResults.BadRequest("Invalid BookingStatus.");
                }

                // Calculate Total Amount
                booking.Totalamount = await CalculateTotalAmount(booking, db);

                booking.Createdatetime = DateTime.UtcNow;
                booking.Lastmodifieddatetime = booking.Createdatetime;

                db.Bookings.Add(booking);
                await db.SaveChangesAsync();

                return TypedResults.Created($"/api/Booking/{booking.Bookingid}", booking);
            })
            .WithName("CreateBooking")
            .WithOpenApi();

            // 4. Update an Existing Booking
            group.MapPut("/{id}", async Task<Results<Ok, NotFound, BadRequest<string>>> (int id, Booking updatedBooking, OdaDbContext db) =>
            {
                var existingBooking = await db.Bookings.FindAsync(id);
                if (existingBooking is null)
                    return TypedResults.NotFound();

                // Validate Customer ID
                if (updatedBooking.Customerid.HasValue && !await db.Customers.AnyAsync(c => c.Customerid == updatedBooking.Customerid))
                {
                    return TypedResults.BadRequest($"Invalid CustomerId: {updatedBooking.Customerid}");
                }

                // Validate Apartment ID
                if (updatedBooking.Apartmentid.HasValue && !await db.Apartments.AnyAsync(a => a.Apartmentid == updatedBooking.Apartmentid))
                {
                    return TypedResults.BadRequest($"Invalid ApartmentId: {updatedBooking.Apartmentid}");
                }

                // Validate Payment Plan ID
                if (updatedBooking.Paymentplanid.HasValue && !await db.Paymentplans.AnyAsync(p => p.Paymentplanid == updatedBooking.Paymentplanid))
                {
                    return TypedResults.BadRequest($"Invalid PaymentPlanId: {updatedBooking.Paymentplanid}");
                }

                // Validate Booking Status
                if (!Enum.IsDefined(typeof(Bookingstatus), updatedBooking.Bookingstatus))
                {
                    return TypedResults.BadRequest("Invalid BookingStatus.");
                }

                // Update properties
                existingBooking.Customerid = updatedBooking.Customerid;
                existingBooking.Apartmentid = updatedBooking.Apartmentid;
                existingBooking.Paymentmethodid = updatedBooking.Paymentmethodid;
                existingBooking.Paymentplanid = updatedBooking.Paymentplanid;
                existingBooking.Bookingstatus = updatedBooking.Bookingstatus;
                existingBooking.Totalamount = await CalculateTotalAmount(updatedBooking, db);
                existingBooking.Lastmodifieddatetime = DateTime.UtcNow;

                await db.SaveChangesAsync();
                return TypedResults.Ok();
            })
            .WithName("UpdateBooking")
            .WithOpenApi();

            // 5. Delete a Booking
            group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, OdaDbContext db) =>
            {
                var booking = await db.Bookings.FindAsync(id);
                if (booking is null)
                    return TypedResults.NotFound();

                db.Bookings.Remove(booking);
                await db.SaveChangesAsync();

                return TypedResults.Ok();
            })
            .WithName("DeleteBooking")
            .WithOpenApi();

            // 6. Get All Bookings for a Specific Customer
            group.MapGet("/by-customer/{customerId}", async (int customerId, OdaDbContext db) =>
            {
                return await db.Bookings
                    .Where(b => b.Customerid == customerId)
                    .ToListAsync();
            })
            .WithName("GetBookingsByCustomer")
            .WithOpenApi();

            // 7. Get All Bookings for a Specific Apartment
            group.MapGet("/by-apartment/{apartmentId}", async (int apartmentId, OdaDbContext db) =>
            {
                return await db.Bookings
                    .Where(b => b.Apartmentid == apartmentId)
                    .ToListAsync();
            })
            .WithName("GetBookingsByApartment")
            .WithOpenApi();

            // 8. Confirm a Booking (Only if Status is InProgress) and Send Email
            group.MapPut("/{id}/confirm", async Task<Results<Ok, NotFound, BadRequest<string>>> (int id, OdaDbContext db) =>
            {
                var booking = await db.Bookings
                    .Include(b => b.Apartment)
                    .FirstOrDefaultAsync(b => b.Bookingid == id);

                if (booking == null)
                {
                    return TypedResults.NotFound();
                }

                if (booking.Apartment == null)
                {
                    // Log the issue or handle it appropriately
                    return TypedResults.BadRequest("Apartment details are missing for this booking.");
                }

                // Allow confirmation nly if status is "InProgress"
                if (booking.Bookingstatus != Bookingstatus.InProgress)
                    return TypedResults.BadRequest("Booking can only be confirmed if it is in 'InProgress' status.");

                // Update booking status to Confirmed
                booking.Bookingstatus = Bookingstatus.Confirmed;

                // Fix the UTC DateTime issue
                booking.Lastmodifieddatetime = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);

                // Handle apartment creation or cloning based on ApartmentType
                booking.Apartment.Apartmentstatus = Apartmentstatus.InProgress;

                if (booking.Apartment.Apartmenttype == (int)ApartmentType.Project)
                {
                    if (booking.Apartmentid == null)
                        throw new ArgumentException("ApartmentID must be provided for Project type.");
                    var existingApartment = await db.Apartments.FindAsync(booking.Apartmentid);
                    if (existingApartment == null)
                        throw new Exception("Apartment not found for cloning.");
                    var clonedApartment = new Apartment
                    {
                        Apartmenttype = existingApartment.Apartmenttype,
                        Apartmentstatus = Apartmentstatus.ForSale,
                        Apartmentspace = existingApartment.Apartmentspace,
                        Description = existingApartment.Description,
                        Createddatetime = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
                        Lastmodifieddatetime = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified)
                    };
                    db.Apartments.Add(clonedApartment);
                    await db.SaveChangesAsync();
                }

                await db.SaveChangesAsync();

                // Get booking data for email
                var bookingDataOut = await BookingDataOutServices.GetBookingDataOut(db, id);
                if (bookingDataOut != null)
                {
                    // Send email notification
                    string emailBody = EmailService.GenerateEmailBody(bookingDataOut);
                    await EmailService.SendEmailToAllRecipients("Booking Confirmed!", emailBody);
                    if (bookingDataOut.CustomerInfo.Email != null)
                    {
                        // send email to customer 
                        string customerEmail = bookingDataOut.CustomerInfo.Email;
                        string customerEmailBody = EmailService.GenerateCustomerEmailBody(bookingDataOut);
                        await EmailService.SendCustomerEmail("Booking Confirmed!", customerEmailBody, customerEmail);
                    }
                }


                return TypedResults.Ok();
            })
            .WithName("ConfirmBooking")
            .WithOpenApi();
        }


        // **Helper function to calculate total amount**
        private static async Task<decimal> CalculateTotalAmount(Booking booking, OdaDbContext db)
        {
            if (booking.Apartmentid is null)
                return 0;

            var apartment = await db.Apartments.FindAsync(booking.Apartmentid);
            if (apartment is null || apartment.Apartmentspace is null)
                return 0;

            var plan = await db.Plans.FindAsync(booking.Paymentplanid);
            if (plan is null)
                return 0;

            return (decimal)(apartment.Apartmentspace.Value * plan.Pricepermeter);
        }
    }
}