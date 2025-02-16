using Microsoft.EntityFrameworkCore;
using OdaWepApi.Domain.DTOs;
using OdaWepApi.Domain.Enums;
using OdaWepApi.Domain.Models;
using OdaWepApi.Infrastructure;

namespace OdaWepApi.API.DTOEndpoints
{
    public static class BookingDataOutEndpoints
    {
        public static void MapBookingDataOutEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/BookingDataOut").WithTags(nameof(BookingDataOut));

            group.MapGet("/{bookingID}", async (int bookingID, OdaDbContext db) =>
            {
                var bookingDataOut = await FetchBookingDetails(db, bookingID);
                return bookingDataOut != null ? Results.Ok(bookingDataOut) : Results.NotFound();
            });
        }

        private static async Task<BookingDataOut> FetchBookingDetails(OdaDbContext db, int bookingID)
        {
            var booking = await db.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Apartment)
                    .ThenInclude(a => a.Project)
                .Include(b => b.Apartment)
                    .ThenInclude(a => a.Plan)
                .Include(b => b.Apartment)
                    .ThenInclude(a => a.Automation)
                .Include(b => b.Paymentplan)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Bookingid == bookingID);

            if (booking == null) return null;

            var apartment = await db.Apartments
                .Include(a => a.Project)
                .Include(a => a.Plan)
                .Include(a => a.Automation)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Apartmentid == booking.Apartmentid);

            var plan = await db.Plans.AsNoTracking().FirstOrDefaultAsync(p => p.Planid == booking.Paymentplanid);
            var totalPlanPrice = plan.Pricepermeter * apartment.Apartmentspace;

            var addons = await db.ApartmentAddons
                .Where(aa => aa.Apartmentid == apartment.Apartmentid)
                .Include(aa => aa.Addon)
                .AsNoTracking()
                .ToListAsync();

            var addonDetails = addons.Select(aa => new AddonDetail
            {
                AddonID = aa.Addonid,
                AddonName = aa.Addon.Addonname,
                Quantity = aa.Quantity,
                Price = (decimal)(aa.Addon.Unitormeter == UnitOrMeterType.Unit ? aa.Addon.Price * aa.Quantity : aa.Addon.Price * apartment.Apartmentspace)
            }).ToList();

            var totalAddonPrice = addonDetails.Sum(a => a.Price);

            var addonPerRequests = await db.ApartmentAddonperrequests
                .Where(aapr => aapr.Apartmentid == apartment.Apartmentid)
                .Include(aapr => aapr.Addperrequest)
                .AsNoTracking()
                .ToListAsync();

            var addonPerRequestDetails = addonPerRequests.Select(aapr => new AddonPerRequestDetail
            {
                AddonPerRequestID = aapr.Addperrequestid,
                AddonPerRequestName = aapr.Addperrequest.Addperrequestname
            }).ToList();

            return new BookingDataOut
            {
                BookingID = booking.Bookingid,
                DeveloperID = (int)booking.Apartment.Project.Developerid,
                ProjectID = (int)booking.Apartment.Projectid,
                NewApartmentID = (int)booking.Apartmentid,
                ApartmentSpace = (decimal)booking.Apartment.Apartmentspace,
                PlanID = (int)booking.Apartment.Planid,
                PlanName = plan.Planname,
                TotalPlanPrice = (decimal)totalPlanPrice,
                Addons = addonDetails,
                SumOfTotalAddonPrices = totalAddonPrice,
                AutomationID = booking.Apartment.Automationid,
                AddonPerRequests = addonPerRequestDetails,
                CustomerInfo = new Customer
                {
                    Customerid = booking.Customer.Customerid,
                    Firstname = booking.Customer.Firstname,
                    Lastname = booking.Customer.Lastname
                },
                //PaymentPlanID = (int)booking.Paymentplanid,
                TotalAmount = (decimal)(totalPlanPrice + totalAddonPrice)
            };
        }
    }
}

