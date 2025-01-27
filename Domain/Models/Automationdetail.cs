using System;
using System.Collections.Generic;

namespace OdaWepApi.Domain.Models;

public partial class Automationdetail
{
    public int Automationdetailsid { get; set; }

    public string? Automationdetailsname { get; set; }

    public int? Automationid { get; set; }

    public string? Description { get; set; }

    public DateTime? Createdatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }

    public virtual Automation? Automation { get; set; }
}
