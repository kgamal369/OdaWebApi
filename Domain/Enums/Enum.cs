namespace OdaWepApi.Domain.Enums
{
    public enum UnitOrMeterType
    {
        Unit,
        Meter
    }
    public enum RoleType
    {
        Admin,
        Sales,
        Customer
    }

    public enum PlanDetailsType
    {
        Foundation,
        Decoration
    }

    public enum ApartmentType
    {
        Project,
        Kit
    }

    public enum Apartmentstatus
    {
        ForSale,
        InProgress,
        InReview
    }
    public enum Bookingstatus
    {
        Pending,
        InProgress,
        Confirmed,
        Cancelled,
        Completed
    }
    public enum EntitiesNames
    {
        Addon,
        Apartment,
        Apartmentaddon,
        Booking,
        Customer,
        Enum,
        Invoices,
        Package,
        Permission,
        Project,
        Role,
        User
    }

    public enum PermissionActions
    {
        Get,
        Remove,
        Add,
        Update
    }
}
