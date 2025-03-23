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
                .Include(b => b.Apartment)
                .ThenInclude(a => a.Plan)
                .Include(b => b.Paymentplan)
                .FirstOrDefaultAsync(b => b.Bookingid == bookingID);

            if (booking == null) return null;


            return new FaceLiftBookingDataOutDTO
            {
                BookingID = booking.Bookingid,
                AutomationID = booking.Apartment?.Automationid,
                AddonPerRequests = null,
                Rooms = null

            };

        }
    }
}