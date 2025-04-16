using OdaWepApi.Domain.Enums;

namespace OdaWepApi.Domain.DTOs
{
    public class ApartmentDTO
    {
        public int? ApartmentId { get; set; }
        public int? ApartmentType { get; set; }
        public decimal? ApartmentSpace { get; set; }
        public string? ApartmentAddress { get; set; }
        public int? Unittypeid { get; set; }
        public string? UnittypeName { get; set; }
        //    public List<ApartmentRoomsDTO>? ApartmentRooms { get; set; }
        //    public FaceLiftRoomType? RoomType { get; internal set; }
    }
}