using OdaWepApi.Domain.Enums;

namespace OdaWepApi.Domain.DTOs
{
    public class ApartmentRoomsDTO
    {
        public FaceLiftRoomType RoomType { get; set; }
        public int? NumberOfRoomsQuantity { get; set; }
    }
}