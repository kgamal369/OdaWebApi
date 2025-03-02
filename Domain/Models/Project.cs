using System;
using System.Collections.Generic;

namespace OdaWepApi.Domain.Models;

public partial class Project
{
    public int Projectid { get; set; }

    public string? Projectname { get; set; }

    public string? Location { get; set; }

    public string? Amenities { get; set; }

    public int? Totalunits { get; set; }

    public byte[]? Projectlogo { get; set; } // Changed from List<byte[]> to byte[]

    public DateTime? Createdatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }

    public int? Developerid { get; set; }
}
