using System.ComponentModel.DataAnnotations;

namespace OdaWepApi.Models
{
    public partial class Customer
    {
        [Key]
        public int Customerid { get; set; }

        [Required(ErrorMessage = "Customer First Name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Customer First Name must be between 2 and 100 characters.")]
        public string? Firstname { get; set; }

        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public string? Phonenumber { get; set; }
        public string? Address { get; set; }
        public DateTime? Createdatetime { get; set; }
        public DateTime? Lastmodifieddatetime { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
