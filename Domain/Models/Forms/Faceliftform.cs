using System;
using System.Collections.Generic;

namespace OdaWepApi.Domain.Models.Forms;

public partial class Faceliftform
{
    public int FaceLiftFormId { get; set; }

    public required string FirstName { get; set; }

    public required string PhoneNumber { get; set; }

    public int BedroomsCount { get; set; }

    public int BathroomsCount { get; set; }

    public int KitchenCount { get; set; }
}