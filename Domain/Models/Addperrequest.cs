﻿using System;
using System.Collections.Generic;

namespace OdaWepApi.Domain.Models;

public partial class Addperrequest
{
    public int Addperrequestid { get; set; }

    public string? Addperrequestname { get; set; }

    public decimal? Price { get; set; }

    public string? Description { get; set; }

    public DateTime? Createddatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }

    public int? Displayorder { get; set; }

    public virtual ICollection<ApartmentAddonperrequest> ApartmentAddonperrequests { get; set; } = new List<ApartmentAddonperrequest>();
}
