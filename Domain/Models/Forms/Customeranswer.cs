using OdaWepApi.Domain.Models.Common;
using System;
using System.Collections.Generic;

namespace OdaWepApi.Domain.Models.Forms;

public partial class Customeranswer
{
    public int Customeranswerid { get; set; }

    public int? Bookingid { get; set; }

    public int? Questionid { get; set; }

    public int? Answerid { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual Answer? Answer { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual Question? Question { get; set; }
}
