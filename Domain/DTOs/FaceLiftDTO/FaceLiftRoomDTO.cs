using OdaWepApi.Domain.Enums;

namespace OdaWepApi.Domain.DTOs.FaceLiftDTO
{
    public class FaceLiftRoomDTO
    {
        public FaceLiftRoomType RoomType { get; set; }
        public List<AddonSelection> AddonSelectionsList { get; set; }
    }
}