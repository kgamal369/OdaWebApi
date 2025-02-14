using System;
using System.Collections.Generic;

namespace OdaWepApi.Domain.Models;

public partial class Paymentplan
{
    public int Paymentplanid { get; set; }

    public string Paymentplanname { get; set; } = null!;

    public int Numberofinstallmentmonths { get; set; }

    public byte[]? Paymentplanicon { get; set; }

    public bool Downpayment { get; set; }

    public decimal? Downpaymentpercentage { get; set; }

    public bool Adminfees { get; set; }

    public decimal? Adminfeespercentage { get; set; }

    public bool Interestrate { get; set; }

    public decimal? Interestrateperyearpercentage { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Installmentbreakdown> Installmentbreakdowns { get; set; } = new List<Installmentbreakdown>();
}
