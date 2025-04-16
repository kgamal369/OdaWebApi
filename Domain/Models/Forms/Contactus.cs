using System;
using System.Collections.Generic;

namespace OdaWepApi.Domain.Models.Forms;

public partial class Contactus
{
    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Phonenumber { get; set; } = null!;

    public string? Email { get; set; }

    public string Comments { get; set; } = null!;

    public int Contactusid { get; set; }
}
