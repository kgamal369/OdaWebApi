using System;
using System.Collections.Generic;

namespace OdaWepApi.Domain.Models;

public partial class Plandetail
{
    public int Plandetailsid { get; set; }

    public string? Plandetailsname { get; set; }

    public string? Plandetailstype { get; set; }

    public int? Planid { get; set; }

    public string? Description { get; set; }

    public DateTime? Createdatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }

    public virtual Plan? Plan { get; set; }
}
