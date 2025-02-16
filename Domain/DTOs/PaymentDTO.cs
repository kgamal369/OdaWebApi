using System;
namespace OdaWepApi.Domain.DTOs
{
    public class PaymentDTO
    {
    public int Paymentplanid { get; set; }

    public string Paymentplanname { get; set; } = null!;

    public int Numberofinstallmentmonths { get; set; }

    public bool Downpayment { get; set; }

    public decimal? Downpaymentpercentage { get; set; }

    public bool Adminfees { get; set; }

    public decimal? Adminfeespercentage { get; set; }

    public bool Interestrate { get; set; }

    public decimal? Interestrateperyearpercentage { get; set; }

    public Boolean EqualPayment {get; set;} // 1 =Equal ; 0 = false

    public List<InstallmentDTO> InstallmentDTO {get;set;}  

    }
}