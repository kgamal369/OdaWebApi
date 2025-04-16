using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OdaWepApi.Domain.Models.FaceLift;

public partial class FaceliftroomAddon
{
    public int Roomid { get; set; }

    public int Addonid { get; set; }

    public int Quantity { get; set; }

    [JsonIgnore]
    public virtual Faceliftaddon Addon { get; set; } = null!;

    [JsonIgnore]
    public virtual Faceliftroom Room { get; set; } = null!;
}
