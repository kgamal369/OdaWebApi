namespace OdaWepApi.Models
{
    public class Enum
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
            Booked,
            InReview,
            InProgress,
            Template,
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

    }
}
