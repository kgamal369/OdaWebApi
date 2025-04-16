using OdaWepApi.Domain.Models.LocateYourHome_BuildYourKit;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OdaWepApi.Domain.Models.FaceLift;

public partial class FaceliftroomAddonperrequest
{
    public int Apartmentid { get; set; }

    public int Addperrequestid { get; set; }

    public int? Quantity { get; set; }

    [JsonIgnore]
    public virtual Faceliftaddperrequest Addperrequest { get; set; } = null!;

    [JsonIgnore]
    public virtual Apartment Apartment { get; set; } = null!;
}
