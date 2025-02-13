namespace OdaWepApi.Domain.Models;

public partial class ApartmentAddon
{
    public int Apartmentid { get; set; }

    public int Addonid { get; set; }

    public int Quantity { get; set; }

    public virtual Addon Addon { get; set; } = null!;

    public virtual Apartment Apartment { get; set; } = null!;

}
