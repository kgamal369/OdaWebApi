using OdaWepApi.Domain.Enums;

namespace OdaWepApi.Domain.DTOs
{
    public class AddonDetail
    {
        public int AddonID { get; set; }
        public string AddonName { get; set; }
        public string? Addongroup { get; set; }
        public string? Description { get; set; }
        public UnitOrMeterType Unitormeter { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int DisplayOrder { get; set; }
    }
}
