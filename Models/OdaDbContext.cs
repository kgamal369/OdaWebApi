using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace OdaWepApi.Models;

public partial class OdaDbContext : DbContext
{

    public OdaDbContext(DbContextOptions<OdaDbContext> options) :
        base(options)
    {
    }

    public virtual DbSet<Addon> Addons { get; set; }

    public virtual DbSet<Apartment> Apartments { get; set; }

    public virtual DbSet<Apartmentaddon> Apartmentaddons { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Package> Packages { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Fallback if the connection string is not passed via DI
            optionsBuilder.UseNpgsql("DefaultConnection");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Addon>(entity =>
        {
            entity.HasKey(e => e.Addonid).HasName("addons_pkey");

            entity.ToTable("addons");

            entity.Property(e => e.Addonid).HasColumnName("addonid");
            entity.Property(e => e.Addonname)
                .HasMaxLength(255)
                .HasColumnName("addonname");
            entity.Property(e => e.Addontype)
                .HasMaxLength(50)
                .HasColumnName("addontype");
            entity.Property(e => e.Brand).HasColumnName("brand");
            entity.Property(e => e.Createddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddatetime");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
            entity.Property(e => e.Priceperunit)
                .HasPrecision(10, 2)
                .HasColumnName("priceperunit");
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
                .HasColumnName("apartmentstatus");
            entity.Property(e => e.Apartmenttype)
                .HasMaxLength(50)
                .HasColumnName("apartmenttype");
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
            entity.Property(e => e.Projectid).HasColumnName("projectid");

            entity.HasOne(d => d.Project).WithMany(p => p.Apartments)
                .HasForeignKey(d => d.Projectid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("apartment_projectid_fkey");
        });

        modelBuilder.Entity<Apartmentaddon>(entity =>
        {
            entity.HasKey(e => e.Apartmentaddonsid).HasName("apartmentaddons_pkey");

            entity.ToTable("apartmentaddons");

            entity.Property(e => e.Apartmentaddonsid).HasColumnName("apartmentaddonsid");
            entity.Property(e => e.Addonid).HasColumnName("addonid");
            entity.Property(e => e.Apartmentid).HasColumnName("apartmentid");
            entity.Property(e => e.Assignedaddons).HasColumnName("assignedaddons");
            entity.Property(e => e.Availableaddons).HasColumnName("availableaddons");
            entity.Property(e => e.Createdatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdatetime");
            entity.Property(e => e.Installedamount).HasColumnName("installedamount");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
            entity.Property(e => e.Maxavailable).HasColumnName("maxavailable");

            entity.HasOne(d => d.Addon).WithMany(p => p.Apartmentaddons)
                .HasForeignKey(d => d.Addonid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("apartmentaddons_addonid_fkey");

            entity.HasOne(d => d.Apartment).WithMany(p => p.Apartmentaddons)
                .HasForeignKey(d => d.Apartmentid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("apartmentaddons_apartmentid_fkey");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Bookingid).HasName("booking_pkey");

            entity.ToTable("booking");

            entity.Property(e => e.Bookingid).HasColumnName("bookingid");
            entity.Property(e => e.Apartmentid).HasColumnName("apartmentid");
            entity.Property(e => e.Bookingstatus).HasColumnName("bookingstatus");
            entity.Property(e => e.Createdatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdatetime");
            entity.Property(e => e.Customerid).HasColumnName("customerid");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
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

            entity.HasOne(d => d.Booking).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.Bookingid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("invoices_bookingid_fkey");
        });

        modelBuilder.Entity<Package>(entity =>
        {
            entity.HasKey(e => e.Packageid).HasName("package_pkey");

            entity.ToTable("package");

            entity.Property(e => e.Packageid).HasColumnName("packageid");
            entity.Property(e => e.Apartmentid).HasColumnName("apartmentid");
            entity.Property(e => e.Assignedpackage).HasColumnName("assignedpackage");
            entity.Property(e => e.Createdatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdatetime");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
            entity.Property(e => e.Packagename)
                .HasMaxLength(255)
                .HasColumnName("packagename");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");

            entity.HasOne(d => d.Apartment).WithMany(p => p.Packages)
                .HasForeignKey(d => d.Apartmentid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("package_apartmentid_fkey");
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

            entity.HasOne(d => d.Role).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.Roleid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("permission_roleid_fkey");
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

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.Roleid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("users_roleid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
