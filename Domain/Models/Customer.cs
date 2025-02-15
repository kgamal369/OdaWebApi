namespace OdaWepApi.Domain.Models;

public partial class Customer
{
    public int Customerid { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string? Email { get; set; }

    public string? Phonenumber { get; set; }

    public string? Address { get; set; }

    public DateTime? Createdatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
