using System.Text.Json.Serialization;
using OdaWepApi.Domain.Enums;
using OdaWepApi.Domain.Models.Common;
using OdaWepApi.Domain.Models.LocateYourHome_BuildYourKit;

namespace OdaWepApi.Domain.Models.FaceLift;

public partial class Faceliftroom
{
    public int Roomid { get; set; }

    public FaceLiftRoomType Roomtype { get; set; }

    public int? Automationid { get; set; }

    public DateTime? Createddatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }

    public int? Bookingid { get; set; }

    public int? Apartmentid { get; set; }

    [JsonIgnore]
    public virtual Apartment? Apartment { get; set; }

    [JsonIgnore]
    public virtual Automation? Automation { get; set; }

    [JsonIgnore]
    public virtual Booking? Booking { get; set; }

    [JsonIgnore]
    public virtual ICollection<FaceliftroomAddon> FaceliftroomAddons { get; set; } = new List<FaceliftroomAddon>();
}
