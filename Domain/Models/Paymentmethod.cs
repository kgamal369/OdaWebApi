using System;
using System.Collections.Generic;

namespace OdaWepApi.Domain.Models;

public partial class Paymentmethod
{
    public int Paymentmethodid { get; set; }

    public string? Paymentmethodname { get; set; }

    public List<byte[]>? Paymentmethodphotos { get; set; }

    public string? Description { get; set; }

    public decimal? Depositpercentage { get; set; }

    public int? Numberofinstallments { get; set; }

    public DateTime? Createddatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
