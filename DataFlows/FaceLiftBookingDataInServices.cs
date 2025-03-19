using OdaWepApi.Infrastructure;
using OdaWepApi.Domain.Models;
using OdaWepApi.Domain.DTOs;
using OdaWepApi.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Domain.DTOs.FaceLiftDTO;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace OdaWepApi.DataFlows
{
    public class FaceLiftBookingDataInServices
    {
        public static async Task<int> CreateFaceLiftBookingDataIn(OdaDbContext db, FaceLiftBookingDataInDTO faceLiftBookingDataInDTO)
        {
            using var transaction = await db.Database.BeginTransactionAsync(); // 🔥 Start transaction
            try
            {
                int newApartmentId;
                var newApartment = new Apartment
                {
                    Createddatetime = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
                    Lastmodifieddatetime = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
                    Automationid = faceLiftBookingDataInDTO.AutomationID
                };
                db.Apartments.Add(newApartment);
                await db.SaveChangesAsync();
                newApartmentId = newApartment.Apartmentid;

                // Create FaceLiftRoom
                List<int> roomIds = await CreateFaceLiftRoom(db, faceLiftBookingDataInDTO);
                // Create or get customer
                int customerId = await CreateOrGetCustomer(db, faceLiftBookingDataInDTO.CustomerInfo);

                // Create apartment addon per requests
                await CreateApartmentAddonPerRequests(db, newApartmentId, faceLiftBookingDataInDTO.AddonPerRequestIDs);
                int bookingId = await CreateBookingRecord(db, customerId, faceLiftBookingDataInDTO);

                await db.SaveChangesAsync();

                // Return the booking ID
                return bookingId;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(); // 🔥 Rollback on failure
                throw;
            }
        }
        private static async Task<List<int>> CreateFaceLiftRoom(OdaDbContext db, FaceLiftBookingDataInDTO bookingDataIn)
        {
            var roomToInsert = new List<Faceliftroom>();
            List<int> roomIds = new List<int>();
            foreach (var room in bookingDataIn.Rooms)
            {
                var newRoom = new Faceliftroom
                {
                    Roomtype = room.RoomType,
                    Createddatetime = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
                    Lastmodifieddatetime = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
                    //Bookingid = bookingid,
                    Automationid = bookingDataIn.AutomationID
                };
                roomToInsert.Add(newRoom);
                roomIds.Add(newRoom.Roomid);
                await db.SaveChangesAsync();

                for (int i = 0; i < room.AddonSelectionsList.Count; i++)
                {
                    var faceliftroomAddon = new FaceliftroomAddon
                    {
                        Addonid = room.AddonSelectionsList[i].AddonID,
                        Roomid = newRoom.Roomid,
                        Quantity = room.AddonSelectionsList[i].Quantity,
                    };
                    db.FaceliftroomAddons.Add(faceliftroomAddon);
                }
                await db.SaveChangesAsync();
            }
            return roomIds;
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
        private static async Task<int> CreateBookingRecord(OdaDbContext db, int customerId, FaceLiftBookingDataInDTO bookingDataIn)
        {
            var booking = new Booking
            {
                Customerid = customerId,
                Paymentplanid = bookingDataIn.PaymentPlanID,
                Createdatetime = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
                Lastmodifieddatetime = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
                Bookingstatus = Bookingstatus.InProgress,
                Totalamount = await CalculateTotalAmount(db, bookingDataIn)
            };
            db.Bookings.Add(booking);
            await db.SaveChangesAsync();

            return booking.Bookingid;
        }
        private static async Task<decimal> CalculateTotalAmount(OdaDbContext db, FaceLiftBookingDataInDTO bookingDataIn)
        {
            //var plan = await db.Plans.FindAsync(bookingDataIn.PlanID);
            //var apartment = await db.Apartments.FindAsync(newApartmentId);
            // decimal totalPlanPrice = (decimal)(plan.Pricepermeter * apartment.Apartmentspace);
            // decimal totalAddonPrice = 0;
            // foreach (var addon in bookingDataIn.Addons)
            // {
            //     var addonDetails = await db.Addons.FindAsync(addon.AddonID);
            //     totalAddonPrice += (decimal)(addonDetails.Unitormeter == UnitOrMeterType.Unit
            //         ? addonDetails.Price * addon.Quantity
            //         : addonDetails.Price * apartment.Apartmentspace);
            // }
            // return totalPlanPrice + totalAddonPrice;
            return 0;
        }

        public static async Task<int> UpdateFaceLiftBookingDataIn(OdaDbContext db, int bookingID, FaceLiftBookingDataInDTO bookingDataIn)
        {
            return 0;
        }
    }
}
