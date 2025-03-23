using OdaWepApi.Domain.Enums;
using OdaWepApi.Domain.Models;

namespace OdaWepApi.Domain.DTOs.FaceLiftDTO
{
    public class FaceLiftBookingDataOutDTO
    {
        public int BookingID { get; set; }
        public int? AutomationID { get; set; }
        public List<AddonPerRequestDetail>? AddonPerRequests { get; set; }
        public List<FaceLiftRoomDTO>? Rooms { get; set; }
    }
}
