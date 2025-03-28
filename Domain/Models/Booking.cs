﻿using System;
using System.Collections.Generic;
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

    public virtual Apartment? Apartment { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Customeranswer> Customeranswers { get; set; } = new List<Customeranswer>();

    public virtual ICollection<Faceliftroom> Faceliftrooms { get; set; } = new List<Faceliftroom>();

    public virtual Paymentmethod? Paymentmethod { get; set; }

    public virtual Paymentplan? Paymentplan { get; set; }

    public virtual User? User { get; set; }
}
