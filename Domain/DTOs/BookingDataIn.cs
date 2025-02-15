using OdaWepApi.Domain.Models;

namespace OdaWepApi.Domain.DTOs
{
    public class BookingDataIn
    {
        public int DeveloperID { get; set; }
        public int ProjectID { get; set; }
        public int? ApartmentId { get; set; }
        public int? ApartmentType { get; set; }
        public decimal? ApartmentSpace { get; set; }
        public int PlanID { get; set; }
        public List<AddonSelection> Addons { get; set; }
        public List<int> AddonPerRequestIDs { get; set; }
        public int? AutomationID { get; set; }
        public Customer CustomerInfo { get; set; }
        public int PaymentPlanID { get; set; }
    }
}
