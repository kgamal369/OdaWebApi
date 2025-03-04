using OdaWepApi.Infrastructure;
using OdaWepApi.Domain.Models;
using OdaWepApi.Domain.DTOs;
using OdaWepApi.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace OdaWepApi.DataFlows
{
    public class BookingDataInServices
    {
        public static async Task<int> CreateBookingDataIn(OdaDbContext db, BookingDataIn bookingDataIn)
        {
            using var transaction = await db.Database.BeginTransactionAsync(); // 🔥 Start transaction
            try
            {
                int newApartmentId;


                var newApartment = new Apartment
                {
                    Apartmenttype = (ApartmentType)bookingDataIn.apartmentDTO.ApartmentType,
                    Apartmentstatus = Apartmentstatus.InProgress,
                    Apartmentspace = bookingDataIn.apartmentDTO.ApartmentSpace,
                    Apartmentaddress = bookingDataIn.apartmentDTO.ApartmentAddress,
                    Description = bookingDataIn.apartmentDTO.ApartmentAddress,
                    Planid = bookingDataIn.PlanID,
                    Automationid = bookingDataIn.AutomationID,
                    Unittypeid = bookingDataIn.apartmentDTO.Unittypeid,
                    Createddatetime = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
                    Lastmodifieddatetime = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified)
                };

                db.Apartments.Add(newApartment);
                await db.SaveChangesAsync();
                newApartmentId = newApartment.Apartmentid;

                // Create or get customer
                int customerId = await CreateOrGetCustomer(db, bookingDataIn.CustomerInfo);

                // Create or get question

                // Create booking record
                int bookingId = await CreateBookingRecord(db, newApartmentId, customerId, bookingDataIn);

                // Insert questions associated with the booking
                await CreateOrGetQuestion(db, bookingDataIn.Questions, bookingId);

                // Create apartment addons
                await CreateApartmentAddons(db, newApartmentId, bookingDataIn.Addons);

                // Create apartment addon per requests
                await CreateApartmentAddonPerRequests(db, newApartmentId, bookingDataIn.AddonPerRequestIDs);

                await transaction.CommitAsync(); // 🔥 Commit only if everything succeeds

                return bookingId;
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


        private static async Task<int> CreateOrGetQuestion(OdaDbContext db, List<Question> questions, int bookingId)
        {
            if (questions == null || questions.Count == 0)
            {
                return 0;
            }

            foreach (var question in questions)
            {
                question.Bookingid = bookingId; // Ensure the booking ID is set
            }

            db.Questions.AddRange(questions); // Add the list of questions to the context
            return await db.SaveChangesAsync(); // Save changes and return the number of affected rows
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

        public static async Task<int> UpdateBookingDataIn(OdaDbContext db, int bookingID, BookingDataIn bookingDataIn)
        {
            // Fetch the existing booking
            var booking = await db.Bookings
                .Include(b => b.Apartment)
                .Include(b => b.Customer)
                .FirstOrDefaultAsync(b => b.Bookingid == bookingID);

            if (booking == null)
                throw new Exception("Booking not found.");

            // Update the booking fields
            booking.Paymentplanid = bookingDataIn.PaymentPlanID;
            booking.Lastmodifieddatetime = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
            booking.Totalamount = await CalculateTotalAmount(db, booking.Apartmentid ?? 0, bookingDataIn);

            // Update the apartment details if applicable
            if (booking.Apartment != null)
            {
                booking.Apartment.Apartmentspace = bookingDataIn.apartmentDTO.ApartmentSpace;
                booking.Apartment.Description = bookingDataIn.apartmentDTO.ApartmentAddress;
                booking.Apartment.Planid = bookingDataIn.PlanID;
                booking.Apartment.Automationid = bookingDataIn.AutomationID;
                booking.Apartment.Lastmodifieddatetime = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
            }

            // Update the customer details
            if (booking.Customer != null)
            {
                booking.Customer.Firstname = bookingDataIn.CustomerInfo.Firstname;
                booking.Customer.Lastname = bookingDataIn.CustomerInfo.Lastname;
                booking.Customer.Email = bookingDataIn.CustomerInfo.Email;
                booking.Customer.Phonenumber = bookingDataIn.CustomerInfo.Phonenumber;
                booking.Customer.Lastmodifieddatetime = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
            }

            // Update apartment addons
            await UpdateApartmentAddons(db, booking.Apartmentid ?? 0, bookingDataIn.Addons);

            // Update apartment addon per requests
            await UpdateApartmentAddonPerRequests(db, booking.Apartmentid ?? 0, bookingDataIn.AddonPerRequestIDs);
            // Save changes to the database
            await db.SaveChangesAsync();

            // Return the booking ID
            return bookingID;
        }

        private static async Task UpdateApartmentAddons(OdaDbContext db, int apartmentId, List<AddonSelection> addons)
        {
            var existingAddons = db.ApartmentAddons.Where(a => a.Apartmentid == apartmentId);
            db.ApartmentAddons.RemoveRange(existingAddons);

            foreach (var addon in addons)
            {
                var apartmentAddon = new ApartmentAddon
                {
                    Apartmentid = apartmentId,
                    Addonid = addon.AddonID,
                    Quantity = addon.Quantity
                };
                db.ApartmentAddons.Add(apartmentAddon);
            }
            await db.SaveChangesAsync();
        }

        private static async Task UpdateApartmentAddonPerRequests(OdaDbContext db, int apartmentId, List<int> addonPerRequestIDs)
        {
            var existingAddonPerRequests = db.ApartmentAddonperrequests.Where(a => a.Apartmentid == apartmentId);
            db.ApartmentAddonperrequests.RemoveRange(existingAddonPerRequests);

            foreach (var addonPerRequestId in addonPerRequestIDs)
            {
                var apartmentAddonPerRequest = new ApartmentAddonperrequest
                {
                    Apartmentid = apartmentId,
                    Addperrequestid = addonPerRequestId,
                    Quantity = 1
                };
                db.ApartmentAddonperrequests.Add(apartmentAddonPerRequest);
            }
            await db.SaveChangesAsync();
        }
    }
}