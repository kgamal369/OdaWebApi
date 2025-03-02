using System;
using System.Collections.Generic;

namespace OdaWepApi.Domain.Models;

public partial class Invoice
{
    public int Invoiceid { get; set; }

    public int? Bookingid { get; set; }

    public DateTime? Createdatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }

    public decimal? Invoiceamount { get; set; }

    public string? Invoicestatus { get; set; }

    public DateTime? Invoiceduedate { get; set; }
}
