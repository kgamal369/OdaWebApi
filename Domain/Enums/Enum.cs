namespace OdaWepApi.Domain.Enums
{
    public enum PackageType
    {
        Golden,
        Silver,
        Platinum
    }

    public enum AddOnType
    {
        AirConditioning1_5hp,
        AirConditioning2_5hp,
        SmartLighting,
        Boilers,
        SolarHeating,
        ShowerTemperedGlass,
        ShuttersAluminum
    }

    public enum ApartmentType
    {
        Template,
        Standalone,
        Project
    }

    public enum ApartmentStatus
    {
        ForSale,
        Template,
        Draft,
        InProgress,
        InReview,
        Booked,
        Completed
    }
    public enum InvoiceStatus
    {
        Pending,
        PartiallyPaid,
        Paid,
        Overdue,
        Cancelled
    }

    public enum PaymentMethod
    {
        Card,
        Cash,
        Invoice,
        BankTransfer
    }

    public enum BookingStatus
    {
        Pending,
        InProgress,
        UnderReview,
        Approved,
        Rejected,
        Finalized,
        Cancelled
    }

    public enum PaymentStatus
    {
        Pending,
        PartiallyPaid,
        Paid,
        Overdue,
        Cancelled
    }
    public enum RoleType
    {
        Admin,
        Sales,
        Customer
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
