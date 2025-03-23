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
                .Include(b => b.Paymentplan)
                  .ThenInclude(p => p.Installmentbreakdowns)
                .FirstOrDefaultAsync(b => b.Bookingid == bookingID);

            if (booking == null) return null;

            var apartment = booking.Apartment;
            if (apartment == null) return null;

            var paymentPlan = booking.Paymentplan ?? throw new System.Exception("Payment plan not found.");

            var addonPerRequests = await db.ApartmentAddonperrequests
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

            var faceLiftRoomDTO = new List<FaceLiftRoomDTO>();

            foreach (var room in rooms)
            {
                var addons = await db.FaceliftroomAddons
                    .Where(a => a.Roomid == room.Roomid)
                    .Select(a => new AddonSelection
                    {
                        AddonID = a.Addonid,
                        Quantity = a.Quantity
                    })
                    .ToListAsync();

                faceLiftRoomDTO.Add(new FaceLiftRoomDTO
                {
                    RoomType = room.Roomtype,
                    AddonSelectionsList = addons
                });
            }

            return new FaceLiftBookingDataOutDTO
            {
                BookingID = booking.Bookingid,
                AutomationID = booking.Apartment?.Automationid,
                AddonPerRequests = addonPerRequestDetails,
                Rooms = faceLiftRoomDTO
            };
        }
    }
}