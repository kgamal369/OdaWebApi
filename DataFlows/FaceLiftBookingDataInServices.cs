using OdaWepApi.Infrastructure;
using OdaWepApi.Domain.DTOs;
using OdaWepApi.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Domain.DTOs.FaceLiftDTO;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using OdaWepApi.Domain.Models.FaceLift;
using OdaWepApi.Domain.Models.Common;
using OdaWepApi.Domain.Models.LocateYourHome_BuildYourKit;

namespace OdaWepApi.DataFlows
{
    public class FaceLiftBookingDataInServices
    {
        public static async Task<int> CreateFaceLiftBookingDataIn(OdaDbContext db, FaceLiftBookingDataInDTO faceLiftBookingDataInDTO)
        {
            using var transaction = await db.Database.BeginTransactionAsync(); // 🔥 Start transaction
            try
            {
                int newBookingId;
                var newBooking = new Booking
                {
                    Customerid = await CreateOrGetCustomer(db, faceLiftBookingDataInDTO.CustomerInfo),
                    Apartmentid = await CreateOrGetApartment(db, faceLiftBookingDataInDTO),
                    //    Paymentplanid = faceLiftBookingDataInDTO.PaymentPlanID,
                    Createdatetime = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
                    Lastmodifieddatetime = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
                    Bookingstatus = Bookingstatus.InProgress,
                    Totalamount = await CalculateTotalAmount(db, faceLiftBookingDataInDTO)
                };
                db.Bookings.Add(newBooking);
                await db.SaveChangesAsync();
                newBookingId = newBooking.Bookingid;
                int newApartmentId = (int)newBooking.Apartmentid;

                // Create FaceLiftRoom
                List<int> roomIds = await CreateFaceLiftRoom(db, newBookingId, newApartmentId, faceLiftBookingDataInDTO);

                // Create apartment addon per requests
                await CreateApartmentAddonPerRequests(db, newApartmentId, faceLiftBookingDataInDTO.AddonPerRequestIDs);

                await db.SaveChangesAsync();

                await transaction.CommitAsync(); // 🔥 Commit only if everything succeeds

                return newBookingId;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(); // 🔥 Rollback on failure
                throw;
            }
        }

        private static async Task<int?> CreateOrGetApartment(OdaDbContext db, FaceLiftBookingDataInDTO faceLiftBookingDataInDTO)
        {
            var Apartment = new Apartment
            {
                Createddatetime = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
                Lastmodifieddatetime = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
                Apartmenttype = ApartmentType.Facelift,
                Apartmentstatus = Apartmentstatus.InProgress,
                Automationid = faceLiftBookingDataInDTO.AutomationID,
            };
            db.Apartments.Add(Apartment);
            await db.SaveChangesAsync();
            return Apartment.Apartmentid;
        }

        private static async Task<List<int>> CreateFaceLiftRoom(OdaDbContext db, int newBookingId, int newApartmentId, FaceLiftBookingDataInDTO bookingDataIn)
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
                    Bookingid = newBookingId,
                    Apartmentid = newApartmentId
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


        private static async Task CreateApartmentAddonPerRequests(OdaDbContext db, int newApartmentId, List<int> addonPerRequestIDs)
        {
            foreach (var addonPerRequestId in addonPerRequestIDs)
            {
                var apartmentAddonPerRequest = new FaceliftroomAddonperrequest
                {
                    Apartmentid = newApartmentId,
                    Addperrequestid = addonPerRequestId,
                    Quantity = 1
                };
                db.FaceliftroomAddonperrequests.Add(apartmentAddonPerRequest);
            }
            await db.SaveChangesAsync();
        }

        private static async Task<decimal> CalculateTotalAmount(OdaDbContext db, FaceLiftBookingDataInDTO dto)
        {
            decimal total = 0;
            foreach (var room in dto.Rooms)
            {
                foreach (var addon in room.AddonSelectionsList)
                {
                    var details = await db.Addons.FindAsync(addon.AddonID);
                    if (details == null) continue;

                    if (details.Unitormeter == UnitOrMeterType.Unit)
                    {
                        total += (decimal)(details.Price * addon.Quantity);
                    }
                    else if (details.Unitormeter == UnitOrMeterType.Meter)
                    {
                        int space = room.RoomType switch
                        {
                            FaceLiftRoomType.Bedroom => 16,
                            FaceLiftRoomType.Bathroom => 4,
                            FaceLiftRoomType.Kitchen => 9,
                            _ => 1 // Fallback for undefined room types
                        };

                        total += (decimal)(details.Price * space * addon.Quantity);
                    }
                }
            }
            return total;
        }

        public static async Task<int> UpdateFaceLiftBookingDataIn(OdaDbContext db, int bookingID, FaceLiftBookingDataInDTO bookingDataIn)
        {
            //Fetch the exiting Booking record
            // Fetch the existing booking
            var booking = await db.Bookings
                .Include(b => b.Apartment)
                .Include(b => b.Customer)
                .FirstOrDefaultAsync(b => b.Bookingid == bookingID);

            if (booking == null)
                throw new Exception("Booking not found.");


            // Update the booking fields
            // booking.Paymentplanid = bookingDataIn.PaymentPlanID;
            booking.Lastmodifieddatetime = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
            booking.Totalamount = await CalculateTotalAmount(db, bookingDataIn);

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
            if (booking.Apartment != null)
            {
                booking.Apartment.Automationid = bookingDataIn.AutomationID;
                booking.Apartment.Lastmodifieddatetime = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
            }

            // Update room and addons 
            await UpdateFaceLiftRoom(db, booking.Apartmentid ?? 0, bookingDataIn.Rooms);



            // Save changes to the database
            await db.SaveChangesAsync();
            // Update  addon per requests
            await UpdateApartmentAddonPerRequests(db, booking.Apartmentid ?? 0, bookingDataIn.AddonPerRequestIDs);
            // Save changes to the database
            await db.SaveChangesAsync();

            // Return the booking ID
            return bookingID;
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
        private static async Task UpdateFaceLiftRoom(OdaDbContext db, int apartmentId, List<FaceLiftRoomDTO> rooms)
        {
            var existingRooms = db.Faceliftrooms.Where(r => r.Apartmentid == apartmentId);
            db.Faceliftrooms.RemoveRange(existingRooms);
            await db.SaveChangesAsync();

            foreach (var room in rooms)
            {
                var newRoom = new Faceliftroom
                {
                    Roomtype = room.RoomType,
                    Createddatetime = DateTime.UtcNow,
                    Lastmodifieddatetime = DateTime.UtcNow,
                    Apartmentid = apartmentId
                };
                db.Faceliftrooms.Add(newRoom);
                await db.SaveChangesAsync();

                foreach (var addon in room.AddonSelectionsList)
                {
                    db.FaceliftroomAddons.Add(new FaceliftroomAddon
                    {
                        Roomid = newRoom.Roomid,
                        Addonid = addon.AddonID,
                        Quantity = addon.Quantity
                    });
                }
                await db.SaveChangesAsync();
            }
        }
    }
}
