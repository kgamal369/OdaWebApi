using System.ComponentModel.DataAnnotations;

namespace OdaWepApi.Domain.Models
{
    public class Apartmentaddon
    {
        [Key]
        public int Apartmentaddonsid { get; set; }

        public int? Apartmentid { get; set; }
        public int? Addonid { get; set; }
        public bool? Availableaddons { get; set; }
        public bool? Assignedaddons { get; set; }
        public int? Maxavailable { get; set; }
        public int? Installedamount { get; set; }
        public DateTime? Createdatetime { get; set; }
        public DateTime? Lastmodifieddatetime { get; set; }

        public virtual Addon? Addon { get; set; }
        public virtual Apartment? Apartment { get; set; }
    }
}
