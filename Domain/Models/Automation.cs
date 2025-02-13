using System;
using System.Collections.Generic;

namespace OdaWepApi.Domain.Models;

public partial class Automation
{
    public int Automationid { get; set; }

    public string? Automationname { get; set; }

    public string? Description { get; set; }

    public DateTime? Createdatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }
    public virtual ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();

    public virtual ICollection<Automationdetail> Automationdetails { get; set; } = new List<Automationdetail>();

}
