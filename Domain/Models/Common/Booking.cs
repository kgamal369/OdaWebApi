using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using OdaWepApi.Domain.Enums;
using OdaWepApi.Domain.Models.FaceLift;
using OdaWepApi.Domain.Models.Forms;
using OdaWepApi.Domain.Models.LocateYourHome_BuildYourKit;

namespace OdaWepApi.Domain.Models.Common;

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
    public virtual ICollection<Customeranswer> Customeranswers { get; set; } = new List<Customeranswer>();

    [JsonIgnore]
    public virtual ICollection<Faceliftroom> Faceliftrooms { get; set; } = new List<Faceliftroom>();

    [JsonIgnore]
    public virtual Paymentmethod? Paymentmethod { get; set; }

    [JsonIgnore]
    public virtual Paymentplan? Paymentplan { get; set; }

    [JsonIgnore]
    public virtual User? User { get; set; }
}
