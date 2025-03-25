using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using OdaWepApi.Domain.Enums;

namespace OdaWepApi.Domain.Models;

public partial class Faceliftaddon
{
    public int Addonid { get; set; }

    public string? Addonname { get; set; }

    public string? Addongroup { get; set; }

    public decimal? Price { get; set; }

    public string? Description { get; set; }

    public string? Brand { get; set; }

    public DateTime? Createddatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }

    public string? Unitormeter { get; set; }
    public int? Displayorder { get; set; }

    public FaceLiftRoomType Faceliftroomtype { get; set; }

    public virtual ICollection<FaceliftroomAddon> FaceliftroomAddons { get; set; } = new List<FaceliftroomAddon>();
}
