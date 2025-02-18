using OdaWepApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace OdaWepApi.Infrastructure;

public partial class OdaDbContext : DbContext
{
    public OdaDbContext()
    {
    }

    public OdaDbContext(DbContextOptions<OdaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Addon> Addons { get; set; }

    public virtual DbSet<Addperrequest> Addperrequests { get; set; }

    public virtual DbSet<Apartment> Apartments { get; set; }

    public virtual DbSet<ApartmentAddon> ApartmentAddons { get; set; }

    public virtual DbSet<ApartmentAddonperrequest> ApartmentAddonperrequests { get; set; }

    public virtual DbSet<Automation> Automations { get; set; }

    public virtual DbSet<Automationdetail> Automationdetails { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Developer> Developers { get; set; }

    public virtual DbSet<Installmentbreakdown> Installmentbreakdowns { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Paymentmethod> Paymentmethods { get; set; }

    public virtual DbSet<Paymentplan> Paymentplans { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Plan> Plans { get; set; }

    public virtual DbSet<Plandetail> Plandetails { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Testimonial> Testimonials { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=dpg-cuc1s39opnds738s419g-a.oregon-postgres.render.com;Database=odadb;Username=odadb_user;Password=iwiEqjZ2mwcqFuREbb8U1GNTyfxKbgGw;Port=5432;SslMode=Require;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Addon>(entity =>
        {
            entity.HasKey(e => e.Addonid).HasName("addons_pkey");

            entity.ToTable("addons");

            entity.Property(e => e.Addonid).HasColumnName("addonid");
            entity.Property(e => e.Addongroup)
                .HasMaxLength(50)
                .HasColumnName("addongroup");
            entity.Property(e => e.Addonname)
                .HasMaxLength(255)
                .HasColumnName("addonname");
            entity.Property(e => e.Brand).HasColumnName("brand");
            entity.Property(e => e.Createddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddatetime");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.Unitormeter)
                .HasMaxLength(255)
                .HasConversion<string>() // Convert enum to string
                .HasColumnName("unitormeter");
        });

        modelBuilder.Entity<Addperrequest>(entity =>
        {
            entity.HasKey(e => e.Addperrequestid).HasName("addperrequest_pkey");

            entity.ToTable("addperrequest");

            entity.Property(e => e.Addperrequestid).HasColumnName("addperrequestid");
            entity.Property(e => e.Addperrequestname)
                .HasMaxLength(255)
                .HasColumnName("addperrequestname");
            entity.Property(e => e.Createddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddatetime");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
        });

        modelBuilder.Entity<Apartment>(entity =>
        {
            entity.HasKey(e => e.Apartmentid).HasName("apartment_pkey");

            entity.ToTable("apartment");

            entity.Property(e => e.Apartmentid).HasColumnName("apartmentid");
            entity.Property(e => e.Apartmentname)
                .HasMaxLength(255)
                .HasColumnName("apartmentname");
            entity.Property(e => e.Apartmentphotos).HasColumnName("apartmentphotos");
            entity.Property(e => e.Apartmentspace)
                .HasPrecision(10, 2)
                .HasColumnName("apartmentspace");
            entity.Property(e => e.Apartmentstatus)
                .HasMaxLength(50)
                .HasConversion<string>() // Convert Enum to string
                .HasColumnType("text")
                .HasColumnName("apartmentstatus");
            entity.Property(e => e.Apartmenttype)
                .HasMaxLength(50)
                .HasConversion<string>() // Convert Enum to string
                .HasColumnType("text")
                .HasColumnName("apartmenttype");
            entity.Property(e => e.Automationid).HasColumnName("automationid");
            entity.Property(e => e.Availabilitydate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("availabilitydate");
            entity.Property(e => e.Createddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddatetime");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Floornumber).HasColumnName("floornumber");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
            entity.Property(e => e.Planid).HasColumnName("planid");
            entity.Property(e => e.Projectid).HasColumnName("projectid");
        });

        modelBuilder.Entity<ApartmentAddon>(entity =>
        {
            entity.HasKey(e => new { e.Apartmentid, e.Addonid }).HasName("apartment_addon_pkey");

            entity.ToTable("apartment_addon");

            entity.Property(e => e.Apartmentid).HasColumnName("apartmentid");
            entity.Property(e => e.Addonid).HasColumnName("addonid");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
        });

        modelBuilder.Entity<ApartmentAddonperrequest>(entity =>
        {
            entity.HasKey(e => new { e.Apartmentid, e.Addperrequestid }).HasName("apartment_addonperrequest_pkey");

            entity.ToTable("apartment_addonperrequest");

            entity.Property(e => e.Apartmentid).HasColumnName("apartmentid");
            entity.Property(e => e.Addperrequestid).HasColumnName("addperrequestid");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1)
                .HasColumnName("quantity");
        });

        modelBuilder.Entity<Automation>(entity =>
        {
            entity.HasKey(e => e.Automationid).HasName("automation_pkey");

            entity.ToTable("automation");

            entity.Property(e => e.Automationid).HasColumnName("automationid");
            entity.Property(e => e.Automationname)
                .HasMaxLength(255)
                .HasColumnName("automationname");
            entity.Property(e => e.Createdatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdatetime");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
        });

        modelBuilder.Entity<Automationdetail>(entity =>
        {
            entity.HasKey(e => e.Automationdetailsid).HasName("automationdetails_pkey");

            entity.ToTable("automationdetails");

            entity.Property(e => e.Automationdetailsid).HasColumnName("automationdetailsid");
            entity.Property(e => e.Automationdetailsname)
                .HasMaxLength(255)
                .HasColumnName("automationdetailsname");
            entity.Property(e => e.Automationid).HasColumnName("automationid");
            entity.Property(e => e.Createdatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdatetime");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Icon).HasColumnName("icon");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Bookingid).HasName("booking_pkey");

            entity.ToTable("booking");

            entity.Property(e => e.Bookingid).HasColumnName("bookingid");
            entity.Property(e => e.Apartmentid).HasColumnName("apartmentid");
            entity.Property(e => e.Bookingstatus)
            .HasColumnName("bookingstatus")
            .HasConversion<string>()
            .HasColumnType("text"); ;
            entity.Property(e => e.Createdatetime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdatetime");
            entity.Property(e => e.Customerid).HasColumnName("customerid");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
            entity.Property(e => e.Paymentmethodid).HasColumnName("paymentmethodid");
            entity.Property(e => e.Paymentplanid).HasColumnName("paymentplanid");
            entity.Property(e => e.Totalamount)
                .HasPrecision(10, 2)
                .HasColumnName("totalamount");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Apartment).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.Apartmentid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("booking_apartmentid_fkey");

            entity.HasOne(d => d.Customer).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.Customerid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("booking_customerid_fkey");

            entity.HasOne(d => d.Paymentmethod).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.Paymentmethodid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("booking_paymentmethodid_fkey");

            entity.HasOne(d => d.Paymentplan).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.Paymentplanid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("booking_paymentplanid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("booking_userid_fkey");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Customerid).HasName("customer_pkey");

            entity.ToTable("customer");

            entity.Property(e => e.Customerid).HasColumnName("customerid");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Createdatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdatetime");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(255)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
            entity.Property(e => e.Lastname)
                .HasMaxLength(255)
                .HasColumnName("lastname");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(20)
                .HasColumnName("phonenumber");
        });

        modelBuilder.Entity<Developer>(entity =>
        {
            entity.HasKey(e => e.Developerid).HasName("developer_pkey");

            entity.ToTable("developer");

            entity.Property(e => e.Developerid).HasColumnName("developerid");
            entity.Property(e => e.Createdatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdatetime");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Developerlogo).HasColumnName("developerlogo");
            entity.Property(e => e.Developername)
                .HasMaxLength(255)
                .HasColumnName("developername");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
        });

        modelBuilder.Entity<Installmentbreakdown>(entity =>
        {
            entity.HasKey(e => e.Breakdownid).HasName("installmentbreakdown_pkey");

            entity.ToTable("installmentbreakdown");

            entity.Property(e => e.Breakdownid).HasColumnName("breakdownid");
            entity.Property(e => e.Createddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddatetime");
            entity.Property(e => e.Installmentmonth).HasColumnName("installmentmonth");
            entity.Property(e => e.Installmentpercentage)
                .HasPrecision(10, 2)
                .HasColumnName("installmentpercentage");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
            entity.Property(e => e.Paymentplanid).HasColumnName("paymentplanid");

            entity.HasOne(d => d.Paymentplan).WithMany(p => p.Installmentbreakdowns)
                .HasForeignKey(d => d.Paymentplanid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("installmentbreakdown_paymentplanid_fkey");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Invoiceid).HasName("invoices_pkey");

            entity.ToTable("invoices");

            entity.Property(e => e.Invoiceid).HasColumnName("invoiceid");
            entity.Property(e => e.Bookingid).HasColumnName("bookingid");
            entity.Property(e => e.Createdatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdatetime");
            entity.Property(e => e.Invoiceamount)
                .HasPrecision(10, 2)
                .HasColumnName("invoiceamount");
            entity.Property(e => e.Invoiceduedate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("invoiceduedate");
            entity.Property(e => e.Invoicestatus)
                .HasMaxLength(50)
                .HasColumnName("invoicestatus");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
        });

        modelBuilder.Entity<Paymentmethod>(entity =>
        {
            entity.HasKey(e => e.Paymentmethodid).HasName("paymentmethod_pkey");

            entity.ToTable("paymentmethod");

            entity.Property(e => e.Paymentmethodid).HasColumnName("paymentmethodid");
            entity.Property(e => e.Createddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddatetime");
            entity.Property(e => e.Depositpercentage)
                .HasPrecision(10, 2)
                .HasColumnName("depositpercentage");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
            entity.Property(e => e.Numberofinstallments).HasColumnName("numberofinstallments");
            entity.Property(e => e.Paymentmethodname)
                .HasMaxLength(255)
                .HasColumnName("paymentmethodname");
            entity.Property(e => e.Paymentmethodphotos).HasColumnName("paymentmethodphotos");
        });

        modelBuilder.Entity<Paymentplan>(entity =>
        {
            entity.HasKey(e => e.Paymentplanid).HasName("paymentplans_pkey");

            entity.ToTable("paymentplans");

            entity.Property(e => e.Paymentplanid).HasColumnName("paymentplanid");
            entity.Property(e => e.Adminfees).HasColumnName("adminfees");
            entity.Property(e => e.Adminfeespercentage)
                .HasPrecision(10, 2)
                .HasColumnName("adminfeespercentage");
            entity.Property(e => e.Downpayment).HasColumnName("downpayment");
            entity.Property(e => e.Downpaymentpercentage)
                .HasPrecision(10, 2)
                .HasColumnName("downpaymentpercentage");
            entity.Property(e => e.Interestrate).HasColumnName("interestrate");
            entity.Property(e => e.Interestrateperyearpercentage)
                .HasPrecision(5, 2)
                .HasColumnName("interestrateperyearpercentage");
            entity.Property(e => e.Numberofinstallmentmonths).HasColumnName("numberofinstallmentmonths");
            entity.Property(e => e.Paymentplanicon).HasColumnName("paymentplanicon");
            entity.Property(e => e.Paymentplanname)
                .HasMaxLength(255)
                .HasColumnName("paymentplanname");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Permissionid).HasName("permission_pkey");

            entity.ToTable("permission");

            entity.Property(e => e.Permissionid).HasColumnName("permissionid");
            entity.Property(e => e.Action)
                .HasMaxLength(50)
                .HasColumnName("action");
            entity.Property(e => e.Createdatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdatetime");
            entity.Property(e => e.Entityname)
                .HasMaxLength(255)
                .HasColumnName("entityname");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
            entity.Property(e => e.Roleid).HasColumnName("roleid");
        });

        modelBuilder.Entity<Plan>(entity =>
        {
            entity.HasKey(e => e.Planid).HasName("plan_pkey");

            entity.ToTable("plan");

            entity.Property(e => e.Planid).HasColumnName("planid");
            entity.Property(e => e.Createdatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdatetime");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
            entity.Property(e => e.Planname)
                .HasMaxLength(255)
                .HasColumnName("planname");
            entity.Property(e => e.Planphoto).HasColumnName("planphoto");
            entity.Property(e => e.Pricepermeter)
                .HasPrecision(10, 2)
                .HasColumnName("pricepermeter");
        });

        modelBuilder.Entity<Plandetail>(entity =>
        {
            entity.HasKey(e => e.Plandetailsid).HasName("plandetails_pkey");

            entity.ToTable("plandetails");

            entity.Property(e => e.Plandetailsid).HasColumnName("plandetailsid");
            entity.Property(e => e.Createdatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdatetime");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
            entity.Property(e => e.Plandetailsname)
                .HasMaxLength(255)
                .HasColumnName("plandetailsname");
            entity.Property(e => e.Plandetailstype)
                .HasMaxLength(255)
                .HasConversion<string>() // Convert Enum to string
                .HasColumnName("plandetailstype");
            entity.Property(e => e.Planid).HasColumnName("planid");
            entity.Property(e => e.Stars).HasColumnName("stars");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Projectid).HasName("project_pkey");

            entity.ToTable("project");

            entity.Property(e => e.Projectid).HasColumnName("projectid");
            entity.Property(e => e.Amenities).HasColumnName("amenities");
            entity.Property(e => e.Createdatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdatetime");
            entity.Property(e => e.Developerid).HasColumnName("developerid");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .HasColumnName("location");
            entity.Property(e => e.Projectlogo).HasColumnName("projectlogo");
            entity.Property(e => e.Projectname)
                .HasMaxLength(255)
                .HasColumnName("projectname");
            entity.Property(e => e.Totalunits).HasColumnName("totalunits");
        });
        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Questionsid).HasName("questions_pkey");
            entity.ToTable("questions");
            entity.Property(e => e.Questionsid).HasColumnName("questionsid");
            entity.Property(e => e.Answer).HasColumnName("answer");
            entity.Property(e => e.Bookingid).HasColumnName("bookingid");
            entity.Property(e => e.Questionname)
                .HasMaxLength(255)
                .HasColumnName("questionname");
            entity.HasOne(d => d.Booking).WithMany(p => p.Questions)
                .HasForeignKey(d => d.Bookingid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("questions_bookingid_fkey");
        });
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Roleid).HasName("role_pkey");

            entity.ToTable("role");

            entity.Property(e => e.Roleid).HasColumnName("roleid");
            entity.Property(e => e.Createdatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdatetime");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
            entity.Property(e => e.Rolename)
                .HasMaxLength(50)
                .HasColumnName("rolename");
        });

        modelBuilder.Entity<Testimonial>(entity =>
        {
            entity.HasKey(e => e.Testimonialsid).HasName("testimonials_pkey");

            entity.ToTable("testimonials");

            entity.Property(e => e.Testimonialsid).HasColumnName("testimonialsid");
            entity.Property(e => e.Createdatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdatetime");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
            entity.Property(e => e.Testimonialsname)
                .HasMaxLength(255)
                .HasColumnName("testimonialsname");
            entity.Property(e => e.Testimonialsphoto).HasColumnName("testimonialsphoto");
            entity.Property(e => e.Testimonialstitle)
                .HasMaxLength(255)
                .HasColumnName("testimonialstitle");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Createdatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdatetime");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(255)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastlogin)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastlogin");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
            entity.Property(e => e.Lastname)
                .HasMaxLength(255)
                .HasColumnName("lastname");
            entity.Property(e => e.Passwordhash).HasColumnName("passwordhash");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(20)
                .HasColumnName("phonenumber");
            entity.Property(e => e.Roleid).HasColumnName("roleid");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
