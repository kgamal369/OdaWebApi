using OdaWepApi.Domain.Enums;
using OdaWepApi.Domain.Models;

namespace OdaWepApi.Domain.DTOs
{
    public class BookingDataOut
    {
        public int BookingID { get; set; }
        public int? DeveloperID { get; set; }
        public int? ProjectID { get; set; }
        public ApartmentDTO ApartmentDTO { get; set; }
        public int PlanID { get; set; }
        public string PlanName { get; set; }
        public List<AddonDetail> Addons { get; set; }
        public int? AutomationID { get; set; }
        public List<AddonPerRequestDetail>? AddonPerRequests { get; set; }
        public List<CustomerAnswersOutDTO>? CustomerAnswers { get; set; }
        public Customer CustomerInfo { get; set; }
        public PaymentDTO paymentDTO { get; set; }
        public decimal TotalPlanPrice { get; set; }
        public decimal SumOfTotalAddonPrices { get; set; }
        public decimal TotalAirconditionerPrice { get; set; }
        public decimal TotalAmount_Addons_plan { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
