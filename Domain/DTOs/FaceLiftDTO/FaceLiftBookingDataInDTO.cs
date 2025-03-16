using OdaWepApi.Domain.Models;

namespace OdaWepApi.Domain.DTOs.FaceLiftDTO
{
    public class FaceLiftBookingDataInDTO
    {
        
        public List<int> AddonPerRequestIDs { get; set; }
        public int? AutomationID { get; set; }
        public Customer CustomerInfo { get; set; }
        public int PaymentPlanID { get; set; }
    }
}