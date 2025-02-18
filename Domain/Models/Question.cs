using System.Text.Json.Serialization;

namespace OdaWepApi.Domain.Models;

public partial class Question
{
    public int Questionsid { get; set; }

    public string? Questionname { get; set; }

    public int? Answer { get; set; }

    public int? Bookingid { get; set; }

    [JsonIgnore]
    public virtual Booking? Booking { get; set; }
}
