﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using OdaWepApi.Domain.Enums;

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

    public string? Apartmentaddress { get; set; }

    public int? Developerid { get; set; }

    public int? Unittypeid { get; set; }

    [JsonIgnore]
    public virtual ICollection<ApartmentAddonperrequest> ApartmentAddonperrequests { get; set; } = new List<ApartmentAddonperrequest>();

    [JsonIgnore]
    public virtual ICollection<ApartmentAddon> ApartmentAddons { get; set; } = new List<ApartmentAddon>();

    [JsonIgnore]
    public virtual Automation? Automation { get; set; }

    [JsonIgnore]
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Developer? Developer { get; set; }

    [JsonIgnore]
    public virtual ICollection<FaceliftroomAddonperrequest> FaceliftroomAddonperrequests { get; set; } = new List<FaceliftroomAddonperrequest>();

    [JsonIgnore]
    public virtual ICollection<Faceliftroom> Faceliftrooms { get; set; } = new List<Faceliftroom>();

    public virtual Plan? Plan { get; set; }

    public virtual Unittype? Unittype { get; set; }
}
