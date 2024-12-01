using System;
using System.ComponentModel.DataAnnotations;

namespace OdaWepApi.Domain.Models
{
    public partial class Package
    {
        [Key]
        public int Packageid { get; set; }

        [Required(ErrorMessage = "Package Name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Package Name must be between 2 and 100 characters.")]
        public string? Packagename { get; set; }

        public int? Apartmentid { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public bool? Assignedpackage { get; set; }
        public DateTime? Createdatetime { get; set; }
        public DateTime? Lastmodifieddatetime { get; set; }

        public virtual Apartment? Apartment { get; set; }
    }
}
