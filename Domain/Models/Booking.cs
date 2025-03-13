using System.Text.Json.Serialization;
using OdaWepApi.Domain.Enums;

namespace OdaWepApi.Domain.Models;

public partial class Booking
{
    public int Bookingid { get; set; }

    public int? Customerid { get; set; }

    public int? Apartmentid { get; set; }

    public int? Paymentmethodid { get; set; }

    public int? Paymentplanid { get; set; }

    public DateTime Createdatetime { get; set; }

    public DateTime Lastmodifieddatetime { get; set; }

    public Bookingstatus Bookingstatus { get; set; }

    public int? Userid { get; set; }

    public decimal Totalamount { get; set; }

    [JsonIgnore]
    public virtual Apartment? Apartment { get; set; }

    [JsonIgnore]
    public virtual Customer? Customer { get; set; }

    [JsonIgnore]
    public virtual Paymentmethod? Paymentmethod { get; set; }

    [JsonIgnore]
    public virtual Paymentplan? Paymentplan { get; set; }

    [JsonIgnore]
    public virtual User? User { get; set; }

    [JsonIgnore]
    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
    [JsonIgnore]
    public virtual ICollection<Faceliftroom> FaceliftRooms { get; set; } = new List<Faceliftroom>();
}
