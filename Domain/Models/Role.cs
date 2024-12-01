using System.ComponentModel.DataAnnotations;

namespace OdaWepApi.Domain.Models
{
    public partial class Role
    {
        [Key]
        public int Roleid { get; set; }

        [Required(ErrorMessage = "Role Name is required.")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Role Name must be between 2 and 30 characters.")]
        public string? Rolename { get; set; }

        public string? Description { get; set; }
        public DateTime? Createdatetime { get; set; }
        public DateTime? Lastmodifieddatetime { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
