using System.ComponentModel.DataAnnotations;

namespace OdaWepApi.Models
{
    public class Addon
    {
        [Key]
        public int Addonid { get; set; }

        [Required(ErrorMessage = "Addon Name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "AddOn Name must be between 2 and 100 characters.")]
        public string? Addonname { get; set; }

        [Required(ErrorMessage = "AddOn Type is required.")]
        [EnumDataType(typeof(Enum.AddOnType), ErrorMessage = "Invalid AddOn Type.")]
        public string? Addontype { get; set; }

        [Required(ErrorMessage = "Price per unit is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price per unit must be greater than 0.")]
        public decimal? Priceperunit { get; set; }

        public string? Description { get; set; }
        public string? Brand { get; set; }
        public DateTime? Createddatetime { get; set; }
        public DateTime? Lastmodifieddatetime { get; set; }
        public virtual ICollection<Apartmentaddon> Apartmentaddons { get; set; } = new List<Apartmentaddon>();
    }
}
