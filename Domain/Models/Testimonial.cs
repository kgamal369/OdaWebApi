using System;
using System.Collections.Generic;

namespace OdaWepApi.Domain.Models;

public partial class Testimonial
{
    public int Testimonialsid { get; set; }

    public string? Testimonialsname { get; set; }

    public string? Testimonialstitle { get; set; }

    public string? Description { get; set; }

    public List<byte[]>? Testimonialsphoto { get; set; }

    public DateTime? Createdatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }
}
