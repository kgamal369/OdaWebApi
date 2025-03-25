using System;
using System.Collections.Generic;

namespace OdaWepApi.Domain.Models;

public partial class User
{
    public int Userid { get; set; }

    public string? Username { get; set; }

    public string? Passwordhash { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string? Email { get; set; }

    public string? Phonenumber { get; set; }

    public DateTime? Createdatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }

    public DateTime? Lastlogin { get; set; }

    public int? Roleid { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
