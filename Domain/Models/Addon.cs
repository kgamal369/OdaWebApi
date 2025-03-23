using OdaWepApi.Domain.Enums;
namespace OdaWepApi.Domain.Models;

public partial class Addon
{
    public int Addonid { get; set; }

    public string? Addonname { get; set; }

    public string? Addongroup { get; set; }

    public decimal? Price { get; set; }

    public string? Description { get; set; }

    public string? Brand { get; set; }
    
    public int? DisplayOrder { get; set; }

    public DateTime? Createddatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }

    public UnitOrMeterType Unitormeter { get; set; }

    public virtual ICollection<ApartmentAddon> ApartmentAddons { get; set; } = new List<ApartmentAddon>();

}
