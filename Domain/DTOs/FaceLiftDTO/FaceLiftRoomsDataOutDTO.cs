using OdaWepApi.Domain.Enums;

namespace OdaWepApi.Domain.DTOs.FaceLiftDTO
{
    public class FaceLiftRoomsDataOutDTO
    {
        public FaceLiftRoomType RoomType { get; set; }
        public List<AddonDetail>? AddonDetail { get; set; }
        public decimal? TotalAddonPrice { get; set; }
        public decimal? TotalAirconditionerPrice { get; set; }
    }
}