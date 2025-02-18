using System;
namespace OdaWepApi.Domain.DTOs
{
    public class InstallmentDTO
    {
        public int Installmentmonth { get; set; }
        public decimal Installmentpercentage { get; set; }
        public decimal Installmentvalue { get; set; }

    }
}