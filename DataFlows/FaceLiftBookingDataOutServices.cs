using OdaWepApi.Infrastructure;
using OdaWepApi.Domain.Models;
using OdaWepApi.Domain.DTOs;
using OdaWepApi.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Domain.DTOs.FaceLiftDTO;

namespace OdaWepApi.DataFlows
{
    public class FaceLiftBookingDataOutServices
    {
        public static async Task<FaceLiftBookingDataOutDTO> GetFaceLiftBookingDataOutDTO(OdaDbContext db, int bookingID)
        {
            var booking = await db.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Apartment)
                .ThenInclude(a => a.Automation)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Bookingid == bookingID);

            if (booking == null) return null;

            var apartment = booking.Apartment;
            if (apartment == null) return null;

            var addonPerRequests = await db.FaceliftroomAddonperrequests
                .Where(aapr => aapr.Apartmentid == apartment.Apartmentid)
                .Include(aapr => aapr.Addperrequest)
                .AsNoTracking()
                .ToListAsync();

            var addonPerRequestDetails = addonPerRequests.Select(aapr => new AddonPerRequestDetail
            {
                AddonPerRequestID = aapr.Addperrequestid,
                AddonPerRequestName = aapr.Addperrequest.Addperrequestname,
                AddonPerRequestDescription = aapr.Addperrequest.Description
            }).ToList();

            var rooms = await db.Faceliftrooms
       .Where(r => r.Bookingid == bookingID && r.Apartmentid == booking.Apartmentid)
       .ToListAsync();

            var faceLiftRoomDTO = new List<FaceLiftRoomsDataOutDTO>();

            foreach (var room in rooms)
            {
                var addons = await db.FaceliftroomAddons
                    .Where(a => a.Roomid == room.Roomid)
                    .Select(a => new AddonDetail
                    {
                        AddonID = a.Addonid,
                        AddonName = a.Addon.Addonname,
                        Addongroup = a.Addon.Addongroup,
                        Unitormeter = a.Addon.Unitormeter,
                        Description = a.Addon.Description,
                        Quantity = a.Quantity,
                        Price = (decimal)((a.Addon.Unitormeter == UnitOrMeterType.Unit)
                        ? a.Addon.Price * a.Quantity
                        : a.Addon.Price * (apartment.Apartmentspace ?? 0))
                    })
                    .AsNoTracking()
                    .ToListAsync();


                var TotalRoomAddonPrice = addons.Sum(a => a.Price); // Initialize the TotalRoomAddonPrice for each room
                var TotalRoomAirconditionerPrice = addons
                .Where(a => a.Addongroup == "AirConditioning")
                .Sum(a => a.Price);

                faceLiftRoomDTO.Add(new FaceLiftRoomsDataOutDTO
                {
                    RoomType = room.Roomtype,
                    AddonDetail = addons,
                    TotalAddonPrice = TotalRoomAddonPrice,
                    TotalAirconditionerPrice = TotalRoomAirconditionerPrice
                });
            }
            var totalPrice = faceLiftRoomDTO.Sum(r => r.TotalAddonPrice);
            var totalAirconditionerPrice = faceLiftRoomDTO.Sum(r => r.TotalAirconditionerPrice);
            var apartmentRooms = await db.Faceliftrooms
                .Where(r => r.Apartmentid == apartment.Apartmentid)
                .GroupBy(r => r.Roomtype)
                .Select(g => new ApartmentRoomsDTO
                {
                    RoomType = g.Key,
                    NumberOfRoomsQuantity = g.Count()
                })
                .ToListAsync();
            return new FaceLiftBookingDataOutDTO
            {
                BookingID = booking.Bookingid,
                AutomationID = booking.Apartment?.Automationid,
                AddonPerRequests = addonPerRequestDetails,
                Rooms = faceLiftRoomDTO,

                ApartmentRooms = apartmentRooms,
                SumOfTotalAddonPrices = (decimal)totalPrice,
                TotalAirconditionerPrice = (decimal)totalAirconditionerPrice

            };
        }
    }
}