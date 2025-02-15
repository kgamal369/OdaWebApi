using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using OdaWepApi.Domain.Enums;

namespace OdaWepApi.Domain.Models;

public partial class Apartment
{
    public int Apartmentid { get; set; }

    public string? Apartmentname { get; set; }

    public ApartmentType Apartmenttype { get; set; }

    public Apartmentstatus Apartmentstatus { get; set; }

    public decimal? Apartmentspace { get; set; }

    public string? Description { get; set; }

    public byte[]? Apartmentphotos { get; set; } // Changed from List<byte[]> to byte[]

    public int? Projectid { get; set; }

    public int? Floornumber { get; set; }

    public DateTime? Availabilitydate { get; set; }

    public DateTime? Createddatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }

    public int? Planid { get; set; }

    public int? Automationid { get; set; }

    [JsonIgnore]
    public virtual ICollection<ApartmentAddonperrequest> ApartmentAddonperrequests { get; set; } = new List<ApartmentAddonperrequest>();

    [JsonIgnore]
    public virtual ICollection<ApartmentAddon> ApartmentAddons { get; set; } = new List<ApartmentAddon>();

    [JsonIgnore]
    [ForeignKey("Automationid")]
    public virtual Automation? Automation { get; set; }

    [JsonIgnore]
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    [JsonIgnore]
    [ForeignKey("Planid")]
    public virtual Plan? Plan { get; set; }

    [JsonIgnore]
    [ForeignKey("Projectid")]
    public virtual Project? Project { get; set; }
}
