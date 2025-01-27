using System;
using System.Collections.Generic;

namespace OdaWepApi.Domain.Models;

public partial class Plan
{
    public int Planid { get; set; }

    public string? Planname { get; set; }

    public decimal? Pricepermeter { get; set; }

    public string? Description { get; set; }

    public List<byte[]>? Planphoto { get; set; }

    public DateTime? Createdatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }

    public virtual ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();

    public virtual ICollection<Plandetail> Plandetails { get; set; } = new List<Plandetail>();
}
