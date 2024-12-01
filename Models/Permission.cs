using System.ComponentModel.DataAnnotations;

namespace OdaWepApi.Models
{
    public partial class Permission
    {
        [Key]
        public int Permissionid { get; set; }

        [Required(ErrorMessage = "Entities Name is required.")]
        [EnumDataType(typeof(Enum.EntitiesNames), ErrorMessage = "Invalid Entities.")]
        public string? Entityname { get; set; }

        [Required(ErrorMessage = "Permission Actions is required.")]
        [EnumDataType(typeof(Enum.PermissionActions), ErrorMessage = "Invalid Actions.")]
        public string? Action { get; set; }

        [Required]
        public int? Roleid { get; set; }

        public DateTime? Createdatetime { get; set; }
        public DateTime? Lastmodifieddatetime { get; set; }

        public virtual Role? Role { get; set; }
    }
}
