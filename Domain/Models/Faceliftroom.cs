using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using OdaWepApi.Domain.Enums;

namespace OdaWepApi.Domain.Models;

public partial class Faceliftroom
{
    public int Roomid { get; set; }

    public FaceLiftRoomType Roomtype { get; set; }

    public int? Automationid { get; set; }

    public DateTime? Createddatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }

    public int? Bookingid { get; set; }

    public virtual Automation? Automation { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual ICollection<FaceliftroomAddonperrequest> FaceliftroomAddonperrequests { get; set; } = new List<FaceliftroomAddonperrequest>();

    public virtual ICollection<FaceliftroomAddon> FaceliftroomAddons { get; set; } = new List<FaceliftroomAddon>();
}
