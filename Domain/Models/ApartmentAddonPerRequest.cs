
namespace OdaWepApi.Domain.Models;

public partial class ApartmentAddonperrequest
{
    public int Apartmentid { get; set; }

    public int Addperrequestid { get; set; }

    public int? Quantity { get; set; }
    public virtual Addperrequest Addperrequest { get; set; } = null!;

    public virtual Apartment Apartment { get; set; } = null!;

}
