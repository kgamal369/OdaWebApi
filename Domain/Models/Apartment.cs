using System.ComponentModel.DataAnnotations;
using OdaWepApi.Domain.Enums;

namespace OdaWepApi.Domain.Models
{
    public class Apartment
    {
        [Key]
        public int Apartmentid { get; set; }

        [Required(ErrorMessage = "Apartment Name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Apartment Name must be between 2 and 100 characters.")]
        public string? Apartmentname { get; set; }

        [Required(ErrorMessage = "Apartment Type is required.")]
        [EnumDataType(typeof(ApartmentType), ErrorMessage = "Invalid Apartment Type.")]
        public string? Apartmenttype { get; set; }

        [Required(ErrorMessage = "Apartment status is required.")]
        [EnumDataType(typeof(ApartmentStatus), ErrorMessage = "Invalid Apartment Status.")]
        public string? Apartmentstatus { get; set; }

        [Required(ErrorMessage = "Apartment space is required.")]
        [Range(10, 1000, ErrorMessage = "Space must be between 10 and 1,000 square meters.")]
        public decimal? Apartmentspace { get; set; }

        public string? Description { get; set; }
        public List<byte[]>? Apartmentphotos { get; set; }
        public int? Projectid { get; set; }
        public int? Floornumber { get; set; }
        public DateTime? Availabilitydate { get; set; }
        public DateTime? Createddatetime { get; set; }
        public DateTime? Lastmodifieddatetime { get; set; }
        public virtual ICollection<Apartmentaddon> Apartmentaddons { get; set; } = new List<Apartmentaddon>();
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public virtual ICollection<Package> Packages { get; set; } = new List<Package>();
        public virtual Project? Project { get; set; }

        // Virtual method to calculate total price
        public virtual decimal CalculateApartmentTotalPrice()
        {
            // Sum of assigned package price
            var totalPackagePrice = Packages?.Where(p => p.Assignedpackage == true)
                                             .Sum(p => p.Price) ?? 0;

            // Sum of installed addons price
            var totalAddonPrice = Apartmentaddons?
                .Where(aa => aa.Assignedaddons == true)
                .Sum(aa => (aa.Installedamount ?? 0) * (aa.Addon?.Priceperunit ?? 0)) ?? 0;

            return totalPackagePrice + totalAddonPrice;
        }
    }
}
