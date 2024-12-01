using System.ComponentModel.DataAnnotations;
using OdaWepApi.Domain.Enums;

namespace OdaWepApi.Domain.Models
{
    public partial class Invoice
    {
        [Key]
        public int Invoiceid { get; set; }

        [Required]
        public int? Bookingid { get; set; }

        public DateTime? Createdatetime { get; set; }
        public DateTime? Lastmodifieddatetime { get; set; }

        [Required(ErrorMessage = "Invoice Amount is required.")]
        [Range(10, 10000000, ErrorMessage = "Invoice amount must be between 10 and 10M EGP.")]
        public decimal? Invoiceamount { get; set; }

        [Required(ErrorMessage = "Invoice Status is required.")]
        [EnumDataType(typeof(InvoiceStatus), ErrorMessage = "Invalid Invoice Status.")]
        public string? Invoicestatus { get; set; }

        public DateTime? Invoiceduedate { get; set; }

        public virtual Booking? Booking { get; set; }
    }
}
