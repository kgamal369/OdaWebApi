using System;
using System.Collections.Generic;

namespace OdaWepApi.Domain.Models;

public partial class Projecttype
{
    public int Projecttypeid { get; set; }

    public string Projecttypedetail { get; set; } = null!;

    public string Projecttypename { get; set; } = null!;
}
