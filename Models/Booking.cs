using System.ComponentModel.DataAnnotations;

namespace OdaWepApi.Models
{
    public class Booking
    {
        [Key]
        public int Bookingid { get; set; }

        [Required(ErrorMessage = "Customer Id is required.")]
        public int? Customerid { get; set; }

        [Required(ErrorMessage = "Apartment Id is required.")]
        public int? Apartmentid { get; set; }

        public DateTime? Createdatetime { get; set; }
        public DateTime? Lastmodifieddatetime { get; set; }

        [Required(ErrorMessage = "Booking Status is required.")]
        [EnumDataType(typeof(Enum.BookingStatus), ErrorMessage = "Invalid Booking Status.")]
        public string? Bookingstatus { get; set; }

        public int? Userid { get; set; }

        [Required(ErrorMessage = "Total Amount is required.")]
        [Range(10, 10000000, ErrorMessage = "Total Amount must be between 10 and 10M EGP.")]
        public decimal? Totalamount { get; set; }

        public virtual Apartment? Apartment { get; set; }
        public virtual Customer? Customer { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
        public virtual User? User { get; set; }
    }
}
