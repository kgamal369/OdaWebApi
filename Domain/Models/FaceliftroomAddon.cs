using System;
using System.Collections.Generic;

namespace OdaWepApi.Domain.Models;

public partial class FaceliftroomAddon
{
    public int Roomid { get; set; }

    public int Addonid { get; set; }

    public int Quantity { get; set; }

    public virtual Faceliftaddon Addon { get; set; } = null!;

    public virtual Faceliftroom Room { get; set; } = null!;
}
