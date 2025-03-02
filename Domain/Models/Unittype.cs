using System;
using System.Collections.Generic;

namespace OdaWepApi.Domain.Models;

public partial class Unittype
{
    public int Unittypeid { get; set; }

    public string UnittypeName { get; set; } = null!;

    public virtual ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();
}
