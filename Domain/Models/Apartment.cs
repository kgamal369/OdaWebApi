using OdaWepApi.Domain.Enums;
using System;
using System.Collections.Generic;

namespace OdaWepApi.Domain.Models;

public partial class Apartment
{
    public int Apartmentid { get; set; }

    public string? Apartmentname { get; set; }

    public ApartmentType Apartmenttype { get; set; }

    public Apartmentstatus Apartmentstatus { get; set; }

    public decimal? Apartmentspace { get; set; }

    public string? Description { get; set; }

    public List<byte[]>? Apartmentphotos { get; set; }

    public int? Projectid { get; set; }

    public int? Floornumber { get; set; }

    public DateTime? Availabilitydate { get; set; }

    public DateTime? Createddatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }

    public int? Planid { get; set; }

    public int? Automationid { get; set; }

    public virtual ICollection<ApartmentAddonperrequest> ApartmentAddonperrequests { get; set; } = new List<ApartmentAddonperrequest>();

    public virtual ICollection<ApartmentAddon> ApartmentAddons { get; set; } = new List<ApartmentAddon>();

    public virtual Automation? Automation { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Plan? Plan { get; set; }

    public virtual Project? Project { get; set; }
}
