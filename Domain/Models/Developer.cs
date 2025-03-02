using System;
using System.Collections.Generic;

namespace OdaWepApi.Domain.Models;

public partial class Developer
{
    public int Developerid { get; set; }

    public string? Developername { get; set; }

    public string? Description { get; set; }

    public List<byte[]>? Developerlogo { get; set; }

    public DateTime? Createdatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }

    public virtual ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();
}
