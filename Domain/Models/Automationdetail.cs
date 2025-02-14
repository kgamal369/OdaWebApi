namespace OdaWepApi.Domain.Models;

public partial class Automationdetail
{
    public int Automationdetailsid { get; set; }

    public string? Automationdetailsname { get; set; }

    public int? Automationid { get; set; }

    public bool Description { get; set; }

    public DateTime? Createdatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }

    public byte[]? Icon { get; set; }

    public virtual Automation? Automation { get; set; }
}
