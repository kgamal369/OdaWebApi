using System;
using System.Collections.Generic;

namespace OdaWepApi.Domain.Models;

public partial class Odaambassador
{
    public string Ownername { get; set; } = null!;

    public string? Ownerphonenumber { get; set; }

    public decimal? Ownerunitarea { get; set; }

    public string? Ownerunitlocation { get; set; }

    public string? Ownerdeveloper { get; set; }

    public decimal? Ownerselectbudget { get; set; }

    public string Referralname { get; set; } = null!;

    public string? Referralphonenumber { get; set; }

    public string? Referralemail { get; set; }

    public string? Referralclientstatue { get; set; }

    public int Odaambassadorid { get; set; }
}
