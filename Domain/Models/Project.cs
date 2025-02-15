using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OdaWepApi.Domain.Models;

public partial class Project
{
    public int Projectid { get; set; }

    public string? Projectname { get; set; }

    public string? Location { get; set; }

    public string? Amenities { get; set; }

    public int? Totalunits { get; set; }

    public byte[]? Projectlogo { get; set; } // Changed from List<byte[]> to byte[]

    public DateTime? Createdatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }

    public int? Developerid { get; set; }

    [JsonIgnore]
    [ForeignKey("Developerid")]
    public virtual Developer? Developer { get; set; }

    [JsonIgnore]
    public virtual ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();
}
