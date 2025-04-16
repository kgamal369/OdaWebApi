using OdaWepApi.Domain.Enums;
using OdaWepApi.Domain.Models;

namespace OdaWepApi.Domain.DTOs.FaceLiftDTO
{
    public class FaceLiftBookingDataOutDTO
    {
        public int BookingID { get; set; }
        public int? AutomationID { get; set; }
        public List<AddonPerRequestDetail>? AddonPerRequests { get; set; }
        public List<FaceLiftRoomsDataOutDTO>? Rooms { get; set; }
        public List<ApartmentRoomsDTO>? ApartmentRooms { get; set; }
        public decimal SumOfTotalAddonPrices { get; set; }
        public decimal TotalAirconditionerPrice { get; set; }
    }
}
