using Microsoft.EntityFrameworkCore;
using OdaWepApi.Domain.DTOs;
using OdaWepApi.Domain.Enums;
using OdaWepApi.Domain.Models;
using OdaWepApi.Infrastructure;

namespace OdaWepApi.API.DTOEndpoints
{
    public static class BookingDataInEndpoints
    {
        public static void MapBookingDataInEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/BookingDataIn").WithTags(nameof(BookingDataIn));

            group.MapPost("/", async (BookingDataIn bookingDataIn, OdaDbContext db) =>
            {
                var newApartmentId = await CloneApartment(db, bookingDataIn);
                var customerId = await CreateCustomer(db, bookingDataIn.CustomerInfo);
                var bookingId = await CreateBookingRecord(db, newApartmentId, customerId, bookingDataIn);
                await CreateApartmentAddons(db, newApartmentId, bookingDataIn.Addons);
                await CreateApartmentAddonPerRequests(db, newApartmentId, bookingDataIn.AddonPerRequestIDs);
                return Results.Created($"/api/Booking/{bookingId}", new { BookingID = bookingId });
            });
        }

        private static async Task<int> CloneApartment(OdaDbContext db, BookingDataIn bookingDataIn)
        {
            var newApartment = new Apartment
            {
                Apartmentstatus = Apartmentstatus.InProgress,
                Projectid = bookingDataIn.ProjectID,
                Planid = bookingDataIn.PlanID,
                Automationid = bookingDataIn.AutomationID,
                Createddatetime = DateTime.UtcNow,
                Lastmodifieddatetime = DateTime.UtcNow
            };
            db.Apartments.Add(newApartment);
            await db.SaveChangesAsync();
            return newApartment.Apartmentid;
        }

        private static async Task<int> CreateCustomer(OdaDbContext db, Customer customerInfo)
        {
            var maxCustomerId = await db.Customers.MaxAsync(c => (int?)c.Customerid) ?? 0;
            customerInfo.Customerid = maxCustomerId + 1;
            customerInfo.Createdatetime = DateTime.UtcNow;
            customerInfo.Lastmodifieddatetime = DateTime.UtcNow;
            db.Customers.Add(customerInfo);
            await db.SaveChangesAsync();
            return customerInfo.Customerid;
        }

        private static async Task<int> CreateBookingRecord(OdaDbContext db, int newApartmentId, int customerId, BookingDataIn bookingDataIn)
        {
            var booking = new Booking
            {
                Customerid = customerId,
                Apartmentid = newApartmentId,
                Paymentplanid = bookingDataIn.PaymentPlanID,
                Createdatetime = DateTime.UtcNow,
                Lastmodifieddatetime = DateTime.UtcNow,
                Bookingstatus = Bookingstatus.InProgress,
                Totalamount = await CalculateTotalAmount(db, newApartmentId, bookingDataIn)
            };
            db.Bookings.Add(booking);
            await db.SaveChangesAsync();
            return booking.Bookingid;
        }

        private static async Task CreateApartmentAddons(OdaDbContext db, int newApartmentId, List<AddonSelection> addons)
        {
            foreach (var addon in addons)
            {
                var apartmentAddon = new ApartmentAddon
                {
                    Apartmentid = newApartmentId,
                    Addonid = addon.AddonID,
                    Quantity = addon.Quantity
                };
                db.ApartmentAddons.Add(apartmentAddon);
            }
            await db.SaveChangesAsync();
        }

        private static async Task CreateApartmentAddonPerRequests(OdaDbContext db, int newApartmentId, List<int> addonPerRequestIDs)
        {
            foreach (var addonPerRequestId in addonPerRequestIDs)
            {
                var apartmentAddonPerRequest = new ApartmentAddonperrequest
                {
                    Apartmentid = newApartmentId,
                    Addperrequestid = addonPerRequestId,
                    Quantity = 1
                };
                db.ApartmentAddonperrequests.Add(apartmentAddonPerRequest);
            }
            await db.SaveChangesAsync();
        }

        private static async Task<decimal> CalculateTotalAmount(OdaDbContext db, int newApartmentId, BookingDataIn bookingDataIn)
        {
            var plan = await db.Plans.FindAsync(bookingDataIn.PlanID);
            var apartment = await db.Apartments.FindAsync(newApartmentId);
            decimal totalPlanPrice = (decimal)(plan.Pricepermeter * apartment.Apartmentspace);
            decimal totalAddonPrice = 0;
            foreach (var addon in bookingDataIn.Addons)
            {
                var addonDetails = await db.Addons.FindAsync(addon.AddonID);
                totalAddonPrice += (decimal)(addonDetails.Unitormeter == UnitOrMeterType.Unit
                    ? addonDetails.Price * addon.Quantity
                    : addonDetails.Price * apartment.Apartmentspace);
            }
            return totalPlanPrice + totalAddonPrice;
        }
    }
}
