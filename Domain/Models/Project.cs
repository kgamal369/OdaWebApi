using System.ComponentModel.DataAnnotations;

namespace OdaWepApi.Domain.Models
{
    public partial class Project
    {
        [Key]
        public int Projectid { get; set; }

        [Required(ErrorMessage = "Project Name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Project Name must be between 2 and 100 characters.")]
        public string? Projectname { get; set; }

        public string? Location { get; set; }
        public string? Amenities { get; set; }
        public int? Totalunits { get; set; }
        public List<byte[]>? Projectlogo { get; set; }
        public DateTime? Createdatetime { get; set; }
        public DateTime? Lastmodifieddatetime { get; set; }
        public virtual ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();
    }
}
