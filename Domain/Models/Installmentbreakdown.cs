using System;
using System.Collections.Generic;

namespace OdaWepApi.Domain.Models;

public partial class Installmentbreakdown
{
    public int Breakdownid { get; set; }

    public int? Paymentplanid { get; set; }

    public int Installmentmonth { get; set; }

    public decimal Installmentpercentage { get; set; }

    public DateTime? Createddatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }

    public virtual Paymentplan? Paymentplan { get; set; }
}
