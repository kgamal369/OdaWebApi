using System;
using System.Collections.Generic;

namespace OdaWepApi.Domain.Models;

public partial class FaceliftroomAddonperrequest
{
    public int Roomid { get; set; }

    public int Addperrequestid { get; set; }

    public int? Quantity { get; set; }

    public virtual Faceliftaddperrequest Addperrequest { get; set; } = null!;

    public virtual Faceliftroom Room { get; set; } = null!;
}
