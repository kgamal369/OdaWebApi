using System.ComponentModel.DataAnnotations;

namespace OdaWepApi.Domain.Models
{
    public partial class User
    {
        [Key]
        public int Userid { get; set; }

        [Required(ErrorMessage = "User Name is required.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "User Name must be between 2 and 20 characters.")]
        public string? Username { get; set; }

        [Required]
        public string? Passwordhash { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "First Name must be between 2 and 20 characters.")]
        public string? Firstname { get; set; }

        public string? Lastname { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Email must be between 2 and 50 characters.")]
        public string? Email { get; set; }

        public string? Phonenumber { get; set; }
        public DateTime? Createdatetime { get; set; }
        public DateTime? Lastmodifieddatetime { get; set; }
        public DateTime? Lastlogin { get; set; }

        [Required]
        public int? Roleid { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public virtual Role? Role { get; set; }
    }
}