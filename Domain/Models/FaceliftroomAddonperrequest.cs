using System;
using System.Collections.Generic;

namespace OdaWepApi.Domain.Models;

public partial class FaceliftroomAddonperrequest
{
    public int Apartmentid { get; set; }

    public int Addperrequestid { get; set; }

    public int? Quantity { get; set; }

    public virtual Faceliftaddperrequest Addperrequest { get; set; } = null!;

    public virtual Apartment Apartment { get; set; } = null!;
}
