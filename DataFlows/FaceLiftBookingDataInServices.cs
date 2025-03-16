using OdaWepApi.Infrastructure;
using OdaWepApi.Domain.Models;
using OdaWepApi.Domain.DTOs;
using OdaWepApi.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Domain.DTOs.FaceLiftDTO;

namespace OdaWepApi.DataFlows
{
    public class FaceLiftBookingDataInServices
    {
        public static async Task<int> CreateFaceLiftBookingDataIn(OdaDbContext db ,FaceLiftBookingDataInDTO faceLiftBookingDataInDTO )
        {
            using var transaction = await db.Database.BeginTransactionAsync(); // 🔥 Start transaction
            try
            {   
                // Create or get customer
                int customerId = await CreateOrGetCustomer(db, faceLiftBookingDataInDTO.CustomerInfo);

                // Create or get question

                // Create booking record
               // int bookingId = await CreateBookingRecord(db, , customerId, faceLiftBookingDataInDTO);

                
                 return customerId;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(); // 🔥 Rollback on failure
                throw;
            }
        }
        private static async Task<int> CreateOrGetCustomer(OdaDbContext db, Customer customerInfo)
        {
            var existingCustomer = await db.Customers.FirstOrDefaultAsync(c => c.Email == customerInfo.Email);
            if (existingCustomer != null)
            {
                return existingCustomer.Customerid;
            }

            customerInfo.Createdatetime = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
            customerInfo.Lastmodifieddatetime = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
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
                Createdatetime = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
                Lastmodifieddatetime = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
                Bookingstatus = Bookingstatus.InProgress,
                Totalamount = await CalculateTotalAmount(db, newApartmentId, bookingDataIn)
            };
            db.Bookings.Add(booking);
            await db.SaveChangesAsync();

            return booking.Bookingid;
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
