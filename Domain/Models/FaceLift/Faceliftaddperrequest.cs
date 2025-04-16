using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OdaWepApi.Domain.Models.FaceLift;

public partial class Faceliftaddperrequest
{
    public int Addperrequestid { get; set; }

    public string? Addperrequestname { get; set; }

    public decimal? Price { get; set; }

    public string? Description { get; set; }

    public DateTime? Createddatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }
    public int? Displayorder { get; set; }

    [JsonIgnore]
    public virtual ICollection<FaceliftroomAddonperrequest> FaceliftroomAddonperrequests { get; set; } = new List<FaceliftroomAddonperrequest>();
}
