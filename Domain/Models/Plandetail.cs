using OdaWepApi.Domain.Enums;

namespace OdaWepApi.Domain.Models;

public partial class Plandetail
{
    public int Plandetailsid { get; set; }

    public string? Plandetailsname { get; set; }

    public PlanDetailsType Plandetailstype { get; set; }

    public int? Planid { get; set; }

    public int? DisplayOrder { get; set; }

    public string? Description { get; set; }

    public DateTime? Createdatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }

    public int? Stars { get; set; }

    public virtual Plan? Plan { get; set; }

}
