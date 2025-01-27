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

    public List<byte[]>? Projectlogo { get; set; }

    public DateTime? Createdatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }

    public int? Developerid { get; set; }

    public virtual ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();

    public virtual Developer? Developer { get; set; }
}
